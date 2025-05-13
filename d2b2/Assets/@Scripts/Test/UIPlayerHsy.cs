using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerUiType
{
    LookLeft,LookRight,HandUp
}
public class UIPlayerHsy : MonoBehaviour
{
    public static UIPlayerHsy Instance { get; private set; }

    [SerializeField] private GameObject lookAroundLeftProgress;
    [SerializeField] private GameObject lookAroundRightProgress;
    [SerializeField] private GameObject handUpProgressUI;

    private void Awake()
    {
        Instance = this;
    }

    public ScLookAroundProgress GetLookAroundLeftComponent()
    {
       return lookAroundLeftProgress.GetComponent<ScLookAroundProgress>();
    }
    public ScLookAroundProgress GetLookAroundRightComponent()
    {
        return lookAroundRightProgress.GetComponent<ScLookAroundProgress>();
    }
    public ScHandUpProgress GetHandUpComponent()
    {
        return handUpProgressUI.GetComponent<ScHandUpProgress>();
    }
    public void OnLookAroundLeftProgress()
    {
        lookAroundLeftProgress.SetActive(true);
    }
    public void OnLookAroundRightProgress()
    {
        lookAroundRightProgress.SetActive(true);
    }
    public void OnHandUpProgressUI()
    {
        handUpProgressUI.SetActive(true);
    }
    public void OffLookAroundLeftProgress()
    {
        lookAroundLeftProgress.SetActive(false);
    }
    public void OffLookAroundRightProgress()
    {
        lookAroundRightProgress.SetActive(false);
    }
    public void OffHandUpProgressUI()
    {
        handUpProgressUI.SetActive(false);
    }


}
