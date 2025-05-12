using UnityEngine;

public class ScRoad : MonoBehaviour
{
    private const float RoadLength = 4f;

    [SerializeField] private GameObject roadPartPrefab;
    [SerializeField] private GameObject roadEdgePrefab;
    [SerializeField] private int middleSegmentCount = 1;



    private void Start()
    {
        GameObject roadStart = Instantiate(roadEdgePrefab, transform);

        if (middleSegmentCount > 0)
        {
            (..middleSegmentCount).ForEach(i =>
            {
                GameObject road = Instantiate(roadPartPrefab, transform);
                road.transform.localPosition = (i + 1) * RoadLength * -Vector3.forward;
            });
        }

        //Vector3 roadEndPos = roadStart.transform.position + (-Vector3.forward * 4 * (middleSegmentCount + 1));
        //GameObject roadEnd = Instantiate(roadEdgePrefab, roadEndPos, Quaternion.LookRotation(-transform.forward, Vector3.up), transform);
        GameObject roadEnd = Instantiate(roadEdgePrefab, transform);
        roadEnd.transform.localPosition = (middleSegmentCount + 1) * RoadLength * -Vector3.forward;
        roadEnd.transform.Rotate(Vector3.up, 180f);

        //roadEnd.transform.Rotate(Vector3.up, 180f);
    }

    private void OnDrawGizmos()
    {
        Vector3 startPos = transform.position;
        startPos.y += 0.1f;
        startPos += transform.forward * 2;

        Vector3 endPos = startPos + (-transform.forward * (4 * middleSegmentCount + (4 * 2)));

        Gizmos.color = Color.green;
        Gizmos.DrawLine(startPos, endPos);
    }
}
