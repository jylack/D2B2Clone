using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ScMirrorPlayerHsy : MonoBehaviour
{
    [SerializeField] private XRRayInteractor rayInteractor;
    [SerializeField] private XRInteractorLineVisual lineVisual;
    [SerializeField] private Transform carmeraTranse;

    private GameObject hoveredNpc;
    private RayDetectMan hoveredDetectMan;
    private bool isMirrorHovered;
    public int findChildCount { get; private set; }
    private bool isNpcHovered;
    public bool isGamePlaying;
    public event Action<int> findChildChildEvent;


    public void ConnectPlayerTriggerEvent()
    {
        Manager.Instance.InputMgr.OnTriggerPerform += InputMgr_OnTriggerPerform;
    }
    public void DisConnectPlayerTriggerEvent()
    {
        Manager.Instance.InputMgr.OnTriggerPerform -= InputMgr_OnTriggerPerform;
    }

    private void LateUpdate()
    {
        if (isGamePlaying == false)
        {
            return;
        }
        Debug.Log("isMirrorHovered : " + isMirrorHovered);
        Debug.Log("isNpcHovered : " + isNpcHovered);
        if (isMirrorHovered || isNpcHovered)
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
        //Vector3 startPoint = rayInteractor.attachTransform.position;
        Vector3 startPoint = rayInteractor.transform.position;
        Vector3 dir = rayInteractor.attachTransform.forward;
        float distance = lineVisual.lineLength;

        Gizmos.DrawLine(startPoint, startPoint + (dir * distance));

        if (Physics.Raycast(startPoint, dir, out RaycastHit mirrorHit, distance, ScDefine.Layer.MirrorMask))
        {
            Vector3 mirrorDir = mirrorHit.normal;
            Vector3 targetDir = Vector3.Reflect(dir, mirrorDir);

            Vector3 mirrorStartPoint = mirrorHit.point;
            Gizmos.DrawLine(mirrorStartPoint, mirrorStartPoint + (targetDir * distance));
        }
    }



    public void OnHoverEnter(HoverEnterEventArgs args)
    {
        Debug.Log("이름 : " + args.interactableObject.transform.name);
        if ((args.interactableObject.interactionLayers & ScDefine.InteractionLayer.MirrorMask) != 0)
        {
            isMirrorHovered = true;
        }
        else if ((args.interactableObject.interactionLayers & ScDefine.InteractionLayer.NpcMask) != 0)
        {
            isNpcHovered = true;
        }
    }

    public void OnHoverExit(HoverExitEventArgs args)
    {
        if ((args.interactableObject.interactionLayers & ScDefine.InteractionLayer.MirrorMask) != 0)
        {
            isMirrorHovered = false;
            hoveredDetectMan?.SetOutlineVisible(false);
            hoveredDetectMan = null;
        }
        else if ((args.interactableObject.interactionLayers & ScDefine.InteractionLayer.NpcMask) != 0)
        {
            isNpcHovered = false;
            hoveredDetectMan?.SetOutlineVisible(false);
            hoveredDetectMan = null;
        }
    }



    private void InputMgr_OnTriggerPerform()
    {
        if (hoveredDetectMan != null)
        {
            hoveredDetectMan.DoSomething();
            findChildChildEvent.Invoke(++findChildCount);
        }
    }

    private bool Detect(out RaycastHit hitInfo, int layerMask)
    {
        hitInfo = default;
        //Vector3 startPoint = rayInteractor.attachTransform.position;
        Vector3 startPoint = rayInteractor.attachTransform.position;
        Vector3 dir = rayInteractor.attachTransform.forward;
        float distance = lineVisual.lineLength;

        if (Physics.Raycast(startPoint, dir, out RaycastHit mirrorHit, distance, ScDefine.Layer.MirrorMask))
        {
            Vector3 mirrorStartPoint = mirrorHit.point;
            Vector3 mirrorDir = mirrorHit.normal;
            Vector3 targetDir = Vector3.Reflect(dir, mirrorDir);
            Debug.DrawRay(mirrorHit.point, mirrorHit.normal * 5f, Color.magenta); // 진짜 법선
            Debug.DrawRay(mirrorHit.point, mirrorHit.transform.forward * 5f, Color.blue);

            if (Physics.Raycast(mirrorStartPoint, targetDir, out hitInfo, distance))
            {
                return true;
            }
        }
        else if (Physics.Raycast(startPoint, dir, out hitInfo, distance, layerMask))
        {
            return true;
        }

        return false;
    }
}
