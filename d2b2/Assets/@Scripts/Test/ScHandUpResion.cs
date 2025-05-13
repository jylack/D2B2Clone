using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScHandUpResion : MonoBehaviour
{
    private bool handUpMissionClear;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == ScDefine.Layer.PlayerIndex)
        {
            Manager.Instance.GameMgr.OnPlayerHandsUp += CheckHandUp;
        }
    }

    private void OnTriggerStay(Collider other)
    {

    }
    private void OnTriggerExit(Collider other)
    {
        Manager.Instance.GameMgr.OnPlayerHandsUp -= CheckHandUp;
    }
    private void CheckHandUp(bool ledftHand, bool rightHand)
    {
        Debug.Log("ledftHand : " + ledftHand);
        if (ledftHand == false && rightHand == false)
        {
            handUpMissionClear = false;
        }
    }
    
}
