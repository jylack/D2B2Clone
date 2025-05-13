using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScHandUpProgress : MonoBehaviour
{
    [SerializeField] private Image progress; // 전체 게이지
    [SerializeField] private float maxDistance = 1f; // 50cm 기준
    public Action<float,float> onProgress;

    private void Start()
    {
        onProgress += test;
    }

    public void test(float headY , float handY)
    {
        var HeadHandDistance = handY - headY;
        progress.fillAmount = Mathf.InverseLerp(-maxDistance, maxDistance, HeadHandDistance);
    }
}

