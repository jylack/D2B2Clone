using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using static UnityEngine.EventSystems.EventTrigger;

public class ScCharaSelectManager : MonoBehaviour
{
    [SerializeField] GameObject ConfirmPopUp;
    [SerializeField] string sceneName;
    ScTutCharacter hover;
    ScTutCharacter selectCharacter;

    private void Awake()
    {
        Manager.Instance.InputMgr.OnTriggerPerform += InputMgr_OnTriggerPerform;
    }

    private void OnDestroy()
    {
        Manager.Instance.InputMgr.OnTriggerPerform -= InputMgr_OnTriggerPerform;
    }

    public void OnHoverEnter(HoverEnterEventArgs args)
    {
        if (!selectCharacter)
        {
            var tempDetectMan = args.interactableObject.transform.GetComponent<ScTutCharacter>();

            if (tempDetectMan != null)
            {
                tempDetectMan.SetOutline(true);
                tempDetectMan.SetOutlineColor(Color.yellow);
            }

            hover?.SetOutline(false);
            hover = tempDetectMan;
        }
    }

    public void OnHoverExit(HoverExitEventArgs args)
    {
        if (!selectCharacter)
        {
            var tempDetectMan = args.interactableObject.transform.GetComponent<ScTutCharacter>();

            if (tempDetectMan != null)
                tempDetectMan.SetOutline(false);

            if (hover == tempDetectMan)
                hover = null;
        }
    }
    private void InputMgr_OnTriggerPerform()
    {
        if(!selectCharacter && hover)
        {
            selectCharacter = hover;
            print("trigger on");
            selectCharacter.SetOutlineColor(Color.red);
            selectCharacter.WalkForward(this);
        }
    }

    public void OpenConfirmPopUp()
    {
        ConfirmPopUp.SetActive(true);
    }

    public void ConfirmSelect()
    {
        TutorialManager.Instance.playerEntity.guideCharacter = selectCharacter.GetCharaId();
        Manager.Instance.DbMgr.Save(TutorialManager.Instance.playerEntity.nickName, TutorialManager.Instance.playerEntity).Forget();
        ConfirmPopUp.SetActive(false);
        Manager.Instance.SceneMgr.LoadScene(sceneName);
    }

    public void CancelSelect()
    {
        ConfirmPopUp.SetActive(false);
        selectCharacter.WalkBack();
        selectCharacter = null;
    }
}
