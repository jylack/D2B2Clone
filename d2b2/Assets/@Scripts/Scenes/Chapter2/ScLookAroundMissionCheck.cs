using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks.Triggers;
using UnityEngine;
using UnityEngine.Events;

public class ScLookAroundMissionCheck : MonoBehaviour
{
    [SerializeField] private ScLookAroundRegion check;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == ScDefine.Layer.PlayerIndex)
        {
            if (check.lookAroundMissionClear == false)
            {
                Manager.Instance.SceneMgr.LoadScene(ScDefine.ScScene.Sg02_LookAround);
            }
            else
            {
                check.gameObject.SetActive(false);
            }
            check.ExitMissionRegion();
        }
    }
}
