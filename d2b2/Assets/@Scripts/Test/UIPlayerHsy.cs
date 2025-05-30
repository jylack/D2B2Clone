using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class UIPlayerHsy : MonoBehaviour
{
    public static UIPlayerHsy Instance { get; private set; }

    [SerializeField] private GameObject lookAroundLeftProgressUi;
    [SerializeField] private GameObject lookAroundRightProgressUi;
    [SerializeField] private GameObject handUpProgressUI;
    [SerializeField] private Image handUpProgress;
    [SerializeField] private Image lookAroundLeftProgress;
    [SerializeField] private Image lookAroundRightProgress;
    [SerializeField] public TextMeshProUGUI handUpText;
    [SerializeField] public Image explanationUI;
    [SerializeField] public TextMeshProUGUI explanationText;
    [SerializeField] public Image sideLeftScreenImage;
    [SerializeField] public Image sideRightScreenImage;
    [SerializeField] public TextMeshProUGUI countDownText;

    private void Awake()
    {
        Instance = this;
    }
    public void DrawLookArounProgress(float time, float maxTime, ScDefine.ScHeadTurn headDirection)
    {
        if (headDirection == ScDefine.ScHeadTurn.Left)
        {
            lookAroundLeftProgress.fillAmount = Mathf.Clamp01(time / maxTime);
        }
        else if (headDirection == ScDefine.ScHeadTurn.Right)
        {
            lookAroundRightProgress.fillAmount = Mathf.Clamp01(time / maxTime);
        }
    }
    public void DrawHandUpProgress(float distance)
    {
        handUpProgress.fillAmount = distance;

    }
    public void OnLookAroundLeftProgress()
    {
        lookAroundLeftProgressUi.SetActive(true);
    }
    public void OnLookAroundRightProgress()
    {
        lookAroundRightProgressUi.SetActive(true);
    }
    public void OnHandUpProgressUI()
    {
        handUpProgressUI.SetActive(true);
    }
    public void OffLookAroundLeftProgress()
    {
        lookAroundLeftProgressUi.SetActive(false);
    }
    public void OffLookAroundRightProgress()
    {
        lookAroundRightProgressUi.SetActive(false);
    }
    public void OffHandUpProgressUI()
    {
        if (handUpProgressUI == null)
        {
            return;
        }
        handUpProgressUI.SetActive(false);
    }
    public void ChangeText(string text)
    {
        explanationText.text = text;
    }

    public IEnumerator SideFillProduction(float start, float end, float duration)
    {
        float time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;
            float value = Mathf.Lerp(start, end, t);
            sideLeftScreenImage.fillAmount = value;
            sideRightScreenImage.fillAmount = value;
            yield return null;
        }
        sideLeftScreenImage.fillAmount = end;
        sideRightScreenImage.fillAmount = end;
    }

}
