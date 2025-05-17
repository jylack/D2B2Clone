using UnityEngine;

public class ScRoadCrosswalk : MonoBehaviour
{
    [SerializeField] private Collider entireCollider;
    [SerializeField] private Collider rightLaneCollider;


    
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
}
