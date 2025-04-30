using UnityEngine;
using UnityEngine.InputSystem;

public delegate void OnHeadRotatingPerformHandler(Quaternion rotation);
public delegate void OnHeadRotatingCancelHandler();

public class InputManager : MonoBehaviour
{
    public event OnHeadRotatingPerformHandler OnHeadRotatingPerform;
    public event OnHeadRotatingCancelHandler OnHeadRotatingCancel;

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