using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScTutoClear : MonoBehaviour
{
    [SerializeField] private Transform moveGoal;
    [SerializeField] private float walkSpeed;

    private void Start()
    {
        Clear();
    }
    public void Clear()
    {
        StartCoroutine(MoveToGoal());
    }

    private IEnumerator MoveToGoal()
    {
        transform.LookAt(moveGoal);
        while (Vector3.Distance(transform.position, moveGoal.position) > 0.3f)
        {
            yield return null;
            transform.position = Vector3.Lerp(transform.position, moveGoal.position, Time.deltaTime * walkSpeed);
        }
        transform.position = moveGoal.position;
        transform.rotation = Quaternion.Euler(0, 180, 0);
    }
}
