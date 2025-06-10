using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTutorialManager : MonoBehaviour
{
    [SerializeField] ScGuide guide;
    [SerializeField] ScNotifyFairy notifyFairy;
    [SerializeField] GameObject walkObjs;
    [SerializeField] GameObject lookAroundObjs;
    [SerializeField] ScLookAroundRegion lookAround;
    [SerializeField] ScDefine.ScScene sceneName;
    void Start()
    {
        //guide.InstantiateGuide(TutorialManager.Instance.playerEntity.guideCharacter);
    }

    public void WalkSuccess()
    {
        walkObjs.SetActive(false);
        lookAroundObjs.SetActive(true);
        CheckLookAroundComplete().Forget();
    }

    public void LookAroundSuccess()
    {
        Manager.Instance.SceneMgr.LoadScene(sceneName);
    }

    private async UniTaskVoid CheckLookAroundComplete()
    {
        await UniTask.WaitUntil(() => lookAround.lookAroundMissionClear);
        LookAroundSuccess();
    }
 }
