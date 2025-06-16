using DG.Tweening;
using System.Collections;
using UnityEngine;

public class ScChapter1_NpcMoveController : MonoBehaviour
{
    [SerializeField] private float moveTime = 4f;
    [SerializeField] private Transform targetTrans;

    ScCharacter npc;

    private void Awake()
    {
        npc = GetComponent<ScCharacter>();
        npc.SetAnimation(ScDefine.ScNpcAnimState.Running);
    }

    private void OnEnable()
    {
        NpcMove();
    }

    private void NpcMove()
    {
        transform.DOMove(targetTrans.position, moveTime).OnComplete(() =>
            {
                npc.SetAnimation(ScDefine.ScNpcAnimState.Idle);
            });
    }


}
