using UnityEngine;
using UnityEngine.InputSystem;

public delegate void OnHeadRotatingPerformHandler(Quaternion rotation);
public delegate void OnHeadRotatingCancelHandler();
public delegate void OnHandPositionChangedHandler(Vector3 position);

public class InputManager : MonoBehaviour
{
    public event OnHeadRotatingPerformHandler OnHeadRotatingPerform;
    public event OnHeadRotatingCancelHandler OnHeadRotatingCancel;
    public event HandPositionChangedHandler OnLeftHandPositionChanged;
    public event HandPositionChangedHandler OnRightHandPositionChanged;

    XRIDefaultInputActions inputActions;



    private void Awake()
    {
        inputActions = new XRIDefaultInputActions();
    }

    private void Start()
    {
        inputActions.Enable();

        inputActions.XRIHead.Rotation.performed += Rotation_performed;
        inputActions.XRIHead.Rotation.canceled += Rotation_canceled;
        inputActions.XRILeftHand.Position.performed += LeftHandPosition_performed;
        inputActions.XRIRightHand.Position.performed += RightHandPosition_performed;
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

    private void Rotation_performed(InputAction.CallbackContext obj)
    {
        var rotation = obj.ReadValue<Quaternion>();
        OnHeadRotatingPerform?.Invoke(rotation);
    }

    private void Rotation_canceled(InputAction.CallbackContext obj)
    {
        OnHeadRotatingCancel?.Invoke();
    }
}