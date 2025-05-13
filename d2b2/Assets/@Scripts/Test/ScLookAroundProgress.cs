using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScLookAroundProgress : MonoBehaviour
{
    [SerializeField] private Image progressImage;
    public Action<float,float> onProgress;

    private void Awake()
    {
        onProgress += DrawLookArounProgress;
    }
    private void DrawLookArounProgress(float time, float maxTime)
    {
        progressImage.fillAmount = Mathf.Clamp01(time / maxTime);
    }
}
