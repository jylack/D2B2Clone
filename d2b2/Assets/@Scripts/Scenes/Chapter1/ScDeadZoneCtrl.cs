using UnityEngine;

//위험지역 코드
public class ScDeadZoneCtrl : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.layer == ScDefine.Layer.PlayerIndex)
        {
            ScRespawn.Instance.Respawn();
            Manager.Instance.SceneMgr.LoadScene(ScDefine.ScScene.Sg08_Jaywalking);
        }
    }
}
