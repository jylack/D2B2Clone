using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScBlindSpotResion : MonoBehaviour
{
    enum DangerZoneType {BlindSpot,HeadLightCar }
    [SerializeField] DangerZoneType dangerZoneType;
    [SerializeField] public bool isEnterBlindSpot { get; private set; }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == ScDefine.Layer.PlayerIndex)
        {
            if (dangerZoneType == DangerZoneType.BlindSpot)
            {
                // 안전가이드
                Manager.Instance.SceneMgr.LoadScene(ScDefine.ScScene.Ch2BlindSpot);
            }
            else if (dangerZoneType == DangerZoneType.HeadLightCar)
            {
                // 안전가이드
            }
        }
    }

}
