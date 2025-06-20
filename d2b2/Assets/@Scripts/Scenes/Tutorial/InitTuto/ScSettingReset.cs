using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScSettingReset : MonoBehaviour
{
    [SerializeField] private Slider[] sliders;

    public void ResetSettings()
    {
        foreach(var slider in sliders)
        {
            slider.value = slider.maxValue;
        }
    }
}
