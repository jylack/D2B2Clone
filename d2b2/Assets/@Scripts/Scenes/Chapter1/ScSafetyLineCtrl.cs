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
           // Debug.Log("안전선 밟음.");

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

        //Debug.Log("안전선 벗어남.");
    }

    IEnumerator TimeLimit(Collider other)
    {

        yield return new WaitForSeconds(limitTime);

        if (isSafety)
        {
            Debug.Log("safe");
            other.gameObject.GetComponent<ScRespawn>().Respawn(true);
        }
    }
}
