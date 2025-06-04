using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.XR.CoreUtils;
using UnityEngine;

public class CrosswalkTutorialManager : MonoBehaviour
{
    [SerializeField] GameObject clearMessage;
    [SerializeField] ScGuide guide;
    [SerializeField] ScHandUpRegion handUp;
    [SerializeField] XROrigin playerXR;
    [SerializeField] Transform player;
    [SerializeField] ScDefine.ScScene sceneName;
    Vector3 startPos;
    bool missionClear = false;
    void Start()
    {
        clearMessage.SetActive(false);
        Debug.Log("Tuto ins " + TutorialManager.Instance.playerEntity);
        if(TutorialManager.Instance.playerEntity != null)
        {
            guide.InstantiateGuide(TutorialManager.Instance.playerEntity.guideCharacter);
        }
        startPos = player.position;
        startPos.y = playerXR.GetComponent<CharacterController>().height;
        HandUpChecker().Forget();
    }

    private async UniTaskVoid HandUpChecker()
    {
        Debug.Log("Checker start");
        while (!missionClear)
        {
            Debug.Log("tp player");
            playerXR.MoveCameraToWorldLocation(startPos);
            await UniTask.WaitUntil(() => handUp.inHandUpRegion);
            Debug.Log("in region");

            await UniTask.WaitUntil(() => !handUp.inHandUpRegion);
            Debug.Log("out region");
            missionClear = handUp.handUpMissionClear;
            Debug.Log("clear " + missionClear);
        }
        TutorialClear();
    }

    private void TutorialClear() 
    {
        Debug.Log("Tuto Wan");
        clearMessage.SetActive(true);
    }

    public void MoveToTutoStart()
    {
        Manager.Instance.SceneMgr.LoadScene(sceneName);
    }
}
