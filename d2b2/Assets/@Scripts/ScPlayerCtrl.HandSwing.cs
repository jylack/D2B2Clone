using TMPro;
using UnityEngine;

public partial class ScPlayerCtrl : MonoBehaviour
{
    [SerializeField] ScControllerCtrl _rightHand;
    [SerializeField] ScControllerCtrl _leftHand;

    [SerializeField] TextMeshProUGUI text;

    [SerializeField] float moveSpeed = 1f;

    CharacterController characterController;

    bool isRightHandMoving;
    bool isLeftHandMoving;

    bool isFrontCheck;
    bool isBodyCheck;


    private void InitMoving()
    {
        characterController = GetComponent<CharacterController>();

        isFrontCheck = false;
        isBodyCheck = false;

        isRightHandMoving = false;
        isLeftHandMoving = false;

        Manager.Instance.InputMgr.OnRightHandPositionChanged += OnRightHandMoving;
        Manager.Instance.InputMgr.OnLeftHandPositionChanged += OnLeftHandMoving;
    }



    private void OnLeftHandMoving(Vector3 pos)
    {
        var isMoving = _leftHand.IsMoving();

        text.text = "LeftHand : " + _leftHand.IsMoving().ToString();

        if (isMoving)
        {
            Moving();
        }
    }

    private void OnRightHandMoving(Vector3 pos)
    {
        var isMoving = _rightHand.IsMoving();

        text.text = "RightHand : " + isMoving.ToString();

        if (isMoving)
        {
            Moving();
        }
    }

    private void Moving()
    {        
        characterController.Move(transform.forward * Time.deltaTime * moveSpeed);
    }

}
