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
    public int findChildCount { get; set; }
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
        // 거울에 먼저 Raycast
        if (Physics.Raycast(startPoint, dir, out RaycastHit mirrorHit, distance, ScDefine.Layer.MirrorMask))
        {
            // 거울 컴포넌트 확인
            ScMirror mirror = mirrorHit.collider.GetComponent<ScMirror>();
            if (mirror == null) return false;

            // 거울에 붙은 카메라 확인
            Camera mirrorCamera = mirror.mirrorCamTransform.GetComponent<Camera>();
            if (mirrorCamera == null) return false;

            // 충돌 위치를 거울 로컬 기준으로 변환
            Vector3 mirrorLocalHit = mirrorHit.collider.transform.InverseTransformPoint(mirrorHit.point);

            // 로컬 좌표를 Viewport 좌표로 변환
            Vector2 uv = new Vector2
            (
                1f - (mirrorLocalHit.x / mirrorHit.collider.bounds.size.x + 0.5f),
                mirrorLocalHit.y / mirrorHit.collider.bounds.size.y + 0.5f
            );

            // 거울 카메라 기준으로 반사된 Ray 생성
            Ray reflectedRay = mirrorCamera.ViewportPointToRay(new Vector3(uv.x, uv.y, 0));
            Debug.DrawRay(reflectedRay.origin, reflectedRay.direction * distance, Color.magenta);
            if (Physics.Raycast(reflectedRay, out hitInfo, distance))
            {
                if (hitInfo.collider.gameObject.layer == ScDefine.Layer.NpcIndex)
                {
                    Debug.Log($"Hit: {hitInfo.collider.gameObject.name}");
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        if (Physics.Raycast(startPoint, dir, out hitInfo, distance, layerMask))
        {
            return true;
        }
        return false;
    }
}
