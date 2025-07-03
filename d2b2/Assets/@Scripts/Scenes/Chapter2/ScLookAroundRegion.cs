using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class ScLookAroundRegion : MonoBehaviour
{

    private bool checkLookLeft;
    private bool checkLookRight;
    public bool lookAroundMissionClear { get; private set; }
    [SerializeField] private float completeTime = 1;
    private Coroutine lookCor;

    [SerializeField] private UnityEvent OnCheckMissionClear;
    [SerializeField] private UnityEvent OnCheckMissionFailed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == ScDefine.Layer.PlayerIndex)
        {

            lookAroundMissionClear = false;
            checkLookLeft = false;
            checkLookRight = false;
            Manager.Instance.GameMgr.OnPlayerHeadTurn += CheckPlayerHeadTurn;
            OnCheckMissionFailed?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == ScDefine.Layer.PlayerIndex)
        {
            ExitMissionRegion();
        }
    }
    private void OnDestroy()
    {
        ExitMissionRegion();
    }
    // 좌우 확인 미션 시작
    private void CheckPlayerHeadTurn(ScDefine.ScHeadTurn headDirection)
    {
        if (headDirection == ScDefine.ScHeadTurn.Left && checkLookLeft == false)
        {
            UIPlayerHsy.Instance.OnLookAroundLeftProgress();
            lookCor = StartCoroutine(CheckHeadStayTime(headDirection));
        }
        else if (headDirection == ScDefine.ScHeadTurn.Right && checkLookLeft == true && checkLookRight == false)
        {
            UIPlayerHsy.Instance.OnLookAroundRightProgress();
            lookCor = StartCoroutine(CheckHeadStayTime(headDirection));
        }
        else if (headDirection == ScDefine.ScHeadTurn.Forward)
        {
            StopCurrentCoroutine();
            if (checkLookLeft == false)
            {
                UIPlayerHsy.Instance.OffLookAroundLeftProgress();
            }
            else if (checkLookRight == false)
            {
                UIPlayerHsy.Instance.OffLookAroundRightProgress();
            }
        }
    }

    // 좌우 확인 대기시간 측정 후 미션완료 체크
    private IEnumerator CheckHeadStayTime(ScDefine.ScHeadTurn headDirection)
    {
        float timer = 0f;
        while (timer < completeTime)
        {
            timer += Time.deltaTime;
            UIPlayerHsy.Instance.DrawLookArounProgress(timer, completeTime, headDirection);
            yield return null;
        }
        if (headDirection == ScDefine.ScHeadTurn.Left)
        {
            checkLookLeft = true;
        }
        else
        {
            checkLookRight = true;
            lookAroundMissionClear = true;
        }
        lookCor = null;
        if (lookAroundMissionClear)
        {
            OnCheckMissionClear?.Invoke();
            OffAllUI();
        }
    }

    private void StopCurrentCoroutine()
    {
        if (lookCor != null)
        {
            StopCoroutine(lookCor);
            lookCor = null;
        }
    }

    private void OffAllUI()
    {
        if (UIPlayerHsy.Instance != null)
        {
            UIPlayerHsy.Instance.OffLookAroundLeftProgress();
            UIPlayerHsy.Instance.OffLookAroundRightProgress();
        }
    }

    public void ExitMissionRegion()
    {
        Manager.Instance.GameMgr.OnPlayerHeadTurn -= CheckPlayerHeadTurn;
        StopCurrentCoroutine();
        OffAllUI();
    }
}
