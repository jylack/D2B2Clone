using System.Collections;
using TMPro;
using UnityEngine;

public partial class ScPlayerCtrl : MonoBehaviour
{
    [SerializeField] ScControllerCtrl rightHand;
    [SerializeField] ScControllerCtrl leftHand;

    [SerializeField] TextMeshProUGUI RightText;
    [SerializeField] TextMeshProUGUI LeftText;

    [SerializeField] float moveSpeed;
    [SerializeField] float swingTime = 0.5f;

    CharacterController characterController;

    bool isRightHandMoving;
    bool isLeftHandMoving;

    bool isMoving;

    private void InitMoving()
    {
        characterController = GetComponent<CharacterController>();

        //isFrontCheck = false;
        //isBodyCheck = false;

        isRightHandMoving = false;
        isLeftHandMoving = false;

        isMoving = false;

        Manager.Instance.InputMgr.OnRightHandPositionChanged += OnRightHandMoving;
        Manager.Instance.InputMgr.OnLeftHandPositionChanged += OnLeftHandMoving;

        rightHand.Init(swingTime);
        leftHand.Init(swingTime);

    }



    private void OnLeftHandMoving(Vector3 pos)
    {
        isLeftHandMoving = leftHand.IsMoving;
        LeftText.text = "Left : " + isLeftHandMoving;

        if (isLeftHandMoving && isMoving == false)
            Moving();
    }

    private void OnRightHandMoving(Vector3 pos)
    {
        isRightHandMoving = rightHand.IsMoving;
        RightText.text = "Right : " + isRightHandMoving;

        if (isRightHandMoving && isMoving == false) 
            Moving();
    }


    private void Moving()
    {
        characterController.Move(transform.forward * Time.deltaTime * moveSpeed);
        isMoving = true;
    }

   
}
