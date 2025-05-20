using UnityEngine;


public class ScDeadZoneCtrl : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.layer == ScDefine.Layer.PlayerIndex)
        {
            Manager.Instance.GameMgr.canMove = false;

            //Debug.Log("DeadZone-Sg05_Jaywalking");
            ScRespawn.Instance.Respawn(true);
            Manager.Instance.SceneMgr.LoadScene(ScDefine.ScScene.Sg05_Jaywalking);
        }
    }
}
