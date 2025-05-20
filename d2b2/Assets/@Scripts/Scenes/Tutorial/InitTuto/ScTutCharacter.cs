using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScTutCharacter : MonoBehaviour
{
    [SerializeField] ScDefine.ScGuideCharacter charaId;
    [SerializeField] float walkingDist;
    [SerializeField] float movingTime;
    [SerializeField] float moveTick;
    [SerializeField] Animator animator;
    Outline outline;
    Vector3 startPos;

    private void Awake()
    {
        outline = GetComponent<Outline>();
    }

    private void Start()
    {
        startPos = transform.position;
        outline.enabled = false;
    }

    public void SetOutline(bool enable)
    {
        if(outline != null)
        {
            outline.enabled = enable;
        }
    }

    public void SetOutlineColor(Color color)
    {
        outline.OutlineColor = color;
    }

    public void WalkForward(ScCharaSelectManager selectManager)
    {
        StartCoroutine(DoWalkForward(selectManager));
    }

    private IEnumerator DoWalkForward(ScCharaSelectManager selectManager)
    {
        transform.position = startPos;
        Vector3 moved = startPos;
        moved.z -= walkingDist;
        float movingDist = walkingDist / (movingTime / moveTick);
        //animator.SetBool("Walk", true); 여기서 걷기 애니메이션
        while(transform.position.z >= moved.z)
        {
            yield return new WaitForSeconds(moveTick);
            transform.Translate(transform.forward * -movingDist);
            if(transform.position.z <= moved.z)
            {
                transform.position = moved;
                break;
            }
        }
        //animator.SetBool("Walk", false); 걷기 애니메이션 종료
        selectManager.OpenConfirmPopUp();
    }

    public void WalkBack()
    {
        StartCoroutine(DoWalkBack());
    }

    private IEnumerator DoWalkBack()
    {
        Vector3 cur = transform.position;
        float movingDist = (startPos.z - transform.position.z) / (movingTime / moveTick);
        //animator.SetBool("Walk", true); 여기서 걷기 애니메이션
        while (transform.position.z <= startPos.z)
        {
            yield return new WaitForSeconds(moveTick);
            transform.Translate(transform.forward * movingDist);
            if (transform.position.z <= startPos.z)
            {
                transform.position = startPos;
                break;
            }
        }
        //animator.SetBool("Walk", false); 걷기 애니메이션 종료
    }

    public ScDefine.ScGuideCharacter GetCharaId()
    {
        return charaId;
    }
}
