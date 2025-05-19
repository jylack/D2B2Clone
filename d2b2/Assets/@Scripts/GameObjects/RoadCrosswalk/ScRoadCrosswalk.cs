using UnityEngine;
using UnityEngine.Events;

public class ScRoadCrosswalk : MonoBehaviour
{
    [SerializeField] private Collider entireCollider;
    [SerializeField] private Collider rightLaneCollider;
    [SerializeField] private UnityEvent OnCrosswalkEntered;
    [SerializeField] private UnityEvent OnCrosswalkExited;



    public void OnGreenLightActivatedBefore()
    {
        if (rightLaneCollider != null)
            rightLaneCollider.enabled = true;
    }
    
    public void OnGreenLightActivated()
    {
        entireCollider.enabled = true;
    }

    public void OnRedLightActivated()
    {
        entireCollider.enabled = false;
        
        if (rightLaneCollider != null)
            rightLaneCollider.enabled = false;
    }

    public void OnDetectEnted(Collider other)
    {
        OnCrosswalkEntered?.Invoke();
    }

    public void OnDetectExited(Collider other)
    {
        OnCrosswalkExited?.Invoke();
    }
}
