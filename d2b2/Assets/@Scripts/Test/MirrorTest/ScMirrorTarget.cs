using UnityEngine;

public class ScMirrorTarget : MonoBehaviour
{
    private Outline outline;



    private void Awake()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;
    }
}
