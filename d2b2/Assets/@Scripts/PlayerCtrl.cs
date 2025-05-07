using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerCtrl : MonoBehaviour
{

    private void Start()
    {
        isLeftHandUp = false;
        isRightHandUp = false;

        Manager.Instance.InputMgr.OnHeadRotatingPerform += OnHeadRotation;
        Manager.Instance.InputMgr.OnLeftHandPositionChanged += OnLeftHandUp;
        Manager.Instance.InputMgr.OnRightHandPositionChanged += OnRightHandUp;
        
        
        InitMoving();
    }
}
