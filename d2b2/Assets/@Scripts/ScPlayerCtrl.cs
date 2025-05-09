using TMPro;
using UnityEngine;

public partial class ScPlayerCtrl : MonoBehaviour
{


    private void Start()
    {
        StartCoroutine(RecenterView());
        isLeftHandUp = false;
        isRightHandUp = false;

        Manager.Instance.InputMgr.OnLeftHandPositionChanged += OnLeftHandUp;
        Manager.Instance.InputMgr.OnRightHandPositionChanged += OnRightHandUp;


        InitMoving();
    }
}
