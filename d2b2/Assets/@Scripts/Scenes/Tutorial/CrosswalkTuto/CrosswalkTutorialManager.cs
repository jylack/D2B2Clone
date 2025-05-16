using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosswalkTutorialManager : MonoBehaviour
{
    [SerializeField] ScGuideInstance guide;
    [SerializeField] ScHandUpRegion handUp;

    void Start()
    {
        guide.InstantiateGuide(TutorialManager.Instance.playerEntity.guideCharacter);
    }

    private async UniTaskVoid HandUpChecker()
    {
        await UniTask.WaitUntil(() => handUp.inHandUpRegion);
        await UniTask.WaitUntil(() => handUp.handUpMissionClear);
    }
}
