using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ScMirrorPlayer : MonoBehaviour
{
    [SerializeField] private XRRayInteractor rayInteractor;
    [SerializeField] private XRInteractorLineVisual lineVisual;

    private GameObject hoveredNpc;
    private RayDetectMan hoveredDetectMan;
    private bool isMirrorHovered;



    private void Start()
    {
        Manager.Instance.InputMgr.OnTriggerPerform += InputMgr_OnTriggerPerform;
    }

    private void LateUpdate()
    {
        if (isMirrorHovered)
        {
            if (Detect(out RaycastHit hit, ScDefine.Layer.NpcMask))
            {
                if (hoveredNpc == null)
                {
                    var tempDetectMan = hit.transform.GetComponent<RayDetectMan>();
                    tempDetectMan?.SetOutlineVisible(true);
                    hoveredDetectMan = tempDetectMan;
                    hoveredNpc = hit.transform.gameObject;
                }
                else if (hoveredNpc != hit.transform.gameObject)
                {
                    var tempDetectMan = hit.transform.GetComponent<RayDetectMan>();
                    tempDetectMan?.SetOutlineVisible(true);
                    hoveredDetectMan?.SetOutlineVisible(false);
                    hoveredDetectMan = tempDetectMan;
                    hoveredNpc = hit.transform.gameObject;
                }
            }
            else
            {
                hoveredDetectMan?.SetOutlineVisible(false);
                hoveredDetectMan = null;
                hoveredNpc = null;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 startPoint = rayInteractor.attachTransform.position;
        Vector3 dir = rayInteractor.attachTransform.forward;
        float distance = lineVisual.lineLength;

        Gizmos.DrawLine(startPoint, startPoint + (dir * distance));

        if (Physics.Raycast(startPoint, dir, out RaycastHit mirrorHit, distance, ScDefine.Layer.MirrorMask))
        {
            Vector3 mirrorDir = mirrorHit.transform.forward;
            Vector3 targetDir = Vector3.Reflect(dir, mirrorDir);

            Vector3 mirrorStartPoint = mirrorHit.point;
            Gizmos.DrawLine(mirrorStartPoint, mirrorStartPoint + (targetDir * distance));
        }
    }



    public void OnHoverEnter(HoverEnterEventArgs args)
    {
        if (args.interactableObject.interactionLayers == ScDefine.InteractionLayer.MirrorMask)
        {
            print("Mirror Hover Enter");
            isMirrorHovered = true;
        }
    }

    public void OnHoverExit(HoverExitEventArgs args)
    {
        if (args.interactableObject.interactionLayers == ScDefine.InteractionLayer.MirrorMask)
        {
            print("Hover Exit");
            isMirrorHovered = false;

            hoveredDetectMan?.SetOutlineVisible(false);
            hoveredDetectMan = null;
        }
    }



    private void InputMgr_OnTriggerPerform()
    {
        hoveredDetectMan?.DoSomething();
    }

    private bool Detect(out RaycastHit hitInfo, int layerMask)
    {
        hitInfo = default;
        Vector3 startPoint = rayInteractor.attachTransform.position;
        Vector3 dir = rayInteractor.attachTransform.forward;
        float distance = lineVisual.lineLength;

        if (Physics.Raycast(startPoint, dir, out RaycastHit mirrorHit, distance, ScDefine.Layer.MirrorMask))
        {
            Vector3 mirrorDir = mirrorHit.transform.forward;
            Vector3 targetDir = Vector3.Reflect(dir, mirrorDir);

            Vector3 mirrorStartPoint = mirrorHit.point;

            if (Physics.Raycast(mirrorStartPoint, targetDir, out hitInfo, distance, layerMask))
            {
                return true;
            }
        }

        return false;
    }
}
