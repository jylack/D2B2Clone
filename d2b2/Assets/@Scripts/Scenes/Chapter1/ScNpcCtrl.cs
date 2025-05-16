using DG.Tweening;
using UnityEngine;

public class ScNpcCtrl : MonoBehaviour
{
    private Vector3 targetPos;
    [SerializeField] private float moveTime = 5f;
    [SerializeField] private float moveDir = 10f;

    public void NpcMove() 
    {
        var pos = transform.position;
        targetPos = pos + (transform.forward * moveDir);

        transform.DOMove(targetPos, moveTime);
    }
}
