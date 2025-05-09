using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ScMirrorPlayer : MonoBehaviour
{
    [SerializeField] private XRRayInteractor rayInteractor;



    private void Start()
    {
        Manager.Instance.InputMgr.OnTriggerPerform += InputMgr_OnTriggerPerform;
    }



    private void InputMgr_OnTriggerPerform()
    {
        Vector3 startPoint = rayInteractor.transform.position;
        Vector3 dir = rayInteractor.transform.forward;
        float distance = 5f;

        Debug.DrawLine(startPoint, startPoint + (dir * distance), Color.red);
    }
}
