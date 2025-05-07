using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
public delegate void HandPositionChangedHandler(Vector3 position);
public partial class PlayerCtrl : MonoBehaviour
{
    bool isLeftHandUp;
    bool isRightHandUp;

    public Transform headTransform; // 머리 위치 기준

    private void OnLeftHandUp(Vector3 handPosition)
    {
        float currentHeadY = headTransform.position.y; //  매번 현재 머리 높이 가져오기
        isLeftHandUp = handPosition.y > currentHeadY;
       // Debug.Log($"[왼손] 손 위치 Y: {handPosition.y:F2}, 머리 Y: {currentHeadY:F2}, 위? {isLeftHandUp}");
    }

    private void OnRightHandUp(Vector3 handPosition)
    {
        float currentHeadY = headTransform.position.y; //  매번 현재 머리 높이 가져오기
        isRightHandUp = handPosition.y > currentHeadY;
        //Debug.Log($"[오른손] 손 위치 Y: {handPosition.y:F2}, 머리 Y: {currentHeadY:F2}, 위? {isRightHandUp}");
    }
    private void OnHeadRotation(Quaternion rotation)
    {
        Quaternion temp = rotation * transform.rotation;
        //Debug.Log("머리 돌리기 : " + temp.y);
    }
}
