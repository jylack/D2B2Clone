using System.Collections;
using System.Collections.Generic;
using Photon.Pun.Demo.PunBasics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScHandUpRegion : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
    public bool handUpMissionClear { get; private set; }
    private Vector3 headPosition;
    public bool isLeftHandUp { get; private set; }
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
            if (isLeftHandUp == true)
            {
                handUpMissionClear = true;
            }
        }
    }
    private void OnHeadPositionChanged(Vector3 pos)
    {
        headPosition = pos;
    }

    private void CheckLeftHandUp(Vector3 handPos)
    {
        isLeftHandUp = handPos.y > headPosition.y;
        if (isLeftHandUp == false)
        {
            handUpMissionClear = false;
        }
        textMeshProUGUI.text = isLeftHandUp.ToString();
        UIPlayerHsy.Instance.DrawHandUpProgress(headPosition.y, handPos.y,10);
    }

}
