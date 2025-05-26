using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ScSettingSlider))]
public class ScSettingSliderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.Space(20f);

        ScSettingSlider settingSlider = (ScSettingSlider)target;

        EditorGUILayout.LabelField("Slider Values", EditorStyles.boldLabel);
        ++EditorGUI.indentLevel;
        EditorGUILayout.LabelField("Integer Value");
        settingSlider.slider.wholeNumbers = EditorGUILayout.Toggle(settingSlider.slider.wholeNumbers);

        EditorGUILayout.LabelField("Min Value");
        settingSlider.slider.minValue = EditorGUILayout.FloatField(settingSlider.slider.minValue);

        EditorGUI.BeginChangeCheck();
        EditorGUILayout.LabelField("Max Value");
        settingSlider.slider.maxValue = EditorGUILayout.FloatField(settingSlider.slider.maxValue);
        if (EditorGUI.EndChangeCheck())
        {
            var tempArr = settingSlider.displayNames;
            settingSlider.displayNames = new string[(int)settingSlider.slider.maxValue];
            //settingSlider.displayNames = new List<string>((int)settingSlider.slider.maxValue);
            for(int i = 0; i < tempArr.Length && i < (int)settingSlider.slider.maxValue; i++)
            {
                settingSlider.displayNames[i] = tempArr[i];
            }
        }
        --EditorGUI.indentLevel;
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Display Values", EditorStyles.boldLabel);
        ++EditorGUI.indentLevel;
        EditorGUILayout.LabelField("Do Multiply Value");
        settingSlider.multiply = EditorGUILayout.Toggle(settingSlider.multiply);
        if (settingSlider.multiply)
        {
            EditorGUILayout.LabelField("Multiply Value");
            settingSlider.multiplyValue = EditorGUILayout.FloatField(settingSlider.multiplyValue);
        }
        else
        {
            settingSlider.multiplyValue = 1;
        }

        if (settingSlider.slider.wholeNumbers)
        {
            EditorGUILayout.LabelField("String Display");
            settingSlider.displayString = EditorGUILayout.Toggle(settingSlider.displayString);

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("displayNames"), true);
            if(EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
            }
        }
        else
        {
            settingSlider.displayString = false;
        }
        --EditorGUI.indentLevel;
        EditorGUILayout.Space();

        //EditorGUI.BeginChangeCheck();
    }
}
