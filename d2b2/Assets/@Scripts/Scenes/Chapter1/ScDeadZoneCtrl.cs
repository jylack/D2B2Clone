using UnityEngine;


public class ScDeadZoneCtrl : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.layer == ScDefine.Layer.PlayerIndex)
        {
            ScRespawn.Instance.Respawn(true);
            Manager.Instance.SceneMgr.LoadScene(ScDefine.ScScene.Sg05_Jaywalking);
        }
    }
}
