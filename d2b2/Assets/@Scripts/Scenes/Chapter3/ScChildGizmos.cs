using UnityEngine;

public class ScChildGizmos : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        (..transform.childCount).ForEach(i =>
        {
            Transform child = transform.GetChild(i);
            Gizmos.DrawSphere(child.transform.position + Vector3.up * 0.1f, 0.1f);
        });
    }
}
