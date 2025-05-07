using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.XR;
public delegate void HandPositionChangedHandler(Vector3 position);
public partial class ScPlayerCtrl : MonoBehaviour
{
    [SerializeField] bool isLeftHandUp;
    [SerializeField] bool isRightHandUp;
    [SerializeField] bool isHandUp;
    [SerializeField] bool isHeadRotation;

    public Transform headTransform; // 머리 위치 기준

    private void OnLeftHandUp(Vector3 handPosition)
    {
        float currentHeadY = headTransform.position.y; //  매번 현재 머리 높이 가져오기
        isLeftHandUp = handPosition.y > currentHeadY;
        isHandUp = isLeftHandUp;
        // Debug.Log($"[왼손] 손 위치 Y: {handPosition.y:F2}, 머리 Y: {currentHeadY:F2}, 위? {isLeftHandUp}");
    }

    private void OnRightHandUp(Vector3 handPosition)
    {
        float currentHeadY = headTransform.position.y; //  매번 현재 머리 높이 가져오기
        isRightHandUp = handPosition.y > currentHeadY;
        isHandUp = isRightHandUp;
        // Debug.Log($"[오른손] 손 위치 Y: {handPosition.y:F2}, 머리 Y: {currentHeadY:F2}, 위? {isRightHandUp}");
    }
    private void OnHeadRotation(Quaternion headRotation)
    {
        float yawAngle = headTransform.transform.rotation.y;

        // 3. 회전 각도는 0~360이므로, 180 넘어가면 음수로 변환
        if (yawAngle > 180f)
            yawAngle -= 360f;


        // 4. 예: 좌우 30도 이상 돌렸을 때
        if (Mathf.Abs(yawAngle) > 30)
        {
            Debug.Log("---고개를 좌우로 많이 돌림!");
        }

        // 1. 오른쪽 또는 왼쪽을 봤을 때
        // 2. 3초 제한시간 시작
        // 3. 제한 시간 안에 반대 회전값 달성해야 함.
        // 4. 

    }

}
