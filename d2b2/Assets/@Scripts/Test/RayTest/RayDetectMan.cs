using TMPro;
using UnityEngine;

public class RayDetectMan : MonoBehaviour
{

    private Outline outline;
    private int count;



    private void Awake()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;
    }



    public void SetOutlineVisible(bool isVisible)
    {
        if (outline != null)
            outline.enabled = isVisible;
    }

    public void DoSomething()
    {
        gameObject.SetActive(false);
    }
}
