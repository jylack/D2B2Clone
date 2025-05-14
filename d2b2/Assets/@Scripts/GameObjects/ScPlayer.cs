using System;
using System.Collections;
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

    private bool inHandUpRegion;
    private bool lookLeftClear;
    private bool lookRightClear;
    public bool handUpMissionClear { get; set; }
    public bool lookAroundMissionClear { get; set; }
    private Coroutine lookAroundCor;
    [SerializeField] private float maxHandupDistance;
    [SerializeField] private float lookAroundComplateTime;



    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        mainCam = Camera.main;
        headTurnThresholdQuaternion = Quaternion.Euler(0f, headTurnThreshold, 0f).y;

        Manager.Instance.InputMgr.OnHeadPositionChanged += OnHeadPositionChanged;
        //Manager.Instance.InputMgr.OnLeftHandPositionChanged += OnLeftHandPositionChanged;
        //Manager.Instance.InputMgr.OnRightHandPositionChanged += OnRightHandPositionChanged;
    }

    private void Start()
    {
        Manager.Instance.GameMgr.SetPlayer(this);
    }

    private void Update()
    {
        if (inHandUpRegion == true)
        {
            UpdateHeadTurn();
        }
        UpdateMove();
    }

    private void OnDestroy()
    {
        Manager.Instance.InputMgr.OnHeadPositionChanged -= OnHeadPositionChanged;
        //Manager.Instance.InputMgr.OnLeftHandPositionChanged -= OnLeftHandPositionChanged;
        //Manager.Instance.InputMgr.OnRightHandPositionChanged -= OnRightHandPositionChanged;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == ScDefine.Layer.HandUpCheckResionIndex) // 손들기 감지 지역이라면
        {
            if (SecondStageManager.Instance.handUpRegionClear == true)
            {
                return;
            }
            handUpMissionClear = true;
            Manager.Instance.InputMgr.OnLeftHandPositionChanged += OnLeftHandPositionChanged;
            Manager.Instance.InputMgr.OnHeadPositionChanged += OnHeadPositionChanged;
            UIPlayerHsy.Instance.OnHandUpProgressUI();
        }
        else if (other.gameObject.layer == ScDefine.Layer.LookAroundCheckResionIndex) // 고개 돌리기 감지 지역이라면
        {
            if (SecondStageManager.Instance.lookAroundRegionClear == true)
            {
                return;
            }
            Manager.Instance.GameMgr.OnPlayerHeadTurn += CheckPlayerHeadTurn;
            inHandUpRegion = true;
            lookLeftClear = false;
            lookRightClear = false;
            lookAroundMissionClear = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == ScDefine.Layer.HandUpCheckResionIndex) // 손들기 감지 지역이라면
        {
            Manager.Instance.InputMgr.OnLeftHandPositionChanged -= OnLeftHandPositionChanged;
            Manager.Instance.InputMgr.OnHeadPositionChanged -= OnHeadPositionChanged;
            UIPlayerHsy.Instance.OffHandUpProgressUI();
            UIPlayerHsy.Instance.handUpText.text = "";
        }
        else if (other.gameObject.layer == ScDefine.Layer.LookAroundCheckResionIndex) // 고개 돌리기 감지 지역이라면
        {
            Manager.Instance.GameMgr.OnPlayerHeadTurn -= CheckPlayerHeadTurn;
            inHandUpRegion = false;
        }
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
    private void CheckPlayerHeadTurn(ScDefine.ScHeadTurn headDirection)
    {
        if (lookAroundMissionClear == true) return;
        if (headDirection == ScDefine.ScHeadTurn.Left && lookLeftClear == false)
        {
            UIPlayerHsy.Instance.OnLookAroundLeftProgress();
            lookAroundCor = StartCoroutine(CheckHeadStayTime(headDirection));
        }
        else if (headDirection == ScDefine.ScHeadTurn.Right && lookLeftClear == true && lookRightClear == false)
        {
            UIPlayerHsy.Instance.OnLookAroundRightProgress();
            lookAroundCor = StartCoroutine(CheckHeadStayTime(headDirection));
        }
        else if (headDirection == ScDefine.ScHeadTurn.Forward)
        {
            StopCurrentCoroutine();
            if (lookLeftClear == false)
            {
                UIPlayerHsy.Instance.OffLookAroundLeftProgress();
            }
            if (lookRightClear == false)
            {
                UIPlayerHsy.Instance.OffLookAroundRightProgress();
            }
        }
    }
    private IEnumerator CheckHeadStayTime(ScDefine.ScHeadTurn headDirection)
    {
        float timer = 0f;
        while (timer < lookAroundComplateTime)
        {
            timer += Time.deltaTime;
            UIPlayerHsy.Instance.DrawLookArounProgress(timer, lookAroundComplateTime, headDirection);
            yield return null;
        }

        if (headDirection == ScDefine.ScHeadTurn.Left)
        {
            lookLeftClear = true;
            Debug.Log("왼쪽 완료");
        }
        else
        {
            lookRightClear = true;
            lookAroundMissionClear = true;
            Debug.Log("오른쪽 완료");
        }

        lookAroundCor = null;

        if (lookAroundMissionClear)
        {
            Debug.Log("미션 성공!");
            OffAllUI();
        }
    }
    private void StopCurrentCoroutine()
    {
        if (lookAroundCor != null)
        {
            StopCoroutine(lookAroundCor);
            lookAroundCor = null;
        }
    }
    private void OffAllUI()
    {
        UIPlayerHsy.Instance.OffLookAroundLeftProgress();
        UIPlayerHsy.Instance.OffLookAroundRightProgress();
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

        //if (!Manager.Instance.GameMgr.canMove)
        //    return;

        int move = Manager.Instance.GameMgr.canMove ? 1 : 0;

        //Debug.Log("move : " + Manager.Instance.GameMgr.canMove);
        characterController.Move(move * moveSpeed * Time.deltaTime * characterController.transform.forward);

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
        UIPlayerHsy.Instance.DrawHandUpProgress(headPosition.y,pos.y,maxHandupDistance);
        UIPlayerHsy.Instance.handUpText.text = leftHandUp.ToString();
        if (leftHandUp != isLeftHandUp)
        {
            isLeftHandUp = leftHandUp;
            Manager.Instance.GameMgr.RaisePlayerHandsUpEvent(isLeftHandUp, isRightHandUp);
        }
        if (isLeftHandUp == false)
        {
            handUpMissionClear = false;
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