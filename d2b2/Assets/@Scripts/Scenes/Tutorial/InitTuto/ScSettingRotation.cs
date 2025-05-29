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
    [SerializeField] private ActionBasedControllerManager abcm;
    [SerializeField] private SnapTurnProviderBase snap;
    [SerializeField] private ContinuousTurnProviderBase cont;

    private void Start()
    {
        //씬뷰에서는 Right Controller로 나와있으나 Find에는 붙혀놔야됨
        Debug.Log(GameObject.Find("Camera Offset").name);
        abcm = GameObject.Find("Camera Offset").transform.GetChild(5).GetComponent<ActionBasedControllerManager>(); 
        GameObject turn = GameObject.Find("Turn");
        snap = turn.GetComponent<SnapTurnProviderBase>();
        cont = turn.GetComponent<ContinuousTurnProviderBase>();
        RotationModeChange();
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
