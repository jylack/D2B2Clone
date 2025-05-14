using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondStageManager : MonoBehaviour
{
    public static SecondStageManager Instance { get; private set; }
    public bool lookAroundRegionClear;
    public bool handUpRegionClear;

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
        lookAroundRegionClear = false;
        handUpRegionClear = false;
    }

}
