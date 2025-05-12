using UnityEngine;

//행동권환 활성화 해줄 클래스
public class ScCheckBoxCtrl : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        print($"OnTriggerEnter: {other.gameObject.layer}");
        if (other.gameObject.layer == ScDefine.Layer.PlayerIndex)
        {
            //Manager.Instance.GameMgr.MoveFlag = false;
            Debug.Log("행동권환");
        }
    }
}


