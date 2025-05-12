using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ScLookAroundResion : MonoBehaviour
{
    private bool checkLookLeft;
    private bool checkLookRight;
    private bool lookAroundMissionClear;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == ScDefine.Layer.PlayerIndex)
        {
            Manager.Instance.GameMgr.OnPlayerHeadTurn += CheckPlayerHeadTurn;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Manager.Instance.GameMgr.OnPlayerHeadTurn -= CheckPlayerHeadTurn;
    }
    private void CheckPlayerHeadTurn(ScDefine.ScHeadTurn headTurn)
    {
        if (checkLookLeft == true && checkLookRight == true)
        {
            lookAroundMissionClear = true;
            Debug.Log("주위 둘러보기 미션 : " + lookAroundMissionClear);
            return;
        }
        if (headTurn == ScDefine.ScHeadTurn.Left)
        {
            checkLookLeft = true;
        }
        else if (headTurn == ScDefine.ScHeadTurn.Right && checkLookLeft == true)
        {
            checkLookRight = true;
        }
    }
}
