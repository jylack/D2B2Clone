using System.Collections;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;

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
        
        //if (isMoving)
        //{
        //    Init();
        //}

        Debug.Log(isMoving);
        return isMoving;
    }

    IEnumerator IsBody()
    {
        isBody = true;

        yield return new WaitForSeconds(0.5f);

        isBody = false;
    }

    IEnumerator IsFront()
    {
        isFront = true;

        yield return new WaitForSeconds(0.5f);

        isFront = false;
    }




}
