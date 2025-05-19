using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
        handUpProgressUI.SetActive(false);
    }
    public void ChangeText(string text)
    {
        explanationText.text = text;
    }

}
