using DG.Tweening;
using System.Collections;
using UnityEngine;

public class ScChapter1_NpcMoveController : MonoBehaviour
{
    [SerializeField] private float moveTime = 5f;
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
                //Debug.Log("NpcMove End");
                npc.SetAnimation(ScDefine.ScNpcAnimState.Idle);
                //핸드업 애니메이션 
                StartCoroutine(HandUpDown());
            });
    }

    IEnumerator HandUpDown()
    {
        while(true)
        {
            npc.RaiseHandOn();
            yield return new WaitForSeconds(0.5f);
            npc.RaiseHandOff();
        }
        
    }
}
