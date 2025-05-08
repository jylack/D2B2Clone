using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ScLookAroundResion : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI successText;
    [SerializeField] TextMeshProUGUI lookRight;
    [SerializeField] TextMeshProUGUI lookLeft;
    [SerializeField] TextMeshProUGUI angleText;
    ScPlayerCtrl scPlayerCtrl;
    bool isSuccess;
    private void Start()
    {
        isSuccess = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ScPlayerCtrl>() == null)
        {
            return;
        }
            scPlayerCtrl = other.GetComponent<ScPlayerCtrl>();
            scPlayerCtrl.coroutine = StartCoroutine(scPlayerCtrl.ChekLookAround());
    }
    private void OnTriggerExit(Collider other)
    {
        if (scPlayerCtrl.coroutine != null)
        {
            StopCoroutine(scPlayerCtrl.coroutine);
        }
        else
        {
            Debug.Log("scPlayerCtrl.coroutine == null");
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (scPlayerCtrl.isLookAround == true)
        {
            isSuccess = true;
        }
        successText.text = "Success : " + scPlayerCtrl.isLookAround;
        angleText.text = "angle : " + scPlayerCtrl.currentLookAngle;
        lookRight.text = "LookRight : " + scPlayerCtrl.isLookRight;
        lookLeft.text = "LookLeft : " + scPlayerCtrl.isLookLeft;
    }
}
