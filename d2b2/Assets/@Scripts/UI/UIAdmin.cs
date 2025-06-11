using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class UIAdmin : UIBase
{
    public UIAdmin Instance { get; private set; }

    [SerializeField] private GameObject inputPanel;
    [SerializeField] private GameObject chapterButtonPanel;
    [SerializeField] private TMP_InputField inputId;
    [SerializeField] private TMP_InputField inputPassword;

    public bool IsLoaded => ScAdminScene.Instance?.IsLoaded ?? false;

    private Transform hoveredChapterCard;



    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Manager.Instance.InputMgr.OnTriggerPerform += InputMgr_OnTriggerPerform;
    }

    private void OnDestroy()
    {
        Manager.Instance.InputMgr.OnTriggerPerform -= InputMgr_OnTriggerPerform;
    }



    public void OnCardHoverEntered(HoverEnterEventArgs args)
    {
        if (!IsLoaded)
            return;

        hoveredChapterCard = args.interactableObject.transform;
        args.interactableObject.transform.DOLocalMoveZ(-0.5f, 0.2f);
    }

    public void OnCardHoverExited(HoverExitEventArgs args)
    {
        if (!IsLoaded)
            return;

        args.interactableObject.transform.DOLocalMoveZ(0f, 0.2f);

        if (hoveredChapterCard == args.interactableObject.transform)
            hoveredChapterCard = null;
    }



    private void InputMgr_OnTriggerPerform()
    {
        if (hoveredChapterCard == null)
            return;

        ScAdminScene.Instance.SetIsLoadedFalse();
        hoveredChapterCard.GetComponent<UIChapterCard>().Select();
    }
}
