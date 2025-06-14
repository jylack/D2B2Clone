using DG.Tweening;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class UIAdmin : UIBase
{
    public UIAdmin Instance { get; private set; }



    private void Awake()
    {
        Instance = this;
    }



    public void OnCardHoverEntered(HoverEnterEventArgs args)
    {
        Manager.Instance.SoundMgr.PlaySfx(ScDefine.ScSound.SimpleNotification);

        Transform target = args.interactableObject.transform;
        target.DOLocalMoveZ(-0.5f, 0.2f).SetLink(target.gameObject);
    }

    public void OnCardHoverExited(HoverExitEventArgs args)
    {
        Transform target = args.interactableObject.transform;
        target.DOLocalMoveZ(0f, 0.2f).SetLink(target.gameObject);
    }
}
