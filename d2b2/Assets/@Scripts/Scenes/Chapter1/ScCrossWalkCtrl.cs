using System.Collections;
using System.Security.Cryptography;
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

    bool isWalk = false;

    Coroutine coroutine = null;

    private void Start()
    {
        Manager.Instance.GameMgr.OnPlayerMoving += OnPlayerMoving;
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

            var temp = other.gameObject.GetComponent<ScRespawn>();

            if(isColorRed || isBlink)
            {
                ScRespawn.Instance.Init(ScChapter1.CurrentSetp);
                ScRespawn.Instance.Respawn();

                if(isBlink)
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

            if(Hand.isLeftHandUp)
            {
                OnCheckMissionClear?.Invoke();
            }

            if (isWalk == false || Hand.isLeftHandUp == false)
            {
                OnCheckMissionFailed?.Invoke();

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
            //Debug.Log("isWalk : " + isWalk);
            //Debug.Log("Hand.isLeftHandUp : " + Hand.isLeftHandUp);
            var temp = other.gameObject.GetComponent<ScRespawn>();
            ScRespawn.Instance.Init(ScChapter1.CurrentSetp);
            ScRespawn.Instance.Respawn();
            //¿Ãµø ¡ﬂ∞£ø° ∏ÿ√„
            Manager.Instance.SceneMgr.LoadScene(ScDefine.ScScene.Sg05_SafeWalk);
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
        Debug.Log("OnBlink");   
    }

    public void OnGrean()
    {
        isColorRed = false;
        isBlink = false;
    }
}
