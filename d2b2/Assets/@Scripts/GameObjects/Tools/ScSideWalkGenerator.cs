using UnityEditor;
using UnityEngine;

public class ScSideWalkGenerator : ScObjectBase
{
    private const float ModelSize = 2f;

    [SerializeField] int horizontalSegmentCount;
    [SerializeField] int verticalSegmentCount;

    private bool isDelayCallAdded;
    private float TopPosZ => (verticalSegmentCount + 1) * ModelSize;
    private float RightPosX => (horizontalSegmentCount + 1) * ModelSize;



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

            InstantiateAllCorners();
            InstantiateAllEdges();
            InstantiateAllSegments();
        };
#endif
    }



    private void InstantiateAllCorners()
    {
        // ÁÂ»ó´Ü
        GameObject leftTopCorner = InstantiateCorner(transform);
        leftTopCorner.transform.localPosition = new Vector3(0f, 0f, TopPosZ);

        // ¿ì»ó´Ü
        GameObject rightTopCorner = InstantiateCorner(transform);
        rightTopCorner.transform.localPosition = new Vector3((1 + horizontalSegmentCount) * ModelSize, 0f, TopPosZ);
        rightTopCorner.transform.Rotate(Vector3.up, 90f);

        // ÁÂÇÏ´Ü
        GameObject leftBottomCorner = InstantiateCorner(transform);
        leftBottomCorner.transform.Rotate(Vector3.up, -90f);

        // ¿ìÇÏ´Ü
        GameObject rightBottomCorner = InstantiateCorner(transform);
        rightBottomCorner.transform.localPosition = new Vector3(RightPosX, 0f, 0f);
        rightBottomCorner.transform.Rotate(Vector3.up, 180f);
    }

    private void InstantiateAllEdges()
    {
        // »óÇÏ ¿§Áö
        if (horizontalSegmentCount > 0)
        {
            (..horizontalSegmentCount).ForEach(i =>
            {
                float posX = (i + 1) * ModelSize;

                GameObject topEdge = InstantiateEdge(transform);
                topEdge.transform.localPosition = new Vector3(posX, 0f, TopPosZ);
                topEdge.transform.Rotate(Vector3.up, 90f);

                GameObject bottomEdge = InstantiateEdge(transform);
                bottomEdge.transform.localPosition = new Vector3(posX, 0f, 0f);
                bottomEdge.transform.Rotate(Vector3.up, -90f);
            });
        }

        // ÁÂ¿ì ¿§Áö
        if (verticalSegmentCount > 0)
        {
            (..verticalSegmentCount).ForEach(i =>
            {
                float posZ = (i + 1) * ModelSize;

                GameObject leftEdge = InstantiateEdge(transform);
                leftEdge.transform.localPosition = new Vector3(0f, 0f, posZ);

                GameObject rightEdge = InstantiateEdge(transform);
                rightEdge.transform.localPosition = new Vector3(RightPosX, 0f, posZ);
                rightEdge.transform.Rotate(Vector3.up, 180f);
            });
        }
    }

    private void InstantiateAllSegments()
    {
        if (horizontalSegmentCount > 0 || verticalSegmentCount > 0)
        {
            (..horizontalSegmentCount).ForEach(x =>
            {
                (..verticalSegmentCount).ForEach(z =>
                {
                    GameObject segment = InstantiateSegment(transform);
                    segment.transform.localPosition = new Vector3((x + 1) * ModelSize, 0f, (z + 1) * ModelSize);
                });
            });
        }
    }

    private GameObject InstantiateCorner(Transform parent)
    {
        return ResourceManager.InstantiatePrefab("Prefabs/SidewalkCorner", parent);
    }

    private GameObject InstantiateEdge(Transform parent)
    {
        return ResourceManager.InstantiatePrefab("Prefabs/SidewalkEdge", parent);
    }

    private GameObject InstantiateSegment(Transform parent)
    {
        return ResourceManager.InstantiatePrefab("Prefabs/SidewalkSegment", parent);
    }
}
