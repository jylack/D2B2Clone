using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class ScGoalPointHsy : MonoBehaviour
{
    [SerializeField] private UnityEvent goalEvent;
     private Vector3 arrowMinPos;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == ScDefine.Layer.PlayerIndex)
        {
            goalEvent?.Invoke();
        }
    }
}
