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
    public bool isLeftHandUp { get; private set; }
    private Coroutine handDownCor;
    [SerializeField] private float handDownTime;
    private bool inFirstHandUpResion;
    private void Start()
    {
        handDownTime = 2;
        isLeftHandUp =false;
        handUpMissionClear = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        inFirstHandUpResion = true;
        if (other.gameObject.layer == ScDefine.Layer.PlayerIndex)
        {
            UIPlayerHsy.Instance.OnHandUpProgressUI();
            Manager.Instance.GameMgr.OnPlayerHandsUp += HandUpMission;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == ScDefine.Layer.PlayerIndex)
        {
            ExitHandUpRegion();
        }
    }
    public void HandUpMission(bool leftHandUp, bool rightHandUp, float distance)
    {
        Debug.Log("1 : " + leftHandUp);
        Debug.Log("2 : " + isLeftHandUp);
        if (leftHandUp != isLeftHandUp || inFirstHandUpResion == true)
        {
            inFirstHandUpResion = false;
            isLeftHandUp = leftHandUp;
            if (isLeftHandUp == true)
            {
                Debug.Log("시작");
                handDownCor = StartCoroutine(HandDownCoolDown());
            }
            else
            {
                Debug.Log("끝");
                if (handDownCor != null)
                {
                    StopCoroutine(handDownCor);
                }
            }
            UIPlayerHsy.Instance.handUpText.text = leftHandUp.ToString();
        }
        UIPlayerHsy.Instance.DrawHandUpProgress(distance);
    }
    IEnumerator HandDownCoolDown()
    {
        Debug.Log("시작");
        float timer = 0;
        while (timer < handDownTime)
        {
            timer += Time.deltaTime;
            yield return null; 
        }
        // 미션 실패
        Debug.Log("_____________ : " + handUpMissionClear);
        handUpMissionClear = false;
    }
    public void ExitHandUpRegion()
    {
        UIPlayerHsy.Instance.OffHandUpProgressUI();
        Manager.Instance.GameMgr.OnPlayerHandsUp -= HandUpMission;
        if (handUpMissionClear == true)
        {
            // 미션 성공 ui
        }
    }
}
