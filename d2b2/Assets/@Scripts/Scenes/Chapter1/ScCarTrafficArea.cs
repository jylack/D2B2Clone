using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class ScCarTrafficArea : MonoBehaviour
{
    private ScTrafficCtrl scTrafficCtrl;

    private void Awake()
    {
        scTrafficCtrl = transform.parent.GetComponent<ScTrafficCtrl>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == ScDefine.Layer.CarIndex)
        {
            if (scTrafficCtrl.CurrentColor == TrafficLightColor.Green)
            {
                other.gameObject.GetComponent<ScCarController>().StopCar();
            }
        }
    }
}
