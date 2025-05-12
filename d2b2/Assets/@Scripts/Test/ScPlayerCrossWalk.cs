using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScPlayerCrossWalk : MonoBehaviour
{
    int CurrentStep;

    [SerializeField] private ScTrafficCtrl[] scTrafficCtrl;

    private void Start()
    {
        CurrentStep = Manager.Instance.GameMgr.CurrentStep;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == ScDefine.Layer.PlayerIndex)
        {
            if(CurrentStep < scTrafficCtrl.Length )
            {
                if(scTrafficCtrl[CurrentStep].GetCurrentColor() == TrafficLightColor.Red)
                {
                    var temp = other.gameObject.GetComponent<ScRespawn>();
                    temp.Init(CurrentStep);
                    Debug.Log("횡단보도 이동중 빨간불임.");
                    temp.Respawn();
                }
            }
        }
    }

    
}
