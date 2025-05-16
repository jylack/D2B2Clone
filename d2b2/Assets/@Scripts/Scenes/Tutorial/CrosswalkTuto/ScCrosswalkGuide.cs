using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScCrosswalkGuide : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform dest;
    [SerializeField] private float walkSpeed;

    void Start()
    {
        StartCoroutine(MoveToDestination());
    }

    private IEnumerator MoveToDestination()
    {
        transform.LookAt(dest);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);

        //여기서 walk 애니메이션 키기
        while(Vector3.Distance(transform.position, dest.position) > 0.3f)
        {
            yield return null;
            transform.Translate(transform.forward * walkSpeed * Time.deltaTime);
        }
        //여기서 walk 애니메이션 끄기

        transform.LookAt(playerTransform);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);

        //손 흔드느 애니메이션 재생(루프 아니면 따로 빼기)
    }
}
