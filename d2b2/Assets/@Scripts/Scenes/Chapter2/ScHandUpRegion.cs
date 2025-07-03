using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ScHandUpRegion : MonoBehaviour
{
    public bool handUpMissionClear { get; private set; }
    public bool isLeftHandUp { get; private set; }
    private Coroutine handDownCor;
    [SerializeField] private float handDownTime;
    [SerializeField] private UnityEvent handUpFailEvent;
    private bool inFirstHandUpRegion;
    public bool inHandUpRegion { get; private set; }

    private void Start()
    {
        handDownTime = 2;
        isLeftHandUp =false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == ScDefine.Layer.PlayerIndex)
        {
            inHandUpRegion = true;
            handUpMissionClear = true;
            inFirstHandUpRegion = true;
            UIPlayerHsy.Instance.OnHandUpProgressUI();
            Manager.Instance.GameMgr.OnPlayerHandsUp += HandUpMission;
        }
    }
    private void OnDestroy()
    {
        ExitHandUpRegion();
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == ScDefine.Layer.PlayerIndex)
        {
            inHandUpRegion = false;
            ExitHandUpRegion();
        }
    }
    public void StopCurrentCor()
    {
        if (handDownCor != null)
        {
            StopCoroutine(handDownCor);
        }
    }
    public void HandUpMission(bool leftHandUp, bool rightHandUp, float distance)
    {
        if (leftHandUp != isLeftHandUp || inFirstHandUpRegion == true)
        {
            inFirstHandUpRegion = false;
            isLeftHandUp = leftHandUp;
            if (isLeftHandUp == false)
            {
                handDownCor = StartCoroutine(HandDownCoolDown());
            }
            else
            {
                StopCurrentCor();
            }
        }
        UIPlayerHsy.Instance.DrawHandUpProgress(distance);
    }
    IEnumerator HandDownCoolDown()
    {
        float timer = 0;
        while (timer < handDownTime)
        {
            timer += Time.deltaTime;
            yield return null; 
        }
        handUpMissionClear = false;
        handUpFailEvent.Invoke();
    }
    public void ExitHandUpRegion()
    {
        StopCurrentCor();
        if (UIPlayerHsy.Instance != null)
        {
            UIPlayerHsy.Instance.OffHandUpProgressUI();
        }
        Manager.Instance.GameMgr.OnPlayerHandsUp -= HandUpMission;
    }
    public void ShowHandUpGuide()
    {
        Manager.Instance.SceneMgr.LoadScene(ScDefine.ScScene.Sg03_HandUp);
    }
}
