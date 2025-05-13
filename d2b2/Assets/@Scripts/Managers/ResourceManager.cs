using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static GameObject InstantiatePrefab(string prefabPath, Transform parent = null)
    {
        GameObject prefab = Resources.Load<GameObject>(prefabPath);
        GameObject obj = parent != null ? Instantiate(prefab, parent) : Instantiate(prefab);
        obj.name = prefab.name;

        return obj;
    }
}