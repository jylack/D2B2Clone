using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class ScGoalPointHsy : MonoBehaviour
{
    [SerializeField] private UnityEvent goalEvent;
    [SerializeField] private GameObject arrowImg;
     private Vector3 arrowMinPos;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == ScDefine.Layer.PlayerIndex)
        {
            goalEvent?.Invoke();
           // Manager.Instance.GameMgr.SetPlayer();
        }
    }

    private void Start()
    {
        arrowImg.transform.position = new Vector3(transform.position.x, arrowImg.transform.position.y + 1, transform.position.z);
        arrowMinPos = new Vector3(transform.position.x, arrowImg.transform.position.y - 1, transform.position.z);
        StartCoroutine(ArrowUI());
    }

    private IEnumerator ArrowUI()
    {
        arrowImg.SetActive(true);
        arrowImg.transform.DOMoveY(arrowMinPos.y, 2).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine).SetLink(arrowImg, LinkBehaviour.PauseOnDisable);
        yield return new WaitForSeconds(5f);
        arrowImg.SetActive(false);

    }
}
