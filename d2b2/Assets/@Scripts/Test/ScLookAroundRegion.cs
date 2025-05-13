using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ScLookAroundRegion : MonoBehaviour
{
    private bool checkLookLeft;
    private bool checkLookRight;
    private bool lookAroundMissionClear;
    [SerializeField] private float maxTime = 10;
    ScLookAroundProgress lookAroundLeftProgress;
    ScLookAroundProgress lookAroundRightProgress;
    Coroutine lookLeftTimeCor;
    Coroutine lookRightTimeCor;

    private void Start()
    {
        lookAroundLeftProgress = UIPlayerHsy.Instance.GetLookAroundLeftComponent();
        lookAroundRightProgress = UIPlayerHsy.Instance.GetLookAroundRightComponent();
    }
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
        Debug.Log("headTurn : " + headTurn);
        if (checkLookLeft == true && checkLookRight == true)
        {
            lookAroundMissionClear = true;
            return;
        }
        if (headTurn == ScDefine.ScHeadTurn.Left)
        {
            UIPlayerHsy.Instance.OnLookAroundLeftProgress();
            if (lookLeftTimeCor == null)
            {
                lookLeftTimeCor = StartCoroutine(CheckHeadLeftStayTime());
            }
        }
        if (headTurn == ScDefine.ScHeadTurn.Right && checkLookLeft == true)
        {
            UIPlayerHsy.Instance.OnLookAroundRightProgress();
            if (lookRightTimeCor == null)
            {
                lookRightTimeCor = StartCoroutine(CheckHeadRightStayTime());
            }
        }
        if (headTurn == ScDefine.ScHeadTurn.Forward)
        {
            UIPlayerHsy.Instance.OffLookAroundLeftProgress();
            UIPlayerHsy.Instance.OffLookAroundRightProgress();
            if(lookLeftTimeCor != null)
            {
                StopCoroutine(lookLeftTimeCor);
                lookLeftTimeCor = null;
            }
            if (lookRightTimeCor != null)
            {
                StopCoroutine(lookRightTimeCor);
                lookRightTimeCor = null;
            }
        }
    }

    IEnumerator CheckHeadLeftStayTime()
    {
        float currentTime = 0f;
        while (currentTime < maxTime)
        {
            currentTime += Time.deltaTime;
            lookAroundLeftProgress.onProgress?.Invoke(currentTime, maxTime);
            yield return null;
        }
        checkLookLeft = true;
        Debug.Log("왼쪽완료");
    }
    IEnumerator CheckHeadRightStayTime()
    {
        float currentTime = 0f;
        while (currentTime < maxTime)
        {
            currentTime += Time.deltaTime;
            lookAroundRightProgress.onProgress?.Invoke(currentTime, maxTime);
            yield return null;
        }
        checkLookRight = true;
        Debug.Log("오른쪽완료");
    }
}
