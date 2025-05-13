using UnityEditor;
using UnityEngine;

public class ScRoadGenerator : ScObjectBase
{
    private const float ModelSize = 4f;

    [SerializeField] private int segmentCount = 1;

    private bool isDelayCallAdded;



    private void OnValidate()
    {
#if UNITY_EDITOR
        if (isDelayCallAdded)
            return;

        isDelayCallAdded = true;

        EditorApplication.delayCall += () =>
        {
            if (this == null || transform == null) 
                return;

            isDelayCallAdded = false;

            while (transform.childCount > 0)
                DestroyImmediate(transform.GetChild(0).gameObject);

            // 시작점
            GameObject roadStart = InstantiateRoadEdge(transform);
            roadStart.transform.Rotate(Vector3.up, 180f);

            // 중간
            if (segmentCount > 0)
            {
                (..segmentCount).ForEach(i =>
                {
                    GameObject road = InstantiateRoadSegment(transform);
                    road.transform.localPosition = (i + 1) * ModelSize * Vector3.forward;
                });
            }

            // 끝점
            GameObject roadEnd = InstantiateRoadEdge(transform);
            roadEnd.transform.localPosition = (segmentCount + 1) * ModelSize * Vector3.forward;
        };
#endif
    }



    public GameObject InstantiateRoadEdge(Transform parent)
    {
        return ResourceManager.InstantiatePrefab("Prefabs/RoadEdge", parent);
    }

    public GameObject InstantiateRoadSegment(Transform parent)
    {
        return ResourceManager.InstantiatePrefab("Prefabs/RoadSegment", parent);
    }
}
