using UnityEngine;


//����Ⱦ��
public class ScDeadZoneCtrl : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("layer : " + other.gameObject.layer);

        if (other.gameObject.layer == ScDefine.Layer.PlayerIndex)
        {
            Manager.Instance.GameMgr.canMove = false;

            Debug.Log("Dead");
            //�������̵� ����Ⱦ��
            //other.gameObject.GetComponent<ScRespawn>().Respawn(true); 
            ScRespawn.Instance.Respawn(true);
            Manager.Instance.SceneMgr.LoadScene(ScDefine.ScScene.Sg05_Jaywalking);
        }
    }
}
