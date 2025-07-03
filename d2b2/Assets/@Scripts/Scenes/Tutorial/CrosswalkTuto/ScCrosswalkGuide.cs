using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScCrosswalkGuide : MonoBehaviour
{
    [SerializeField] private ScCharacter guide;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform dest;
    [SerializeField] private float walkSpeed;

    void Start()
    {
        StartCoroutine(MoveToDestination());
    }

    private IEnumerator MoveToDestination()
    {
        guide.SetRaiseHandAnimation(true);
        transform.LookAt(dest);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);

        guide.SetWalkingAnim();
        while(Vector3.Distance(transform.position, dest.position) > 0.3f)
        {
            yield return null;
            transform.Translate(transform.forward * walkSpeed * Time.deltaTime);
        }

        guide.SetRaiseHandAnimation(false);
        guide.SetIdleAnim();

        transform.LookAt(playerTransform);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }
}
