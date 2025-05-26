using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScSettingSlider : MonoBehaviour
{
    [SerializeField] private string stringFormat = "F0";
    public Slider slider;
    [SerializeField] TMP_Text valueText;
    public Action<float> OnValueChanged;
    [HideInInspector] public bool displayString;
    [HideInInspector] public bool multiply;
    [HideInInspector] public float multiplyValue;
    [HideInInspector] public string[] displayNames;
    //public List<string> displayNames;

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
        valueText.text = (slider.value * multiplyValue).ToString(stringFormat);
        OnValueChanged?.Invoke(Value);
    }
}
