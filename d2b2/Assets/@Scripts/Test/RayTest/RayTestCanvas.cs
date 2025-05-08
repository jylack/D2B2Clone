using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RayTestCanvas : MonoBehaviour
{
    private void Start()
    {

    }



    public void OnHoverEnter(HoverEnterEventArgs args)
    {

    }

    public void OnHoverExit(HoverExitEventArgs args)
    {
        print("OnHoverExit");
    }

    public void OnSelectEnter(SelectEnterEventArgs args)
    {
        print("OnSelectEnter");
    }

    public void OnSelectExit(SelectExitEventArgs args)
    {
        print("OnSelectExit");
    }
}
