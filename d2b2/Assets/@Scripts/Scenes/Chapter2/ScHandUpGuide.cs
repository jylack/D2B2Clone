using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class ScHandUpGuide : ScSceneBase
{
    [SerializeField] GameObject guideNpc;
    [SerializeField] GameObject guideNpcContextUI;
    [SerializeField] GameObject problemChildNpc;
    [SerializeField] GameObject truck;
    [SerializeField] List<string> npcTextList;
    [SerializeField] TextMeshPro guideNpcContextTxt;
    private void Start()
    {
        //npcTextList.Add("괜찮아. 누구나 실수할 수 있는 거야.");
        //npcTextList.Add("방금 있었던 일이 왜 위험했는지, 같이 살펴보자!");
        //npcTextList.Add("아까 너는 그냥 걸어가기만 했지? 손은 안 들었더라.");
        //npcTextList.Add("손을 안 들면, 차에 있는 사람들이 널 못 보고 차가 그냥 지나갈 수도 있어!");
        //npcTextList.Add("그럼 아야하겠지? 그래서 조심해야 해!");
        //
        //npcTextList.Add("차에 있는 사람들은, 앞이나 옆이 잘 안 보일 때도 있어.");
        //npcTextList.Add("그래서 우리가 먼저 손을 들어서 알려주는 게 좋아.");
        //
        //// 문제아 등장
        //npcTextList.Add("봐봐, 손도 안 들고 그냥 건너고 있지?");
        //npcTextList.Add("손을 안 들면, 운전자도 못 보고 멈추기 어려울 수 있어.");
        //
        //npcTextList.Add("그럼, 이제 어떻게 하면 되는지 같이 볼까?");
        //npcTextList.Add("이 친구는 건널 때 손을 들고 있지?");
        //npcTextList.Add("그럼 차에 있는 사람들도 금방 알아볼 수 있어서 안전하게 건널 수 있어!");
        //npcTextList.Add("어때? 이제 어떻게 하면 좋은지 알겠지?");
        //npcTextList.Add("다시 한번 도로로 돌아가서 다시 도전하자!");
        //
        //yield return new WaitForSeconds(1);
        //StartCoroutine(StartHandUpGuide());
        //StartHandUpGuide();
    }
    void Update()
    {
    }
    IEnumerator StartHandUpGuide()
    {
        // guideNpc 이동
        guideNpc.transform.DOMoveZ(-5, 1.5f).WaitForCompletion();
        guideNpcContextUI.gameObject.SetActive(true);
        guideNpc.transform.DORotate(new Vector3(0, 180f, 0), 2f).WaitForCompletion();

        // 자막 보여주기 (순차적으로)
        yield return StartCoroutine(ShowCurrentText(0, 5));
        yield return StartCoroutine(ShowCurrentText(5, 2));
        yield return new WaitForSeconds(3f);

        // 문제아 등장 + 차량 등장
        problemChildNpc.SetActive(true);
        yield return new WaitForSeconds(1f);
        problemChildNpc.transform.DOMoveZ(-3, 1.5f);
        yield return StartCoroutine(ShowCurrentText(7, 2));
        truck.transform.DOMoveX(-10, 1.5f).WaitForCompletion();

        yield return new WaitForSeconds(2);
        truck.transform.position = new Vector3(-20,0,-2.7f);
        problemChildNpc.SetActive(false);

        yield return StartCoroutine(ShowCurrentText(9, 1));
        problemChildNpc.SetActive(true);
        yield return new WaitForSeconds(1f);
        truck.transform.DOMoveX(-8, 1.5f);
        problemChildNpc.transform.DOMoveZ(5, 1.5f);
        yield return StartCoroutine(ShowCurrentText(10, 4));

    }
    IEnumerator ShowCurrentText(int idx,int count)
    {
        for (int i= idx; i< idx+count; i++)
        {
            Debug.Log("ㅇㅇ : " + i);
            guideNpcContextTxt.text = npcTextList[i];
            yield return new WaitForSeconds(3);
        }
    }
}
