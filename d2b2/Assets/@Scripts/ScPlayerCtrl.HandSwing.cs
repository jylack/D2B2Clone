using System.Collections;
using UnityEngine;

public partial class ScPlayerCtrl : MonoBehaviour
{

    bool isRightHandMoving;
    bool isLeftHandMoving;

    bool isFrontCheck;
    bool isBodyCheck;


    private void InitMoving()
    {
        isFrontCheck = false;
        isBodyCheck = false;

        isRightHandMoving = false;
        isLeftHandMoving = false;

        Manager.Instance.InputMgr.OnRightHandPositionChanged += OnRightHandMoving;
        Manager.Instance.InputMgr.OnLeftHandPositionChanged += OnLeftHandMoving;
    }



    private void OnLeftHandMoving(Vector3 pos)
    {

        var t1 = "left : " + pos;

        var t2 = _leftHand.IsMoving();

        text.text = t1 + t2;


    }

    private void OnRightHandMoving(Vector3 pos)
    {

        var t1 = "right : " + pos;

        var t2 = _rightHand.IsMoving();

        text.text = t1 + t2;
    }

    

}
