using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScGoalPointHsy : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == ScDefine.Layer.PlayerIndex)
        {
            SecondStageManager.Instance.SecondStageClear();
            Manager.Instance.SceneMgr.LoadScene(ScDefine.ScScene.Ch2Login);
                
        }
    }
}
