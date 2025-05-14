using System.Collections;
using UnityEngine;

//행동권환 활성화 해줄 클래스
public class ScCheckBoxCtrl : MonoBehaviour
{
    [SerializeField] private ScLookAroundRegion Look;
    [SerializeField] private ScHandUpRegion Hand;

    private void OnTriggerEnter(Collider other)
    {
        //print($"OnTriggerEnter: {other.gameObject.layer}");
        if (other.gameObject.layer == ScDefine.Layer.PlayerIndex)
        {
            Debug.Log("행동권환 제한 됨");
            StartCoroutine(AllCheck());

        }
    }

    private void FixedUpdate()
    {
        Debug.Log("move : " + Manager.Instance.GameMgr.canMove);
    }

    private IEnumerator AllCheck()
    {
        Debug.Log("여기 체킹후 또오면안됨.");        
        Manager.Instance.GameMgr.canMove = false;

        yield return new WaitUntil(() =>
        Look.lookAroundMissionClear && Hand.handUpMissionClear);

        Debug.Log("체크 완료");
        Manager.Instance.GameMgr.canMove = true;

    }
}


