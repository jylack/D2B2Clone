using System.Collections;
using TMPro;
using UnityEngine;

public class ScCrossWalkCtrl : MonoBehaviour
{
    [SerializeField] private float limitTime = 1f;
    //[SerializeField] private ScTrafficCtrl scTrafficCtrl;
    [SerializeField] private TextMeshProUGUI IsMove;
    [SerializeField] private ScHandUpRegion Hand;



    bool isColorRed = false;
    bool isBlink = false;   

    bool isWalk = false;

    Coroutine coroutine = null;

    private void Start()
    {
        Manager.Instance.GameMgr.OnPlayerMoving += OnPlayerMoving;
        //Debug.Log("이동 구독 시작");
        Manager.Instance.InputMgr.OnLeftStickMove += OnLeftStickMove;

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
                ScRespawn.Instance.Respawn(false);
                //other.gameObject.GetComponent<ScRespawn>().Respawn(false);
                Manager.Instance.SceneMgr.LoadScene(ScDefine.ScScene.Sg02_LookAround);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.layer == ScDefine.Layer.PlayerIndex)
        {

            var temp = other.gameObject.GetComponent<ScRespawn>();

            //횡단보도 이동중 빨간불임. or 신호등 깜빡일때.
            //if (scTrafficCtrl.CurrentColor == TrafficLightColor.Red ||
            //    scTrafficCtrl.IsBlink == true)
            if(isColorRed || isBlink)
            {
                //Debug.Log("can1");
                Manager.Instance.GameMgr.canMove = false;
                ScRespawn.Instance.Init(ScChapter1.Instance.CurrentSetp);
                //temp.Init(ScChapter1.Instance.CurrentSetp);
                //Debug.Log("Blink");
                ScRespawn.Instance.Respawn(true);

                //temp.Respawn(true);

                //신호등 안전가이드 호출
                if(isBlink)
                {
                    Debug.Log("Blink");
                    Manager.Instance.SceneMgr.LoadScene(ScDefine.ScScene.Sg04_TrafficBlink);
                    return;
                }
                if (isColorRed)
                {
                    Debug.Log("Red");
                    Manager.Instance.SceneMgr.LoadScene(ScDefine.ScScene.Sg05_Jaywalking);
                    return;
                }
                
            }

            if (isWalk == false || Hand.isLeftHandUp == false)
            {
                Debug.Log(Hand.isLeftHandUp);
                //움직임 멈췄을때 타임리미트 돌리고 그 시간뒤까지 움직임이없으면 원위치
                if (coroutine == null)
                    coroutine = StartCoroutine(TimeLimit(other));
            }
        }
    }

    private void OnDisable()
    {
        Manager.Instance.GameMgr.OnPlayerMoving -= OnPlayerMoving;
        Manager.Instance.InputMgr.OnLeftStickMove -= OnLeftStickMove;
    }
    

    IEnumerator TimeLimit(Collider other)
    {
        yield return new WaitForSeconds(limitTime);

        if (isWalk == false || Hand.isLeftHandUp == false)
        {
            var temp = other.gameObject.GetComponent<ScRespawn>();
            ScRespawn.Instance.Init(ScChapter1.Instance.CurrentSetp);
            //temp.Init(ScChapter1.Instance.CurrentSetp);
            Debug.Log("Time");
            //temp.Respawn(true);
            ScRespawn.Instance.Respawn(true);
            //이동 중간에 멈춤
            Manager.Instance.SceneMgr.LoadScene(ScDefine.ScScene.Sg05_Jaywalking);
            coroutine = null;
            yield break;
        }
    }

    private void OnPlayerMoving(bool isMoving)
    {
        isWalk = isMoving;
    }

    private void OnLeftStickMove(bool isStick)
    {
        Debug.Log("스틱 움직임 : " + isStick);
        isWalk = isStick;
    }

    public void OnRed()
    {
        isColorRed = true;          
        isBlink = false;
    }

    public void OnBlink()
    {
        isBlink = true;
    }

    public void OnGrean()
    {
        isColorRed = false;
        isBlink = false;
    }
}
