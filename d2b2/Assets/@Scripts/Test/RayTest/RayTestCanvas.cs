using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RayTestCanvas : MonoBehaviour
{
    private RayDetectMan hoveredDetectMan;



    private void Start()
    {
        Manager.Instance.InputMgr.OnTriggerPerform += InputMgr_OnTriggerPerform;
        Manager.Instance.InputMgr.OnTriggerCancel += InputMgr_OnTriggerCancel;
    }



    public void OnHoverEnter(HoverEnterEventArgs args)
    {
        print("Hover Enter");

        var tempDetectMan = args.interactableObject.transform.GetComponent<RayDetectMan>();

        if (tempDetectMan != null)
            tempDetectMan.SetOutlineVisible(true);

        hoveredDetectMan?.SetOutlineVisible(false);
        hoveredDetectMan = tempDetectMan;
    }

    public void OnHoverExit(HoverExitEventArgs args)
    {
        print("Hover Exit");

        var tempDetectMan = args.interactableObject.transform.GetComponent<RayDetectMan>();

        if (tempDetectMan != null)
            tempDetectMan.SetOutlineVisible(false);

        if (hoveredDetectMan == tempDetectMan)
            hoveredDetectMan = null;
    }

    private void InputMgr_OnTriggerPerform()
    {
        print("trigger on");
        hoveredDetectMan.DoSomething();
    }

    private void InputMgr_OnTriggerCancel()
    {
        //hoveredDetectMan.DoSomething();
    }
}
