using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScMoveGoal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == ScDefine.Layer.PlayerIndex)
        {
            Debug.Log("도착했어 태초마을이야");
        }
    }
}
