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
    [SerializeField] private float num;

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

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.green;
    //    //Vector3 startPoint = rayInteractor.attachTransform.position;
    //    Vector3 startPoint = rayInteractor.transform.position;
    //    Vector3 dir = rayInteractor.attachTransform.forward;
    //    float distance = lineVisual.lineLength;

    //    Gizmos.DrawLine(startPoint, startPoint + (dir * distance));

    //    if (Physics.Raycast(startPoint, dir, out RaycastHit mirrorHit, distance, ScDefine.Layer.MirrorMask))
    //    {
    //        Vector3 mirrorDir = mirrorHit.normal;
    //        Vector3 targetDir = Vector3.Reflect(dir, mirrorDir);

    //        Vector3 mirrorStartPoint = mirrorHit.point;
    //        Gizmos.DrawLine(mirrorStartPoint, mirrorStartPoint + (targetDir * distance));
    //    }
    //}



    public void OnHoverEnter(HoverEnterEventArgs args)
    {
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

    Vector3 startPoint = rayInteractor.attachTransform.position;
    Vector3 dir = rayInteractor.attachTransform.forward;
    float distance = lineVisual.lineLength;

    // 1차 Ray: XR Ray → 거울 맞았는지
    if (Physics.Raycast(startPoint, dir, out RaycastHit mirrorHit, distance, ScDefine.Layer.MirrorMask))
    {
        ScMirror mirror = mirrorHit.collider.GetComponent<ScMirror>();
        if (mirror == null) return false;

        Camera mirrorCamera = mirror.mirrorCamTransform.GetComponent<Camera>();
        if (mirrorCamera == null) return false;

        Vector3 mirrorLocalHit = mirrorHit.collider.transform.InverseTransformPoint(mirrorHit.point);
        Vector2 uv = new Vector2(
            1f - (mirrorLocalHit.x / mirrorHit.collider.bounds.size.x + 0.5f),
            mirrorLocalHit.y / mirrorHit.collider.bounds.size.y + 0.5f
        );

        Ray reflectedRay = mirrorCamera.ViewportPointToRay(new Vector3(uv.x, uv.y, 0));
        Debug.DrawRay(reflectedRay.origin, reflectedRay.direction * distance, Color.magenta);

        if (Physics.Raycast(reflectedRay, out hitInfo, distance, layerMask))
        {
            return true;
        }
    }

    // 거울 안 맞으면 그냥 기본 Ray
    if (Physics.Raycast(startPoint, dir, out hitInfo, distance, layerMask))
    {
        return true;
    }

    return false;
}
    //private bool Detect(out RaycastHit hitInfo, int layerMask)
    //{
    //    hitInfo = default;
    //    //Vector3 startPoint = rayInteractor.attachTransform.position;
    //    Vector3 startPoint = rayInteractor.attachTransform.position;
    //    Vector3 dir = rayInteractor.attachTransform.forward;
    //    float distance = lineVisual.lineLength;
    //
    //    if (Physics.Raycast(startPoint, dir, out RaycastHit mirrorHit, distance, ScDefine.Layer.MirrorMask))
    //    {
    //        Vector3 mirrorStartPoint = mirrorHit.point;
    //        Vector3 mirrorDir = mirrorHit.normal;
    //        Vector3 targetDir = Vector3.Reflect(dir, mirrorDir);
    //        Debug.DrawRay(mirrorHit.point, mirrorHit.normal * 5f, Color.magenta); // 진짜 법선
    //        Debug.DrawRay(mirrorHit.point, mirrorHit.transform.forward * 5f, Color.blue);
    //
    //        if (Physics.Raycast(mirrorStartPoint, targetDir, out hitInfo, distance))
    //        {
    //            return true;
    //        }
    //    }
    //    else if (Physics.Raycast(startPoint, dir, out hitInfo, distance, layerMask))
    //    {
    //        return true;
    //    }
    //
    //    return false;
    //}
}
