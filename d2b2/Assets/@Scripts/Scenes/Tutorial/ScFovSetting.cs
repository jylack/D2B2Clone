using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScFovSetting : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] Camera targetCam;

    public void ChangePov()
    {
        targetCam.fieldOfView = slider.value * 10;
    }
}
