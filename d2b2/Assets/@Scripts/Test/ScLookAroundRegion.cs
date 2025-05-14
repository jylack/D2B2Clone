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
    public bool lookAroundMissionClear { get; private set; }
    [SerializeField] private float completeTime = 1;
    private ScLookAroundProgress lookAroundLeftProgress;
    private ScLookAroundProgress lookAroundRightProgress;
    private Coroutine lookCor;

    private void Start()
    {
        lookAroundMissionClear = false;
        lookAroundLeftProgress = UIPlayerHsy.Instance.GetLookAroundLeftComponent();
        lookAroundRightProgress = UIPlayerHsy.Instance.GetLookAroundRightComponent();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == ScDefine.Layer.PlayerIndex)
        {
            checkLookLeft = false;
            checkLookRight = false;
            lookAroundMissionClear = false ;
            Manager.Instance.GameMgr.OnPlayerHeadTurn += CheckPlayerHeadTurn;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == ScDefine.Layer.PlayerIndex)
        {
            Manager.Instance.GameMgr.OnPlayerHeadTurn -= CheckPlayerHeadTurn;
            StopCurrentCoroutine();
            OffAllUI();
        }
    }

    private void CheckPlayerHeadTurn(ScDefine.ScHeadTurn headDirection)
    {
        if (lookAroundMissionClear == true) return;
        Debug.Log(1);
        if (headDirection == ScDefine.ScHeadTurn.Left && checkLookLeft == false)
        {
            StartLooking(headDirection);
        }
        else if (headDirection == ScDefine.ScHeadTurn.Right && checkLookLeft == true && checkLookRight == false)
        {
            StartLooking(headDirection);
        }
        else if (headDirection == ScDefine.ScHeadTurn.Forward)
        {
            StopCurrentCoroutine();
            //OffAllUI();
        }
    }

    private void StartLooking(ScDefine.ScHeadTurn headDirection)
    {
        Debug.Log(2);
        StopCurrentCoroutine();
        ScLookAroundProgress progress = null;
        if (headDirection == ScDefine.ScHeadTurn.Left)
        {
            UIPlayerHsy.Instance.OnLookAroundLeftProgress();
            progress = lookAroundLeftProgress;
        }
        else if (headDirection == ScDefine.ScHeadTurn.Right)
        {
            UIPlayerHsy.Instance.OnLookAroundRightProgress();
            progress = lookAroundRightProgress;
        }
        if (progress != null)
        {
            lookCor = StartCoroutine(CheckHeadStayTime(headDirection, progress));
        }
    }

    private IEnumerator CheckHeadStayTime(ScDefine.ScHeadTurn headDirection, ScLookAroundProgress progress)
    {
        float timer = 0f;
        while (timer < completeTime)
        {
            Debug.Log(3);
            timer += Time.deltaTime;
            progress.onProgress?.Invoke(timer, completeTime);
            yield return null;
        }

        if (headDirection == ScDefine.ScHeadTurn.Left)
        {
            checkLookLeft = true;
            Debug.Log("왼쪽 완료");
        }
        else
        {
            checkLookRight = true;
            lookAroundMissionClear = true;
            Debug.Log("오른쪽 완료");
        }

        lookCor = null;

        if (lookAroundMissionClear)
        {
            Debug.Log("미션 성공!");
            OffAllUI();
        }
    }

    private void StopCurrentCoroutine()
    {
        if (lookCor != null)
        {
            StopCoroutine(lookCor);
            lookCor = null;
        }
    }

    private void OffAllUI()
    {
        UIPlayerHsy.Instance.OffLookAroundLeftProgress();
        UIPlayerHsy.Instance.OffLookAroundRightProgress();
    }
}
