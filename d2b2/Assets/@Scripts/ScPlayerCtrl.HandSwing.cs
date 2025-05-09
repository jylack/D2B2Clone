using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public partial class ScPlayerCtrl : MonoBehaviour
{
    [Header("스폰 위치")]
    [SerializeField] Transform spawnPoint;

    [Header("이동관련 오브젝트 연결")]
    [SerializeField] ScControllerCtrl rightHand;
    [SerializeField] ScControllerCtrl leftHand;

    [SerializeField] TextMeshProUGUI RightText;
    [SerializeField] TextMeshProUGUI LeftText;

    [Header("이동 설정값")]
    [SerializeField] float moveSpeed;

    [SerializeField] float swingTime = 0.5f;

    CharacterController characterController;

    TeleportationProvider tel;

    bool isRightHandMoving;
    bool isLeftHandMoving;

    public bool IsMoving()
    {
        return isRightHandMoving || isLeftHandMoving;
    }

    private void SetPos(Transform spawn)
    {
        var temp = new TeleportRequest()
        {
            destinationPosition = spawn.position,
            destinationRotation = spawn.rotation
        };

        tel.QueueTeleportRequest(temp);
    }

    private void InitMoving()
    {
        //TODO : 사용되는 씬에서 스폰 포인트 오브젝트 명이 다를경우 바꿔줘야함. 
        if (spawnPoint == null)
        {
            spawnPoint = GameObject.Find("spawn point1").transform;
        }

        characterController = GetComponent<CharacterController>();

        //TODO : 이 스크립트가 XR Origin에서 이동할경우 경로 바꿔줘야함.
        tel = transform.Find("Locomotion System/Teleportation").GetComponent<TeleportationProvider>();

        if (tel == null)
            Debug.Log("tel 연결 실패!!!");

        //isFrontCheck = false;
        //isBodyCheck = false;

        isRightHandMoving = false;
        isLeftHandMoving = false;


        Manager.Instance.InputMgr.OnRightHandPositionChanged += OnRightHandMoving;
        Manager.Instance.InputMgr.OnLeftHandPositionChanged += OnLeftHandMoving;

        rightHand.Init(swingTime);
        leftHand.Init(swingTime);


        SetPos(spawnPoint);
    }



    private void OnLeftHandMoving(Vector3 pos)
    {
        isLeftHandMoving = leftHand.IsMoving;
        LeftText.text = "Left : " + isLeftHandMoving;

        //if (isLeftHandMoving)
        //{
        //    Moving();
        //}
    }

    private void OnRightHandMoving(Vector3 pos)
    {
        isRightHandMoving = rightHand.IsMoving;
        RightText.text = "Right : " + isRightHandMoving;

        //if (isRightHandMoving)
        //{
        //    Moving();
        //}
    }

    private void Update()
    {
        if (isRightHandMoving || isLeftHandMoving)
        {
            Moving();
        }
    }

    private void Moving()
    {
        characterController.Move(transform.forward * Time.deltaTime * moveSpeed);
    }


}
