using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ScSettingSlider : MonoBehaviour
{
    [SerializeField] private string stringFormat = "F0";
    public Slider slider;

    [SerializeField] TMP_Text valueText;
    [SerializeField] private bool displayString;
    [SerializeField] private float multiplyValue;
    [SerializeField] private string followingLetter;
    [SerializeField] private string[] displayNames;
    //public List<string> displayNames;


    public float Value
    {
        get; private set;
    }
    private void Awake()
    {
        Value = slider.value;
        ChangeValue();
    }

    public void SetSliderValue(float value)
    {
        slider.value = value;
        ChangeValue();
    }

    public void ChangeValue()
    {
        Value = slider.value;
        // Debug.Log("V " + Value);
        if(displayString)
        {
            valueText.text = displayNames[(int)Value];
        }
        else
        {
            valueText.text = (slider.value * multiplyValue).ToString(stringFormat) + followingLetter;
        }
    }
}
