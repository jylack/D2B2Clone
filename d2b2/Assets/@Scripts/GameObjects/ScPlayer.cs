using UnityEngine;

public class ScPlayer : ScObjectBase
{
    [SerializeField] private CharacterController characterController;
    [Header("move")]
    [SerializeField] private float swingThresholdIntervalTime;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float swingForwardZPosition;
    [SerializeField] private float swingBackwardZPosition;
    [Header("head")]
    [SerializeField] private Transform xrOriginTrans;
    [SerializeField] private float headTurnThreshold;

    private Camera mainCam;
    private AudioSource audioSource;
    private ScDefine.ScHeadTurn headTurn = ScDefine.ScHeadTurn.Forward;
    private bool isLeftHandUp;
    private bool isRightHandUp;
    private bool isMoving;
    private float leftHandForwardTime;
    private float leftHandBackwardTime;
    private float rightHandForwardTime;
    private float rightHandBackwardTime;
    private Vector3 headPosition;
    private float headTurnThresholdQuaternion;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        mainCam = Camera.main;
        headTurnThresholdQuaternion = Quaternion.Euler(0f, headTurnThreshold, 0f).y;

        Manager.Instance.InputMgr.OnHeadPositionChanged += OnHeadPositionChanged;
        Manager.Instance.InputMgr.OnLeftHandPositionChanged += OnLeftHandPositionChanged;
        Manager.Instance.InputMgr.OnRightHandPositionChanged += OnRightHandPositionChanged;
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
        float rotationY = mainCam.transform.localRotation.y;
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

        // 이동중지
        if (isMoving)
        {
            isMoving = false;
            Manager.Instance.GameMgr.RaisePlayerMovingEvent(false);
        }
    }

    private void MoveForward()
    {
        if (!Manager.Instance.GameMgr.canMove)
            return;

        characterController.Move(moveSpeed * Time.deltaTime * characterController.transform.forward);

        if (!isMoving)
        {
            isMoving = true;
            Manager.Instance.GameMgr.RaisePlayerMovingEvent(true);
        }
    }

    // event
    private void OnHeadPositionChanged(Vector3 pos)
    {
        headPosition = pos;
    }

    private void OnLeftHandPositionChanged(Vector3 pos)
    {
        bool leftHandUp = pos.y > headPosition.y;
        if (leftHandUp != isLeftHandUp)
        {
            isLeftHandUp = leftHandUp;
            Manager.Instance.GameMgr.RaisePlayerHandsUpEvent(isLeftHandUp, isRightHandUp);
        }

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
            Manager.Instance.GameMgr.RaisePlayerHandsUpEvent(isLeftHandUp, isRightHandUp);
        }

        if (pos.y < headPosition.y)
        {
            if (pos.z > swingForwardZPosition)
                rightHandForwardTime = Time.time;
            else if (pos.z < swingBackwardZPosition)
                rightHandBackwardTime = Time.time;
        }
    }
}