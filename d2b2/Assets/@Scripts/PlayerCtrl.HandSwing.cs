using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public partial class PlayerCtrl : MonoBehaviour
{

    bool isRightHandMoving;
    bool isLeftHandMoving;

    private void InitMoving()
    {
        isRightHandMoving = false;
        isLeftHandMoving = false;

        Manager.Instance.InputMgr.OnRightHandPositionChanged += OnRightHandMoving;
        Manager.Instance.InputMgr.OnLeftHandPositionChanged += OnLeftHandMoving;
    }

    private void OnLeftHandMoving(Vector3 pos)
    {

    }

    private void OnRightHandMoving(Vector3 pos)
    {

    }

    

}
