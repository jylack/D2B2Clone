using UnityEngine;

public class ScRoadCrosswalkDetect : MonoBehaviour
{
    [SerializeField] private ScRoadCrosswalk parent;



    private void OnTriggerEnter(Collider other)
    {
        parent?.OnDetectEnted(other);
    }

    private void OnTriggerExit(Collider other)
    {
        parent?.OnDetectExited(other);
    }
}
