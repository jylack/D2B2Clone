using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

// Assets/Resources/Prefabs/Ch3Player.prefab
// - Player
public class ScCh3Player : ScPlayerBase
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject xrOrigin;

    public override Camera MainCamera => mainCamera;

    private ScCh3Npc hoveredNpc;


        
    private void Start()
    {
        Manager.Instance.InputMgr.OnTriggerPerform += InputMgrOnOnTriggerPerform;
        Manager.Instance.GameMgr.SetPlayer(this);
    }

    private void OnDestroy()
    {
        Manager.Instance.InputMgr.OnTriggerPerform -= InputMgrOnOnTriggerPerform;
    }


    // Ray Interactor GameObject -> XR Ray Interactor Component
    public void OnOutlineHoverEnter(HoverEnterEventArgs args)
    {
        if (args.interactableObject.transform.TryGetComponent(out ScCh3Npc npc))
        {
            npc.OnRayHoverEnter();

            if (hoveredNpc != null && hoveredNpc != npc)
                hoveredNpc.OnRayHoverExit();
            
            hoveredNpc = npc;
        }
    }

    // Ray Interactor GameObject -> XR Ray Interactor Component
    public void OnOutlineHoverExit(HoverExitEventArgs args)
    {
        if (args.interactableObject.transform.TryGetComponent(out ScCh3Npc npc))
        {
            npc.OnRayHoverExit();
            
            if (npc == hoveredNpc)
                hoveredNpc = null;
        }
    }



    private void InputMgrOnOnTriggerPerform()
    {
        hoveredNpc?.TryCatchNpc();
    }
}