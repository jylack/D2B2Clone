using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScHSYTEST : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer ==ScDefine.Layer.PlayerIndex )
        {
            other.transform.position = Vector3.zero;
        }
    }
    private void Start()
    {
        StartCoroutine(tets());
    }
    IEnumerator tets()
    {
        yield return new WaitForSeconds(2);
        Manager.Instance.SceneMgr.LoadPreviousScene();
    }
}
