using System.Data.Common;
using UnityEngine;

public class ScDeadZoneCtrl : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == ScDefine.Layer.PlayerIndex)
        {
            other.gameObject.GetComponent<ScRespawn>().Respawn();
            Debug.Log("DEAD");
        }
    }
}
