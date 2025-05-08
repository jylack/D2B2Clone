using UnityEngine;

public class RayDetectMan : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        print(collision.gameObject.name);
    }
}
