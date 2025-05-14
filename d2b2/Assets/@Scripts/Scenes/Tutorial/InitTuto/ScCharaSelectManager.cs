using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using static UnityEngine.EventSystems.EventTrigger;

public class ScCharaSelectManager : MonoBehaviour
{
    [SerializeField] GameObject ConfirmPopUp;
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
                tempDetectMan.SetOutline(true);

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
            selectCharacter.WalkForward(this);
        }
    }

    public void OpenConfirmPopUp()
    {
        Debug.Log("나오쇼");
        ConfirmPopUp.SetActive(true);
    }

    public void ConfirmSelect()
    {
        TutorialManager.Instance.playerEntity.guideCharacter = selectCharacter.GetCharaId();
        Manager.Instance.DbMgr.Save(TutorialManager.Instance.playerEntity.nickName, TutorialManager.Instance.playerEntity).Forget();
        ConfirmPopUp.SetActive(false);
        Manager.Instance.SceneMgr.LoadScene("MoveTutorial");
    }

    public void CancelSelect()
    {
        ConfirmPopUp.SetActive(false);
        selectCharacter.WalkBack();
        selectCharacter = null;
    }
}
