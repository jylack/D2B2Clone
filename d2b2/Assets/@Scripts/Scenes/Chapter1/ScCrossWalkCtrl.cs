using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ScCrossWalkCtrl : MonoBehaviour
{
    [SerializeField] private float limitTime = 1f;
    [SerializeField] private TextMeshProUGUI IsMove;
    [SerializeField] private ScHandUpRegion Hand;

    [SerializeField] private UnityEvent OnCheckMissionClear;
    [SerializeField] private UnityEvent OnCheckMissionFailed;


    bool isColorRed = false;
    bool isBlink = false;
    bool isHandUp = false;
    bool isWalk = false;

    Coroutine coroutine = null;

    private void Start()
    {
        Manager.Instance.GameMgr.OnPlayerMoving += OnPlayerMoving;
        Manager.Instance.InputMgr.OnLeftStickMove += OnLeftStickMove;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == ScDefine.Layer.PlayerIndex)
        {
            if (ScChapter1.Instance.LookAroundMissionClear == false)
            {
                ScRespawn.Instance.Respawn();
                Manager.Instance.SceneMgr.LoadScene(ScDefine.ScScene.Sg02_LookAround);
                OnCheckMissionFailed?.Invoke();
                
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.layer == ScDefine.Layer.PlayerIndex)
        {

            if (isColorRed || isBlink)
            {
                ScRespawn.Instance.Init(ScChapter1.CurrentSetp);
                ScRespawn.Instance.Respawn();

                if (isBlink)
                {
                    Manager.Instance.SceneMgr.LoadScene(ScDefine.ScScene.Sg04_TrafficBlink);
                    return;
                }

                if (isColorRed)
                {
                    Manager.Instance.SceneMgr.LoadScene(ScDefine.ScScene.Sg08_Jaywalking);
                    return;
                }

            }

            if (Hand.isLeftHandUp ==true && isHandUp ==false)
            {
                OnCheckMissionClear?.Invoke();
                isHandUp = true;
            }

            //Debug.Log("isWalk : " + isWalk);

            if (isWalk == false)
            {
                if (coroutine == null)
                    coroutine = StartCoroutine(StopMoveTimeLimit(other));
            }
            else if(isWalk == true && coroutine != null)
            {
                Debug.Log("Stop Coroutine");
                StopCoroutine(coroutine);
                coroutine = null;
            }            

            if (Hand.isLeftHandUp == false)
            {
                OnCheckMissionFailed?.Invoke();
                isHandUp = false;

                if (coroutine == null)
                    coroutine = StartCoroutine(HandDownTimeLimit(other));

            }
        }
    }

    private void OnDisable()
    {
        Manager.Instance.GameMgr.OnPlayerMoving -= OnPlayerMoving;
        Manager.Instance.InputMgr.OnLeftStickMove -= OnLeftStickMove;
    }


    IEnumerator StopMoveTimeLimit(Collider other)
    {
        yield return new WaitForSeconds(limitTime);

        if (isWalk == false)
        {
            ScRespawn.Instance.Init(ScChapter1.CurrentSetp);
            ScRespawn.Instance.Respawn();
            Manager.Instance.SceneMgr.LoadScene(ScDefine.ScScene.Sg05_SafeWalk);            
            coroutine = null;
            yield break;
        }
    }

    IEnumerator HandDownTimeLimit(Collider other)
    {
        yield return new WaitForSeconds(limitTime);

        if (Hand.isLeftHandUp == false)
        {
            ScRespawn.Instance.Init(ScChapter1.CurrentSetp);
            ScRespawn.Instance.Respawn();
            Manager.Instance.SceneMgr.LoadScene(ScDefine.ScScene.Sg03_HandUp);
            coroutine = null;
            yield break;
        }
    }

    private void OnPlayerMoving(bool isMoving)
    {
        isWalk = isMoving;
        //Debug.Log("2. OnPlayerMoving : " + isMoving);
    }

    private void OnLeftStickMove(bool isStick)
    {
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
