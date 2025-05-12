using UnityEngine;

//행동권환 활성화 해줄 클래스
public class ScCheckBoxCtrl : MonoBehaviour
{
    [SerializeField] ScTrafficCtrl trafficCtrl;

    private void OnTriggerEnter(Collider other)
    {
        if(trafficCtrl == null )
        {
            Debug.Log("신호등 연결 안됨");
            return;
        }

        if (other.gameObject.layer == ScDefine.Layer.PlayerIndex)
        {
            Manager.Instance.GameMgr.MoveFlag = false;

        }
    }



}


