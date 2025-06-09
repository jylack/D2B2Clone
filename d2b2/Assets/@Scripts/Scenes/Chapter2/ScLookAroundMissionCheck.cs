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
                UIPlayerHsy.Instance.explanationUI.gameObject.SetActive(true);
                UIPlayerHsy.Instance.explanationText.text = "LookAroundFail";
                StartCoroutine(WaitExplanationMessage());
            }
            else
            {
                check.gameObject.SetActive(false);
            }
            check.ExitMissionRegion();
        }
    }
    IEnumerator WaitExplanationMessage()
    {
        yield return new WaitForSeconds(2);
        UIPlayerHsy.Instance.explanationUI.gameObject.SetActive(false);
        Manager.Instance.SceneMgr.LoadScene(ScDefine.ScScene.Sg02_LookAround);
    }
}
