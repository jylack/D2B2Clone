using System.Collections;
using System.Data.Common;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ScCrossWalkCtrl : MonoBehaviour
{
    [SerializeField] private float limitTime = 1f;

    [SerializeField] private ScTrafficCtrl scTrafficCtrl;

    [SerializeField] private TextMeshProUGUI IsMove;

    int CurrentStep = 0;

    bool isWalk = false;

    Coroutine coroutine = null;

    private void Start()
    {
        CurrentStep = ScChapter1.Instance.CurrentSetp;
        Manager.Instance.GameMgr.OnPlayerMoving += OnPlayerMoving;
        //Debug.Log("이동 구독 시작");


    }
    private void Update()
    {
        IsMove.text = isWalk.ToString();
    }


    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.layer == ScDefine.Layer.PlayerIndex)
        {
            
            var temp = other.gameObject.GetComponent<ScRespawn>();

            //횡단보도 이동중 빨간불임. or 신호등 깜빡일때.
            if (scTrafficCtrl.GetCurrentColor() == TrafficLightColor.Red ||
                scTrafficCtrl.IsBlink() == true)
            {
                Manager.Instance.GameMgr.canMove = false;
                temp.Init(CurrentStep);
                temp.Respawn();
            }
            
            if (isWalk == false)
            {
                //움직임 멈췄을때 타임리미트 돌리고 그 시간뒤까지 움직임이없으면 원위치
                if (coroutine == null)
                    coroutine = StartCoroutine(TimeLimit(other));
            }

            
        }

   
    }

    private void OnDisable()
    {
        Manager.Instance.GameMgr.OnPlayerMoving -= OnPlayerMoving;
    }


    IEnumerator TimeLimit(Collider other)
    {
        yield return new WaitForSeconds(limitTime);
        
        if (isWalk == false)
        {
            var temp = other.gameObject.GetComponent<ScRespawn>();
            temp.Init(CurrentStep);
            temp.Respawn();

            coroutine = null;
            yield break;
        }
    }

    private void OnPlayerMoving(bool isMoving)
    {        
        isWalk = isMoving;
    }
}
