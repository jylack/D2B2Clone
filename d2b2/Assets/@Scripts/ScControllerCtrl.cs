using System.Collections;
using UnityEngine;

public enum ColTag
{
    RightHand,
    LeftHand,
    Body,
    MoveFront
}

public class ScControllerCtrl : MonoBehaviour
{
    
    private float swingTime = 0.5f;    // 허용 간격

    private bool isBody = false;     // Body 트리거 감지 여부
    private bool isFront = false;    // MoveFront 트리거 감지 여부
    private bool isMoving = false;     // 연속 스윙 중인지

    private Coroutine resetRoutine;    // 리셋 코루틴 참조

    
    public bool IsMoving => isMoving;

    //초기화
    public void Init(float swingTime)
    {
        this.swingTime = swingTime;
        isBody = false;
        isFront = false;
        isMoving = false;

        if (resetRoutine != null)
            StopCoroutine(resetRoutine);
        resetRoutine = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Body 영역
        if (other.CompareTag(ColTag.Body.ToString()))
        {
            HandleTrigger(ref isBody);
        }
        // MoveFront 영역
        else if (other.CompareTag(ColTag.MoveFront.ToString()))
        {
            HandleTrigger(ref isFront);
        }
    }

    /// <summary>
    /// 한쪽 영역 트리거 시
    /// 1) 해당 플래그를 true로,
    /// 2) 두 플래그가 모두 true면 이동 시작
    /// 3) 리셋 코루틴은 항상 재시작
    /// </summary>
    private void HandleTrigger(ref bool flag)
    {
        flag = true;

        // 두 영역이 모두 트리거된 상태면 이동 시작
        if (isBody && isFront && !isMoving)
        {
            isMoving = true;
        }

        // 일정 시간 입력이 없으면 다시 플래그 해제
        if (resetRoutine != null)
            StopCoroutine(resetRoutine);

        resetRoutine = StartCoroutine(ResetFlags());
    }

    /// <summary>
    /// swingTime 후에 플래그들과 이동 상태를 초기화합니다.
    /// </summary>
    private IEnumerator ResetFlags()
    {
        yield return new WaitForSeconds(swingTime);

        isBody = false;
        isFront = false;
        isMoving = false;
        resetRoutine = null;
    }
}
