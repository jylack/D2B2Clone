using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ScPlayer : ScPlayerBase
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private CharacterController characterController;
    [Header("move")]
    [SerializeField] private ActionBasedContinuousMoveProvider moveProv;
    [SerializeField] private float swingThresholdIntervalTime;
    [SerializeField] private float swingForwardZPosition;
    [SerializeField] private float swingBackwardZPosition;
    [Header("head")]
    [SerializeField] private Transform xrOriginTrans;
    [SerializeField] private float headTurnThreshold;
    [SerializeField] private float maxHandHeight;

    public CharacterController CharacterController => characterController;
    public override Camera MainCamera => mainCamera;

    private ScDefine.ScHeadTurn headTurn = ScDefine.ScHeadTurn.Forward;
    private bool isLeftHandUp;
    private bool isRightHandUp;
    private bool isMoving;
    private bool isLeftStickMove;
    private float leftHandForwardTime;
    private float leftHandBackwardTime;
    private float rightHandForwardTime;
    private float rightHandBackwardTime;
    private Vector3 headPosition;
    private float headTurnThresholdQuaternion;



    private void Awake()
    {
        headTurnThresholdQuaternion = Quaternion.Euler(0f, headTurnThreshold, 0f).y;

        Manager.Instance.InputMgr.OnHeadPositionChanged += OnHeadPositionChanged;
        Manager.Instance.InputMgr.OnLeftHandPositionChanged += OnLeftHandPositionChanged;
        Manager.Instance.InputMgr.OnRightHandPositionChanged += OnRightHandPositionChanged;
        Manager.Instance.InputMgr.OnLeftStickMove += OnLeftStickMove;
        //스틱 이동 방향이 헤드따라가는걸 XrOrigin 기준으로 바꿈
        moveProv.forwardSource = characterController.transform;
    }

    private void Start()
    {
        Manager.Instance.GameMgr.SetPlayer(this);
    }

    private void Update()
    {
        UpdateHeadTurn();
        UpdateMove();
    }



    private void OnDestroy()
    {
        Manager.Instance.InputMgr.OnHeadPositionChanged -= OnHeadPositionChanged;
        Manager.Instance.InputMgr.OnLeftHandPositionChanged -= OnLeftHandPositionChanged;
        Manager.Instance.InputMgr.OnRightHandPositionChanged -= OnRightHandPositionChanged;
        Manager.Instance.InputMgr.OnLeftStickMove -= OnLeftStickMove;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        float chestHeight = 1f;

        Gizmos.color = Color.blue;
        Vector3 forwardCenter = transform.position + (Vector3.up * chestHeight) + (Vector3.forward * swingForwardZPosition);
        Vector3 forwardLeft = forwardCenter + Vector3.left * 1f;
        Vector3 forwardRight = forwardCenter + Vector3.right * 1f;
        Gizmos.DrawLine(forwardLeft, forwardRight);

        Gizmos.color = Color.green;
        Vector3 backwardCenter = transform.position + (Vector3.up * chestHeight) + (Vector3.forward * swingBackwardZPosition);
        Vector3 backwardLeft = backwardCenter + Vector3.left * 1f;
        Vector3 backwardRight = backwardCenter + Vector3.right * 1f;
        Gizmos.DrawLine(backwardLeft, backwardRight);
    }



    private void UpdateHeadTurn()
    {
        var tempHeadTurn = ScDefine.ScHeadTurn.None;
        float rotationY = mainCamera.transform.localRotation.eulerAngles.y;
        if (rotationY > 180f)
            rotationY -= 360f;
        bool lookingLeft = rotationY < -headTurnThreshold;
        bool lookingRight = rotationY > headTurnThreshold;
        if (lookingLeft && headTurn != ScDefine.ScHeadTurn.Left)
        {
            tempHeadTurn = ScDefine.ScHeadTurn.Left;
        }
        else if (lookingRight && headTurn != ScDefine.ScHeadTurn.Right)
        {
            tempHeadTurn = ScDefine.ScHeadTurn.Right;
        }
        else if (!lookingLeft && !lookingRight && headTurn != ScDefine.ScHeadTurn.Forward)
        {
            tempHeadTurn = ScDefine.ScHeadTurn.Forward;
        }

        if (tempHeadTurn != ScDefine.ScHeadTurn.None && tempHeadTurn != headTurn)
        {
            headTurn = tempHeadTurn;
            Manager.Instance.GameMgr.RaisePlayerHeadTurnEvent(headTurn);
        }

    }

    private void UpdateMove()
    {
        isMoving = false;

        if (isLeftStickMove)
        {
            isMoving = true;
            return;
        }

        bool isMoveStart;
        bool isValidSwingIntervalTime;

        // 왼손 체크
        if (leftHandForwardTime > 0 && leftHandBackwardTime > 0)
        {
            isMoveStart = Time.time - leftHandForwardTime <= 0.5f;
            isValidSwingIntervalTime = Mathf.Abs(leftHandForwardTime - leftHandBackwardTime) <= swingThresholdIntervalTime;

            if (isMoveStart && isValidSwingIntervalTime)
            {
                MoveForward();
                return;
            }
        }

        // 오른손 체크
        if (rightHandForwardTime > 0 && rightHandBackwardTime > 0)
        {
            isMoveStart = Time.time - rightHandForwardTime <= 0.5f;
            isValidSwingIntervalTime = Mathf.Abs(rightHandForwardTime - rightHandBackwardTime) <= swingThresholdIntervalTime;

            if (isMoveStart && isValidSwingIntervalTime)
            {
                MoveForward();
                return;
            }
        }

        isMoving = false;
        Manager.Instance.GameMgr.RaisePlayerMovingEvent(isMoving);
    }

    private void MoveForward()
    {
        characterController.Move(moveProv.moveSpeed * Time.deltaTime * characterController.transform.forward);

        isMoving = true;
        Manager.Instance.GameMgr.RaisePlayerMovingEvent(isMoving);
    }

    // event
    private void OnHeadPositionChanged(Vector3 pos)
    {
        headPosition = pos;
    }

    private void OnLeftHandPositionChanged(Vector3 pos)
    {
        bool leftHandUp = pos.y > headPosition.y;
        float temp = pos.y - headPosition.y;
        float distance = Mathf.InverseLerp(-maxHandHeight, maxHandHeight, temp);

        Manager.Instance.GameMgr.RaisePlayerHandsUpEvent(leftHandUp, isRightHandUp, distance);

        if (pos.y < headPosition.y)
        {
            if (pos.z > swingForwardZPosition)
                leftHandForwardTime = Time.time;
            else if (pos.z < swingBackwardZPosition)
                leftHandBackwardTime = Time.time;
        }
    }

    private void OnRightHandPositionChanged(Vector3 pos)
    {
        bool rightHandUp = pos.y > headPosition.y;
        if (rightHandUp != isRightHandUp)
        {
            isRightHandUp = rightHandUp;
        }

        if (pos.y < headPosition.y)
        {
            if (pos.z > swingForwardZPosition)
                rightHandForwardTime = Time.time;
            else if (pos.z < swingBackwardZPosition)
                rightHandBackwardTime = Time.time;
        }
    }

    private void OnLeftStickMove(bool isStick)
    {
        isLeftStickMove = isStick;
        Manager.Instance.GameMgr.RaisePlayerMovingEvent(isStick);
    }
}