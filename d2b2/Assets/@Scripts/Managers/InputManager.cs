using System;
using UnityEngine;
using UnityEngine.InputSystem;

public delegate void OnHeadPositionChangedHandler(Vector3 position);
public delegate void OnHeadRotationChangedHandler(Quaternion rotation);
public delegate void OnHandPositionChangedHandler(Vector3 position);
public delegate void OnLeftStickMoveHandler(bool stickMoving);
//public delegate void OnRightStickMoveHandler(bool stickMoving);

public class InputManager : MonoBehaviour
{
    public event OnHeadPositionChangedHandler OnHeadPositionChanged;
    public event OnHeadRotationChangedHandler OnHeadRotationChanged;
    public event OnHandPositionChangedHandler OnLeftHandPositionChanged;
    public event OnHandPositionChangedHandler OnRightHandPositionChanged;
    public event OnLeftStickMoveHandler OnLeftStickMove;
    //public event OnRightStickMoveHandler OnRightStickMove;

    public event Action OnTriggerPerform;
    public event Action OnTriggerCancel;

    private XRIDefaultInputActions inputActions;



    private void Awake()
    {
        inputActions = new XRIDefaultInputActions();
    }

    private void Start()
    {
        inputActions.Enable();

        inputActions.XRIHead.Position.performed += HeadPosition_performed;
        inputActions.XRIHead.Rotation.performed += HeadRotation_performed;
        inputActions.XRILeftHand.Position.performed += LeftHandPosition_performed;
        inputActions.XRIRightHand.Position.performed += RightHandPosition_performed;
        inputActions.XRIRightHandInteraction.Activate.performed += Select_performed;
        inputActions.XRIRightHandInteraction.Activate.canceled += Select_canceled;

        inputActions.XRILeftHandLocomotion.Move.performed += LeftStickMove_performed;
        inputActions.XRILeftHandLocomotion.Move.canceled += LeftStickMove_canceled;
        //inputActions.XRIRightHandLocomotion.Move.performed += RightStickMove_performed;
        //inputActions.XRIRightHandLocomotion.Move.canceled += RightStickMove_canceled;
    }

    //우측 스틱 회전시 이벤트 발생
    //private void RightStickMove_performed(InputAction.CallbackContext obj)
    //{
    //    bool stickMoving = obj.ReadValue<Vector2>().sqrMagnitude > 0f;
    //    OnRightStickMove?.Invoke(stickMoving);
    //}

    //private void RightStickMove_canceled(InputAction.CallbackContext obj)
    //{
    //    OnRightStickMove?.Invoke(false);
    //}


    private void LeftStickMove_performed(InputAction.CallbackContext obj)
    {
        bool stickMoving = obj.ReadValue<Vector2>().sqrMagnitude > 0f;
        OnLeftStickMove?.Invoke(stickMoving);
    }

    private void LeftStickMove_canceled(InputAction.CallbackContext obj)
    {
        OnLeftStickMove?.Invoke(false);
    }

    private void HeadPosition_performed(InputAction.CallbackContext obj)
    {
        Vector3 pos = obj.ReadValue<Vector3>();
        OnHeadPositionChanged?.Invoke(pos);
    }
    
    private void HeadRotation_performed(InputAction.CallbackContext obj)
    {
        Quaternion rotation = obj.ReadValue<Quaternion>();
        OnHeadRotationChanged?.Invoke(rotation);
    }
    
    private void LeftHandPosition_performed(InputAction.CallbackContext obj)
    {
        Vector3 pos = obj.ReadValue<Vector3>();
        OnLeftHandPositionChanged?.Invoke(pos);
    }

    private void RightHandPosition_performed(InputAction.CallbackContext obj)
    {
        Vector3 pos = obj.ReadValue<Vector3>();
        OnRightHandPositionChanged?.Invoke(pos);
    }

    private void Select_performed(InputAction.CallbackContext obj)
    {
        OnTriggerPerform?.Invoke();
    }

    private void Select_canceled(InputAction.CallbackContext obj)
    {
        OnTriggerCancel?.Invoke();
    }
}