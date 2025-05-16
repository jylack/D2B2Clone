using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondStageManager : MonoBehaviour
{
    public static SecondStageManager Instance { get; private set; }
    public bool lookAroundRegionClear;
    public bool handUpRegionClear;
    [SerializeField] private bool secondStageClear;
    [SerializeField] private GameObject Npc;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        ResetSecondStage();
    }

    public void ResetSecondStage()
    {
        secondStageClear = false;
        lookAroundRegionClear = false;
        handUpRegionClear = false;
    }
    public void SecondStageClear()
    {
        secondStageClear = true;
    }
}
