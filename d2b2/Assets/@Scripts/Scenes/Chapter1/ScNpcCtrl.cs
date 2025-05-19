using DG.Tweening;
using UnityEngine;

public class ScNpcCtrl : MonoBehaviour
{
    [SerializeField] private float moveTime = 5f;
    [SerializeField] private Transform targetTrans;

    public void NpcMove() 
    {
        transform.DOMove(targetTrans.position, moveTime);
    }
}
