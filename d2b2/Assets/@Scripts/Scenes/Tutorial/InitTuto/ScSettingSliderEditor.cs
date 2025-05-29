using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//[CustomEditor(typeof(ScSettingSlider))]
public class ScSettingSliderEditor// : Editor
{
    /*public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.Space(20f);
        
        ScSettingSlider settingSlider = (ScSettingSlider)target;

        EditorGUILayout.LabelField("Slider Values", EditorStyles.boldLabel);
        ++EditorGUI.indentLevel;
        EditorGUILayout.LabelField("Integer Value");
        settingSlider.slider.wholeNumbers = EditorGUILayout.Toggle(settingSlider.slider.wholeNumbers);

        EditorGUILayout.LabelField("Min Value");
        Debug.Log("a " + settingSlider.slider.minValue);
        float temp = EditorGUILayout.FloatField(settingSlider.slider.minValue);

        if (temp > 0)
        {
            settingSlider.slider.minValue = temp;
        }

        if (temp == 0)
        {
            Debug.Log("");
        }

        EditorGUILayout.LabelField("Max Value");
        settingSlider.slider.maxValue = EditorGUILayout.FloatField(settingSlider.slider.maxValue);
        var tempArr = settingSlider.displayNames;
        settingSlider.displayNames = new string[(int)settingSlider.slider.maxValue];
        //settingSlider.displayNames = new List<string>((int)settingSlider.slider.maxValue);
        for (int i = 0; i < tempArr.Length && i < (int)settingSlider.slider.maxValue; i++)
        {
            settingSlider.displayNames[i] = tempArr[i];
        }
        --EditorGUI.indentLevel;
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Display Values", EditorStyles.boldLabel);
        ++EditorGUI.indentLevel;

        EditorGUILayout.LabelField("Following Letter");
        settingSlider.followingLetter = EditorGUILayout.TextArea(settingSlider.followingLetter);

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

            if (settingSlider.displayString)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("displayNames"), true);
            }
        }
        else
        {
            settingSlider.displayString = false;
        }
        --EditorGUI.indentLevel;
        EditorGUILayout.Space();
        if (EditorGUI.EndChangeCheck())
        {
            serializedObject.ApplyModifiedProperties();
        }
        //EditorGUI.BeginChangeCheck();
    }*/
}
