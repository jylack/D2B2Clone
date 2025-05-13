using UnityEngine;

public class ScDeadZoneCtrl : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.name);
        //Debug.Log("layer : " + other.gameObject.layer);

        if (other.gameObject.layer == ScDefine.Layer.PlayerIndex)
        {
            //Debug.Log("OnTriggerEnter");
            other.gameObject.GetComponent<ScRespawn>().Respawn();           
        }
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.layer == ScDefine.Layer.PlayerIndex)
    //    {
    //        Debug.Log("OnTriggerStay");
    //        other.gameObject.GetComponent<ScRespawn>().Respawn();
    //    }
    //}
}
