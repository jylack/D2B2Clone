using System.Collections;
using UnityEngine;

public class ScCrossWalkCtrl : MonoBehaviour
{
    [SerializeField] private float limitTime = 1f;

    bool isWalk = false;

    Coroutine coroutine = null;


    private void OnTriggerEnter(Collider other)
    {
        Manager.Instance.GameMgr.OnPlayerMoving += OnPlayerMoving;
    }
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("È¾´Üº¸µµ ÀÌµ¿Áß.");

        if (isWalk == false)
        {
            if (coroutine == null)
                coroutine = StartCoroutine(TimeLimit(other));
        }
        else
        {
            if (coroutine != null)
            {
                StopAllCoroutines();
                coroutine = null;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("È¾´Üº¸µµ ¹þ¾î³²");
        isWalk = false;
        Manager.Instance.GameMgr.OnPlayerMoving -= OnPlayerMoving;
    }
    IEnumerator TimeLimit(Collider other)
    {

        yield return new WaitForSeconds(limitTime);

        if (isWalk == false)
        {
            other.gameObject.GetComponent<ScRespawn>().Respawn();
        }
    }

    private void OnPlayerMoving(bool isMoving)
    {
        isWalk = true;
    }
}
