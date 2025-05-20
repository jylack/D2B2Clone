using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ScPlayer : ScObjectBase
{
    //[SerializeField] private XROrigin xrOrigin;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private CharacterController characterController;
    [Header("move")]
    [SerializeField] private ActionBasedContinuousMoveProvider moveProv;
    [SerializeField] private float swingThresholdIntervalTime;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float swingForwardZPosition;
    [SerializeField] private float swingBackwardZPosition;
    [Header("head")]
    [SerializeField] private Transform xrOriginTrans;
    [SerializeField] private float headTurnThreshold;
    [SerializeField] private float maxHandHeight;

    public CharacterController CharacterController => characterController;
    private AudioSource audioSource;
    private ScDefine.ScHeadTurn headTurn = ScDefine.ScHeadTurn.Forward;
    private bool isLeftHandUp;
    private bool isRightHandUp;
    private bool isMoving;
    private bool isleftStickMove;
    private float leftHandForwardTime;
    private float leftHandBackwardTime;
    private float rightHandForwardTime;
    private float rightHandBackwardTime;
    private Vector3 headPosition;
    private float headTurnThresholdQuaternion;



    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
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
        //ResetCamera();
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



    public void PlaySound(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }



    private void UpdateHeadTurn()
    {
        var tempHeadTurn = ScDefine.ScHeadTurn.None;
        //float rotationY = xrOrigin.Camera.transform.localRotation.y;
        float rotationY = mainCamera.transform.localRotation.y;
        bool lookingLeft = rotationY < -headTurnThresholdQuaternion;
        bool lookingRight = rotationY > headTurnThresholdQuaternion;
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

        if (isleftStickMove)
        {
            isMoving = true;
            Manager.Instance.GameMgr.RaisePlayerMovingEvent(isMoving);

            return;
        }

        // 왼손 체크
        bool isMoveStart = Time.time - leftHandForwardTime <= 0.5f;
        bool isValidSwingIntervalTime = Mathf.Abs(leftHandForwardTime - leftHandBackwardTime) <= swingThresholdIntervalTime;

        if (isMoveStart && isValidSwingIntervalTime)
        {
            MoveForward();
            return;
        }

        // 오른손 체크
        isMoveStart = Time.time - rightHandForwardTime <= 0.5f;
        isValidSwingIntervalTime = Mathf.Abs(rightHandForwardTime - rightHandBackwardTime) <= swingThresholdIntervalTime;

        if (isMoveStart && isValidSwingIntervalTime)
        {
            MoveForward();
            return;
        }
    }

    private void MoveForward()
    {
        characterController.Move(moveSpeed * Time.deltaTime * characterController.transform.forward);

        isMoving = true;
        Manager.Instance.GameMgr.RaisePlayerMovingEvent(isMoving);

    }

    public void ResetCamera()
    {
        // HMD의 초기 로컬 포지션과 회전 가져오기
        //if (xrOrigin.Camera != null)
        //{
        //    //Vector3 cameraOffset = xrOrigin.Camera.transform.localPosition;
        //    //Quaternion cameraRotation = xrOrigin.Camera.transform.localRotation;

        //    // 카메라가 위치한 지점 기준으로 XR Origin을 반대로 이동시켜 중앙 정렬
        //    xrOrigin.MoveCameraToWorldLocation(Vector3.zero);
        //}

        // 또는 HMD 위치를 기준으로 강제로 위치를 조정하고 싶다면:
        //xrOrigin.transform.position = Vector3.zero;
        //xrOrigin.transform.rotation = Quaternion.identity;
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
            //Manager.Instance.GameMgr.RaisePlayerHandsUpEvent(isLeftHandUp, isRightHandUp, distance);
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
        isleftStickMove = isStick;
    }
}