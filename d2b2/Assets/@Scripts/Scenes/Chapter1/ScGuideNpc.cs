using DG.Tweening;
using UnityEngine;

public class ScGuideNpc : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private Vector3 offset;
    private Tween moveTween;

    private void Awake()
    {
        Manager.Instance.GameMgr.OnPlayerMoving += OnPlayerMoving;

        offset = player.transform.InverseTransformPoint(transform.position);

    }
    private void OnDestroy()
    {
        Manager.Instance.GameMgr.OnPlayerMoving -= OnPlayerMoving;
    }

    private void OnPlayerMoving(bool isMoving)
    {
        // NPC가 플레이어의 움직임에 따라 반응하도록 구현
        if (isMoving)
        {
            // 플레이어가 움직일 때 NPC가 따라오도록 설정
            FollowPlayer();
        }
    }
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

        // 거리 비례 속도 조정 (선택 사항)
        float distance = Vector3.Distance(currentPos, newTarget);
        if (distance < 0.01f) return; // 너무 가까우면 무시

        moveTween = transform.DOMove(newTarget, duration)
                             .SetEase(Ease.Linear)
                             .SetAutoKill(true);
    }
}