using System.Collections;
using UnityEngine;

public class ScSafetyLineCtrl : MonoBehaviour
{
    [SerializeField] private float limitTime = 1f;

    bool isSafety = false;

    Coroutine coroutine = null;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == ScDefine.Layer.PlayerIndex)
        {
            isSafety = true;

            if (coroutine == null)
            {
                coroutine = StartCoroutine(TimeLimit(other));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isSafety = false;
        StopAllCoroutines();
        coroutine = null;
    }

    IEnumerator TimeLimit(Collider other)
    {

        yield return new WaitForSeconds(limitTime);

        if (isSafety)
        {
            ScRespawn.Instance.Respawn(); 
            Manager.Instance.SceneMgr.LoadScene(ScDefine.ScScene.Sg01_SafetyLine);
        }
    }
}
