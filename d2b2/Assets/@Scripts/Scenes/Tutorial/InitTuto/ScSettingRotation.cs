using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class ScSettingRotation : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdown;
    [SerializeField] private ScSettingSlider snapRot;
    [SerializeField] private ScSettingSlider continuousRot;
    [SerializeField] private XRInputModalityManager imm;
    [SerializeField] private ActionBasedControllerManager abcm;
    [SerializeField] private SnapTurnProviderBase snap;
    [SerializeField] private ContinuousTurnProviderBase cont;
    [SerializeField] private List<string> optionIds;

    private void Awake()
    {
        //Debug.Log(GameObject.Find("Right Controller").name);
        //abcm = GameObject.Find("Camera Offset").transform.GetChild(5).GetComponent<ActionBasedControllerManager>();
        imm = FindAnyObjectByType<XRInputModalityManager>();
        Debug.Log(imm.name);
        imm?.rightController.TryGetComponent(out abcm);
        GameObject turn = GameObject.Find("Turn");
        snap = FindAnyObjectByType<ActionBasedSnapTurnProvider>();
        cont = FindAnyObjectByType<ActionBasedContinuousTurnProvider>();
        RotationModeChange();
        gameObject.SetActive(false);
        SetDropdownTexts();
    }

    public void RotationModeChange()
    {
        switch(dropdown.value)
        {
            case 0:
                snapRot.gameObject.SetActive(false);
                continuousRot.gameObject.SetActive(false);
                abcm.smoothTurnEnabled = false;
                snap.turnAmount = 0;
                break;
            case 1:
                snapRot.gameObject.SetActive(true);
                continuousRot.gameObject.SetActive(false);
                abcm.smoothTurnEnabled = false;
                SnapAmountChange();
                break;
            case 2:
                snapRot.gameObject.SetActive(false);
                continuousRot.gameObject.SetActive(true);
                abcm.smoothTurnEnabled = true;
                ContinousAmountChange();
                break;
        }
    }

    public async UniTaskVoid SetDropdownTexts()
    {
        try
        {
            for (int i = 0; i < dropdown.options.Count; i++)
            {
                string txt = await Manager.Instance.LanguageMgr.GetTextAsync(optionIds[i]);
                dropdown.options[i].text = txt;
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }

    public void SnapAmountChange()
    {
        Debug.Log("va " + snapRot.Value);
        snap.turnAmount = snapRot.Value * 15;
    }
    public void ContinousAmountChange()
    {
        cont.turnSpeed = continuousRot.Value * 10;
    }
}
