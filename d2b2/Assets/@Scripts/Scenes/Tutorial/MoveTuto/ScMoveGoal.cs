using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScMoveGoal : MonoBehaviour
{
    [SerializeField] MoveTutorialManager moveManager;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == ScDefine.Layer.PlayerIndex)
        {
            moveManager.WalkSuccess();
        }
    }
}
