using UnityEngine;

public class ScRoadCrosswalk : MonoBehaviour
{
    [SerializeField] private Collider coll;



    public void OnRedLightActivated()
    {
        coll.enabled = false;
    }

    public void OnGreenLightActivated()
    {
        coll.enabled = true;
    }
}
