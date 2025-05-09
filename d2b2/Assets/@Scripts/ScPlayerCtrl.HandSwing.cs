using TMPro;
using UnityEngine;

public partial class ScPlayerCtrl : MonoBehaviour
{
    [Header("이동관련 오브젝트 연결")]
    [SerializeField] ScControllerCtrl rightHand;
    [SerializeField] ScControllerCtrl leftHand;

    [SerializeField] TextMeshProUGUI RightText;
    [SerializeField] TextMeshProUGUI LeftText;

    [Header("이동 설정값")]
    [SerializeField] float moveSpeed;

    [SerializeField] float swingTime = 0.5f;

    CharacterController characterController;

    bool isRightHandMoving;
    bool isLeftHandMoving;




    private void InitMoving()
    {
        characterController = GetComponent<CharacterController>();

        //isFrontCheck = false;
        //isBodyCheck = false;

        isRightHandMoving = false;
        isLeftHandMoving = false;


        Manager.Instance.InputMgr.OnRightHandPositionChanged += OnRightHandMoving;
        Manager.Instance.InputMgr.OnLeftHandPositionChanged += OnLeftHandMoving;

        rightHand.Init(swingTime);
        leftHand.Init(swingTime);


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
