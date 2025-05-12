using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScHandUpResion : MonoBehaviour
{
    //bool isSuccess;
    ScPlayerCtrl scPlayerCtrl;
    [SerializeField] TextMeshProUGUI rightHandText;
    [SerializeField] TextMeshProUGUI leftHandText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ScPlayerCtrl>() == null)
        {
            return;
        }
        scPlayerCtrl = other.GetComponent<ScPlayerCtrl>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (scPlayerCtrl.isHandUp == true)
        {
           // isSuccess = true;
        }
        rightHandText.text = "RightHandUp : " + scPlayerCtrl.isRightHandUp;
        leftHandText.text = "LeftHandUp : " + scPlayerCtrl.isLeftHandUp;
    }
    private void OnTriggerExit(Collider other)
    {
        rightHandText.text = "";
        leftHandText.text = "";
    }
}
