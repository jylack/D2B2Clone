using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScBlindSpotResion : MonoBehaviour
{
    [SerializeField] private bool isEnterBlindSpot;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == ScDefine.Layer.PlayerIndex)
        {
            isEnterBlindSpot = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        isEnterBlindSpot = false;
    }
    // 사각지대 안전 가이드 실행
}
