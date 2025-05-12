using UnityEngine;

public class ScRoad : MonoBehaviour
{
    private const float RoadLength = 4f;

    [SerializeField] private GameObject roadPartPrefab;
    [SerializeField] private GameObject roadEdgePrefab;
    [SerializeField] private int middleSegmentCount = 1;



    private void Start()
    {
        gameObject.DestroyAllChildren();

        // 시작점
        GameObject roadStart = Instantiate(roadEdgePrefab, transform);

        // 중간
        if (middleSegmentCount > 0)
        {
            (..middleSegmentCount).ForEach(i =>
            {
                GameObject road = Instantiate(roadPartPrefab, transform);
                road.transform.localPosition = (i + 1) * RoadLength * -Vector3.forward;
            });
        }

        // 끝점
        GameObject roadEnd = Instantiate(roadEdgePrefab, transform);
        roadEnd.transform.localPosition = (middleSegmentCount + 1) * RoadLength * -Vector3.forward;
        roadEnd.transform.Rotate(Vector3.up, 180f);
    }

    private void OnDrawGizmos()
    {
        Vector3 startPos = transform.position;
        startPos.y += 0.1f;
        startPos += transform.forward * 2;

        float edgeLength = RoadLength * 2;
        Vector3 endPos = startPos + (-transform.forward * (RoadLength * middleSegmentCount + edgeLength));

        Gizmos.color = Color.green;
        Gizmos.DrawLine(startPos, endPos);
    }
}
