using UnityEngine;
using UnityEngine.Events;

//행동권환 활성화 해줄 클래스
public class ScCheckBoxCtrl : MonoBehaviour
{
    [SerializeField] private ScLookAroundRegion Look;

    [SerializeField] private UnityEvent OnCheckMissionClear;
    [SerializeField] private UnityEvent OnCheckMissionFailed;

    private bool isMove = false;
    private bool isClearInvoked = false;  

    private void OnEnable()
    {
        Manager.Instance.GameMgr.OnPlayerMoving += OnMoving;
        Manager.Instance.InputMgr.OnLeftStickMove += OnLeftStickMove;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == ScDefine.Layer.PlayerIndex)
        {
            isClearInvoked = false;
            OnCheckMissionFailed?.Invoke();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == ScDefine.Layer.PlayerIndex)
        {
            //Debug.Log("4. OnTriggerStay : " + isMove);
            if (isMove == false && isClearInvoked == false)
            {
                OnCheckMissionClear?.Invoke();
                isClearInvoked = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == ScDefine.Layer.PlayerIndex)
        {
            ScChapter1.Instance.LookAroundMissionClear = Look.lookAroundMissionClear;
        }
    }

    private void OnDestroy()
    {
        Manager.Instance.GameMgr.OnPlayerMoving -= OnMoving;
        Manager.Instance.InputMgr.OnLeftStickMove -= OnLeftStickMove;
    }

    private void OnMoving(bool isMoving)
    {
        isMove = isMoving;
    }

    private void OnLeftStickMove(bool isMoving)
    {
        isMove = isMoving;
    }

}


