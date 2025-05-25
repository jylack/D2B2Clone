using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    [SerializeField] private GameObject[] characterPrefabs;
    [SerializeField] private GameObject[] characterHeadPrefabs;
    
    
    
    public static GameObject InstantiatePrefab(string prefabPath, Transform parent = null)
    {
        GameObject prefab = Resources.Load<GameObject>(prefabPath);
        GameObject obj = parent != null ? Instantiate(prefab, parent) : Instantiate(prefab);
        obj.name = prefab.name;

        return obj;
    }



    public GameObject GetCharacterPrefab(ScDefine.ScGuideCharacter character)
    {
        return characterPrefabs[(int)ScDefine.ScGuideCharacter.Character1];
    }
    
    public GameObject GetCharacterHeadPrefab(ScDefine.ScGuideCharacter character)
    {
        return characterHeadPrefabs[(int)ScDefine.ScGuideCharacter.Character1];
    }
}