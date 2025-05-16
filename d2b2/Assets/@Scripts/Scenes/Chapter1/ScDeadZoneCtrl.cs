using UnityEngine;


//무단횡단
public class ScDeadZoneCtrl : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("layer : " + other.gameObject.layer);

        if (other.gameObject.layer == ScDefine.Layer.PlayerIndex)
        {
            Manager.Instance.GameMgr.canMove = false;

            Debug.Log("Dead");
            //안전가이드 무단횡단
            //other.gameObject.GetComponent<ScRespawn>().Respawn(true); 
            ScRespawn.Instance.Respawn(true);
        }
    }
}
