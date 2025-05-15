using System.Collections;
using TMPro;
using UnityEngine;

public class ScCrossWalkCtrl : MonoBehaviour
{
    [SerializeField] private float limitTime = 1f;

    [SerializeField] private ScTrafficCtrl scTrafficCtrl;

    [SerializeField] private TextMeshProUGUI IsMove;

    [SerializeField] private ScHandUpRegion Hand;



    bool isWalk = false;

    Coroutine coroutine = null;

    private void Start()
    {
        Manager.Instance.GameMgr.OnPlayerMoving += OnPlayerMoving;
        //Debug.Log("이동 구독 시작");


    }
    private void Update()
    {
        IsMove.text = isWalk.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == ScDefine.Layer.PlayerIndex)
        {
            if (ScChapter1.Instance.lookAroundMissionClear == false)
            {
                Debug.Log("CrossWNoLook");
                //안전가이드 좌우확인 호출할예정
                other.gameObject.GetComponent<ScRespawn>().Respawn(false);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.layer == ScDefine.Layer.PlayerIndex)
        {

            var temp = other.gameObject.GetComponent<ScRespawn>();

            //횡단보도 이동중 빨간불임. or 신호등 깜빡일때.
            if (scTrafficCtrl.CurrentColor == TrafficLightColor.Red ||
                scTrafficCtrl.IsBlink == true)
            {
                //Debug.Log("can1");
                Manager.Instance.GameMgr.canMove = false;
                temp.Init(ScChapter1.Instance.CurrentSetp);
                Debug.Log("Blink");
                temp.Respawn(true);

                //신호등 안전가이드 호출
            }

            if (isWalk == false || Hand.isLeftHandUp == false)
            {

                Debug.Log(Hand.isLeftHandUp);
                //움직임 멈췄을때 타임리미트 돌리고 그 시간뒤까지 움직임이없으면 원위치
                if (coroutine == null)
                    coroutine = StartCoroutine(TimeLimit(other));
                //
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
            temp.Init(ScChapter1.Instance.CurrentSetp);
            Debug.Log("Time");
            temp.Respawn(true);

            coroutine = null;
            yield break;
        }
    }

    private void OnPlayerMoving(bool isMoving)
    {
        isWalk = isMoving;
    }
}
