using System.Collections;
using UnityEngine;

//행동권환 활성화 해줄 클래스
public class ScCheckBoxCtrl : MonoBehaviour
{
    [SerializeField] private ScLookAroundRegion Look;

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == ScDefine.Layer.PlayerIndex)
        {
            if(Look.lookAroundMissionClear == false)
            {
                Debug.Log("checkbox");
                other.gameObject.GetComponent<ScRespawn>().Respawn(false);
            }
            else
            {
                ScChapter1.Instance.lookAroundMissionClear = Look.lookAroundMissionClear;
            }
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.layer == ScDefine.Layer.PlayerIndex)
    //    {
    //        Debug.Log("행동권환 제한 됨");
    //        StartCoroutine(AllCheck(other));
    //    }
    //}


    //private IEnumerator AllCheck(Collider other)
    //{
    //    var prov = other.gameObject.GetComponent<ScRespawn>().PlayerTrans.GetComponent<ScPlayer>().MovePorv;

    //    //Debug.Log("여기 체킹후 또오면안됨.");        
    //    Manager.Instance.GameMgr.canMove = false;
    //    prov.enabled = false;

    //    yield return new WaitUntil(() =>
    //    Look.lookAroundMissionClear && Hand.isLeftHandUp);

    //    //Debug.Log("체크 완료");
    //    Manager.Instance.GameMgr.canMove = true;
    //    prov.enabled = true;
    //}
}


