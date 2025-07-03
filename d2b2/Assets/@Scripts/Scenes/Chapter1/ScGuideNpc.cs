using DG.Tweening;
using UnityEngine;

public class ScGuideNpc : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private Vector3 offset;
    private Tween moveTween;
    private bool isMove = false;

    private void Awake()
    {
        Manager.Instance.GameMgr.OnPlayerMoving += OnPlayerMoving;

        offset = player.transform.InverseTransformPoint(transform.position);
        

    }
    private void OnDestroy()
    {
        Manager.Instance.GameMgr.OnPlayerMoving -= OnPlayerMoving;
    }

    //매니저에서 플레이어가 움직일 때 호출되는 이벤트 핸들러
    private void OnPlayerMoving(bool isMoving)
    {
        isMove = isMoving;

        // NPC가 플레이어의 움직임에 따라 반응하도록 구현
        if (isMoving)
        {
            // 플레이어가 움직일 때 NPC가 따라오도록 설정
            FollowPlayer();
            
        }
    }

    // NPC가 플레이어를 따라가는 메서드
    private void FollowPlayer()
    {
        // 이동 중이면 현재 위치를 시작점으로 새 목표로 다시 계산
        Vector3 currentPos = transform.position;
        Vector3 newTarget = player.transform.TransformPoint(offset);

        // 기존 Tween이 있으면 Kill
        if (moveTween != null && moveTween.IsActive())
        {
            moveTween.Kill();
        }

        float duration = 1f;

        // 플레이어의 위치로 NPC를 이동시키는 Tween 생성
        moveTween = transform.DOMove(newTarget, duration)// DOTween을 사용하여 NPC를 플레이어 위치로 이동
                             .SetEase(Ease.Linear)// Ease 설정
                             .SetAutoKill(true)// 자동으로 Tween을 제거
                             .OnComplete(() => // Tween이 완료되면 호출되는 콜백
                             {
                                 if (isMove)// 플레이어가 여전히 움직이고 있다면
                                 {
                                     FollowPlayer();// 계속해서 플레이어를 따라가도록 재귀 호출
                                 }
                             })                          
                             ;
                             
    }
}