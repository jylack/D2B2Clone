using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScCh2Check : MonoBehaviour
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
                // 씬이동 - 고개 돌리기
            }
        }
    }
    IEnumerator WaitExplanationMessage()
    {
        yield return new WaitForSeconds(2);
        UIPlayerHsy.Instance.explanationUI.gameObject.SetActive(false);
    }
}
