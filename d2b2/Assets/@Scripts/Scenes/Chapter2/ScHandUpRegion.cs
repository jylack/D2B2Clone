using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ScHandUpRegion : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
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
        inHandUpRegion = true;
        handUpMissionClear = true;
        inFirstHandUpRegion = true;
        if (other.gameObject.layer == ScDefine.Layer.PlayerIndex)
        {
            UIPlayerHsy.Instance.OnHandUpProgressUI();
            Manager.Instance.GameMgr.OnPlayerHandsUp += HandUpMission;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        inHandUpRegion = false;
        if (other.gameObject.layer == ScDefine.Layer.PlayerIndex)
        {
            ExitHandUpRegion();
        }
        if (handUpMissionClear == true)
        {
            // 미션 성공 ui
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
        float timer = 0;
        while (timer < handDownTime)
        {
            timer += Time.deltaTime;
            yield return null; 
        }
        // 미션 실패
        handUpMissionClear = false;
        if (handUpMissionClear == false)
        {
            handUpFailEvent.Invoke();
        }
    }
    public void ExitHandUpRegion()
    {
        UIPlayerHsy.Instance.OffHandUpProgressUI();
        Manager.Instance.GameMgr.OnPlayerHandsUp -= HandUpMission;
    }
    public void ShowHandUpGuide()
    {
        Manager.Instance.SceneMgr.LoadScene(ScDefine.ScScene.HandUpGuide);
    }
}
