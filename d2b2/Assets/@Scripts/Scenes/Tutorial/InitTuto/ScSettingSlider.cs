using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScSettingSlider : MonoBehaviour
{
    [SerializeField] string stringFormat = "F0";
    [SerializeField] Slider slider;
    [SerializeField] TMP_Text valueText;
    public Action<float> OnValueChanged;

    public float Value
    {
        get; private set;
    }

    public void SetSliderValue(float value)
    {
        slider.value = value;
        ChangeValue();
    }

    public void ChangeValue()
    {
        Value = slider.value;
        valueText.text = (slider.value * 100).ToString(stringFormat);
        OnValueChanged?.Invoke(Value);
    }
}
