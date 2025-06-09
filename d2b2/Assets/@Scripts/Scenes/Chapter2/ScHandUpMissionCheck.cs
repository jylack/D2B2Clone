using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScHandUpMissionCheck : MonoBehaviour
{
    [SerializeField] private ScHandUpRegion handUpResion;
    [SerializeField] private UnityEvent missonClearEvent;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == ScDefine.Layer.PlayerIndex)
        {
            if (handUpResion.handUpMissionClear == true)
            {
                missonClearEvent?.Invoke();
            }
        }
    }

}
