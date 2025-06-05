using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ScCh3Player : ScObjectBase
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private AudioSource audioSource;
    
    private Outline lastOutline;
    private ScCh3Npc hoveredNpc;


    
    private void Start()
    {
        Manager.Instance.InputMgr.OnTriggerPerform += InputMgrOnOnTriggerPerform;
    }

    private void OnDestroy()
    {
        Manager.Instance.InputMgr.OnTriggerPerform -= InputMgrOnOnTriggerPerform;
    }


    
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
        hoveredNpc?.Select();
    }
}