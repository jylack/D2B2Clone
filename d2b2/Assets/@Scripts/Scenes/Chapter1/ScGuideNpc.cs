using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

public class ScGuideNpc : MonoBehaviour
{
    [SerializeField] private ScPlayer player;


    private void Awake()
    {
        Manager.Instance.GameMgr.OnPlayerMoving += OnPlayerMoving;

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
        else
        {
            // 플레이어가 멈추면 NPC도 멈추도록 설정
            StopFollowingPlayer();
        }
    }

    private void FollowPlayer()
    {
        // NPC가 플레이어를 따라가는 로직 구현
        Vector3 targetPosition = player.transform.position;
        transform.DOMove(targetPosition, 1f).SetEase(Ease.Linear);
    }
    private void StopFollowingPlayer()
    {
        // NPC가 멈추는 로직 구현
        transform.DOKill(); // 현재 진행 중인 이동을 중지
                            // 추가적인 멈춤 애니메이션이나 행동을 여기에 추가할 수 있습니다.

    }
}