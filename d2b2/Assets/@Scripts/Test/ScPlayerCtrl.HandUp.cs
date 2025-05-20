using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
// public delegate void HandPositionChangedHandler(Vector3 position);
public partial class ScPlayerCtrl : MonoBehaviour
{
    [SerializeField] public bool isLeftHandUp { get; private set; } // 왼쪽 손을 들었는지
    [SerializeField] public bool isRightHandUp { get; private set; }// 오른쪽 손을 들었는지
    [SerializeField] public bool isHandUp { get; private set; }// 손을 들었는지
    [SerializeField] public bool isLookAround { get; private set; }// 좌우를 보앗는지
    [SerializeField] public float currentLookAngle { get; private set; }// 현재 보고있는 각도
    [SerializeField] public bool isLookRight{ get; private set; }// 왼쪽을 보았는지
    [SerializeField] public bool isLookLeft { get; private set; }// 오른족을 보았는지
    [Header("설정값")]
    [SerializeField] int lookAroundMaxAngle = 50;


    public Transform headTransform; // 머리 위치 기준
    public Transform cameraOffset; // 머리 위치 기준

    public Coroutine coroutine;
    private void OnLeftHandUp(Vector3 handPosition)
    {
        float currentHeadY = headTransform.position.y; //  매번 현재 머리 높이 가져오기
        isLeftHandUp = handPosition.y > currentHeadY;
        isHandUp = isLeftHandUp;
    }

    private void OnRightHandUp(Vector3 handPosition)
    {
        float currentHeadY = headTransform.position.y; //  매번 현재 머리 높이 가져오기
        isRightHandUp = handPosition.y > currentHeadY;
        isHandUp = isRightHandUp;
        //Debug.Log("Camera Y height: " + Camera.main.transform.position.y);
    }
    public IEnumerator ChekLookAround()
    {
        // Debug.Log("코루틴 시작");
        float timer = 0f;
        isLookAround = false;
        isLookRight = false;
        isLookLeft = false;

        while (true)
        {
            // Debug.Log("ㅎㅇ");
            // Debug.DrawRay(transform.position, transform.forward,Color.red,100f);
            // Debug.DrawRay(transform.position, headTransform.forward, Color.green, 100f);
            Quaternion relativeRot = Quaternion.Inverse(transform.rotation) * headTransform.rotation;
            currentLookAngle = relativeRot.eulerAngles.y;
            if (currentLookAngle > 180f) currentLookAngle -= 360f; // -180 ~ 180으로 변환

            // [2] 각도 표시

            if (currentLookAngle > lookAroundMaxAngle)
            {
                isLookRight = true;
                // Debug.Log("오른쪽 봄");
            }
            else
            {
                isLookRight = false;
            }
            if (currentLookAngle < -lookAroundMaxAngle)
            {
                isLookLeft = true;
                // Debug.Log("왼쪽 봄");
            }
            else
            {
                isLookLeft = false;
            }

            if (isLookLeft && isLookRight)
            {
                isLookAround = true;
                Debug.Log("고개 좌우 다 돌림!");
                //break;
            }

            timer += Time.deltaTime;
            yield return null;
        }
    }
    IEnumerator RecenterView()
    {
        yield return new WaitForSeconds(2) ; // vr의 값이 들어갔을때 실행되게끔 대기 , null 또는 1초하면 못가져옴
        // 현재 시점의 yaw 방향만 추출
        Vector3 forward = headTransform.forward;
        forward.y = 0;
        forward.Normalize();

        // 현재 시점을 기준으로 회전 각도 계산
        float angle = Vector3.SignedAngle(forward, Vector3.forward, Vector3.up);
        Debug.Log("angle : " + angle);

        // XR Origin을 반대로 회전시켜서 정면이 맞춰지게 함
        cameraOffset.Rotate(0, angle, 0);
    }

}
