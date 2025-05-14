using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTutorialManager : MonoBehaviour
{
    [SerializeField] GameObject guideObject;
    [SerializeField] GameObject[] guides;
    [SerializeField] ScNotifyFairy notifyFairy;
    void Start()
    {
        Instantiate(guides[(int)TutorialManager.Instance.playerEntity.guideCharacter - 1], guideObject.transform);
        notifyFairy.ChangeText("팔을 휘둘러 캐릭터를 향해 걸어보세요!");
    }
}
