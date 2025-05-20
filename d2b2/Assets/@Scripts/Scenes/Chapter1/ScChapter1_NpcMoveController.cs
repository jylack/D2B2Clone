using DG.Tweening;
using UnityEngine;

public class ScChapter1_NpcMoveController : MonoBehaviour
{
    [SerializeField] private float moveTime = 5f;
    [SerializeField] private Transform targetTrans;

    ScNpc npc;

    private void Start()
    {
        npc = GetComponent<ScNpc>();
    }

    public void NpcMove()
    {
        npc.SetAnimation(ScDefine.ScNpcAnimState.Running);
        transform.DOMove(targetTrans.position, moveTime).OnComplete(() =>
            {
                npc.SetRaiseHandAnimation(ScDefine.ScHandSide.Right);
            });

    }
}
