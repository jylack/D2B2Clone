using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScGuideNpc : MonoBehaviour
{
    private Vector3 targetPos;
    [SerializeField] private float moveTime = 5f;
    [SerializeField] private float moveDir = 10f;
    [SerializeField] private TextMeshProUGUI TalkBox;
    [SerializeField] private float TalkDeliay = 1f;

    int index = 0;

    private List<string> TalkArr = new List<string>();

    private void Start()
    {
        TalkArr.Add("괜찮아. 누구나 실수할 수 있는 거야.");
        TalkArr.Add("방금 있었던 일이 왜 위험했는지, 같이 살펴보자!");
        TalkArr.Add("방금 행동이 위험했던 건, 네가 노란선을 지나쳤기 때문이야.");
        TalkArr.Add("그 선은 멈추라는 표시야. 무시하면 진짜 큰 사고로 이어질 수도 있어.");
        TalkArr.Add("안전선이 있는 이유는, 운전자들이 우리를 더 쉽게 볼 수 있도록 하기 위해서야.");
        TalkArr.Add("그리고 혹시라도 너무 앞으로 나가면, 차와 부딪힐 위험이 있기 때문이야.");
        TalkArr.Add("봐봐, 저렇게 너무 앞으로 나가 있으면…");
        TalkArr.Add("그럼, 어떻게 해야 안전한지 같이 한번 볼까?");
        TalkArr.Add("봐봐, 이 친구는 노란선 안쪽에서 잘 기다리고 있어!");
        TalkArr.Add("이런 식으로 하면, 운전자도 너를 잘 볼 수 있어서 훨씬 더 안전해!");
        TalkArr.Add("어때? 이제 어떻게 하면 좋은지 알겠지?");
        TalkArr.Add("다시 한번 도로로 돌아가서 다시 도전하자!");

        NpcMove();
    }

    public void NpcMove()
    {
        var pos = transform.position;
        targetPos = pos + (transform.forward * moveDir);

        transform.DOMove(targetPos, moveTime).OnComplete(
            () => Debug.Log(targetPos)).Complete();
        //(7.05, 0.00, 4.70)
        StartCoroutine(talking());
    }

    private IEnumerator talking()
    {
        yield return new WaitForSeconds(moveTime);

        while (index < TalkArr.Count)
        {
            yield return new WaitForSeconds(TalkDeliay);
            NextTalk();
        }
    }

    private void NextTalk()
    {
        TalkBox.text = TalkArr[index];
        index++;
    }
}
