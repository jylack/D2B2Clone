using System.Collections;
using UnityEngine;

public enum ColTag
{
    RightHand,
    LeftHand,
    Body,
    MoveFront
}


public class ScControllerCtrl : MonoBehaviour
{
    bool isBody;
    bool isFront;

    float swingTime = 0.5f;

    private void Init()
    {
        isBody = false;
        isFront = false;
    }
    private void Start()
    {
        Init();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == ColTag.Body.ToString())
        {
            //isBody = !isBody;
            StartCoroutine(IsBody());
        }

        if (other.tag == ColTag.MoveFront.ToString())
        {
            //isFront = !isFront;
            StartCoroutine(IsFront());
        }
    }


    public bool IsMoving()
    {
        bool isMoving = isBody && isFront;

        Debug.Log(isMoving);
        return isMoving;
    }

    IEnumerator IsBody()
    {
        isBody = true;

        yield return new WaitForSeconds(swingTime);

        if (isFront == false)
        {
            isBody = false;
            yield break;
        }
    }

    IEnumerator IsFront()
    {
        isFront = true;

        yield return new WaitForSeconds(swingTime);

        if (isBody == false)
        {
            isFront = false;
            yield break;
        }
    }




}
