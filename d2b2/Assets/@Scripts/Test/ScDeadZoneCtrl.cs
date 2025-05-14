using UnityEngine;

public class ScDeadZoneCtrl : MonoBehaviour
{


    //private void OnTriggerEnter(Collider other)
    //{
    //    //Debug.Log("layer : " + other.gameObject.layer);

    //    if (other.gameObject.layer == ScDefine.Layer.PlayerIndex)
    //    {
    //        Manager.Instance.GameMgr.canMove = false;
    //        other.gameObject.GetComponent<ScRespawn>().Respawn();

    //    }
    //}

    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.layer == ScDefine.Layer.PlayerIndex)
        {
            Debug.Log("can2");
            Manager.Instance.GameMgr.canMove = false;
            other.gameObject.GetComponent<ScRespawn>().Respawn();
        }
    }

}
