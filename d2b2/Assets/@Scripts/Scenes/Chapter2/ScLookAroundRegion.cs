using System.Collections;
using UnityEngine;

public class ScLookAroundRegion : MonoBehaviour
{
    private bool checkLookLeft;
    private bool checkLookRight;
    public bool lookAroundMissionClear { get; private set; }
    [SerializeField] private float completeTime = 1;
    private string missionFaildMessage;
    private Coroutine lookCor;

    private void Start()
    {
        lookAroundMissionClear =false;
        missionFaildMessage = "고개 돌리기 미션 실패";
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == ScDefine.Layer.PlayerIndex)
        {
            checkLookLeft = false;
            checkLookRight = false;
            Manager.Instance.GameMgr.OnPlayerHeadTurn += CheckPlayerHeadTurn;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == ScDefine.Layer.PlayerIndex)
        {
            ExitMissionRegion();
        }
    }
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
            Debug.Log("왼쪽 완료");
        }
        else
        {
            checkLookRight = true;
            lookAroundMissionClear = true;
            Debug.Log("오른쪽 완료");
        }

        lookCor = null;

        if (lookAroundMissionClear)
        {
            Debug.Log("미션 성공!");
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
        UIPlayerHsy.Instance.OffLookAroundLeftProgress();
        UIPlayerHsy.Instance.OffLookAroundRightProgress();
    }

    public void ExitMissionRegion()
    {
        Manager.Instance.GameMgr.OnPlayerHeadTurn -= CheckPlayerHeadTurn;
        StopCurrentCoroutine();
        OffAllUI();
    }
}
