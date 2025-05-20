using System.Collections;
using UnityEngine;

public class ScSafetyLineCtrl : MonoBehaviour
{
    [SerializeField] private float limitTime = 1f;

    bool isSafety = false;

    Coroutine coroutine = null;
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.name);
        //Debug.Log("layer : " + other.gameObject.layer);
        //Debug.Log(ScDefine.Layer.PlayerIndex);

        if (other.gameObject.layer == ScDefine.Layer.PlayerIndex)
        {
           // Debug.Log("������ ����.");

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

        //Debug.Log("������ ���.");
    }

    IEnumerator TimeLimit(Collider other)
    {

        yield return new WaitForSeconds(limitTime);

        if (isSafety)
        {
            Debug.Log("Sg01_SafetyLine");
            ScRespawn.Instance.Respawn(true); 
            Manager.Instance.SceneMgr.LoadScene(ScDefine.ScScene.Sg01_SafetyLine);
        }
    }
}
