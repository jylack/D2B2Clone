using System.Collections;
using System.Collections.Generic;
using Photon.Pun.Demo.PunBasics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScHandUpRegion : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMeshProUGUI;
    private bool handUpMissionClear;
    ScHandUpProgress handUpProgress;
    Vector3 headPosition;
    private bool isLeftHandUp;
    private bool isRightHandUp;
    private void Start()
    {
        handUpProgress = UIPlayerHsy.Instance.GetHandUpComponent();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == ScDefine.Layer.PlayerIndex)
        {
            Manager.Instance.InputMgr.OnLeftHandPositionChanged += CheckLeftHandUp;
            Manager.Instance.InputMgr.OnHeadPositionChanged += OnHeadPositionChanged;
            UIPlayerHsy.Instance.OnHandUpProgressUI();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == ScDefine.Layer.PlayerIndex)
        {
            Manager.Instance.InputMgr.OnLeftHandPositionChanged -= CheckLeftHandUp;
            Manager.Instance.InputMgr.OnHeadPositionChanged -= OnHeadPositionChanged;
            UIPlayerHsy.Instance.OffHandUpProgressUI();
        }
    }
    private void OnHeadPositionChanged(Vector3 pos)
    {
        Debug.Log("È÷È÷È÷22");
        headPosition = pos;
    }

    private void CheckLeftHandUp(Vector3 handPos)
    {
        bool leftHandUp = handPos.y > headPosition.y;
        textMeshProUGUI.text = leftHandUp.ToString();
        handUpProgress.onProgress?.Invoke(headPosition.y, handPos.y);
    }

}
