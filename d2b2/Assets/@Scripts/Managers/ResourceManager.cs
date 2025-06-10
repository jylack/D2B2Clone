using UnityEngine;
using UnityEngine.UIElements;

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



    public GameObject InstantiateBadThingPointer()
    {
        return Instantiate(Resources.Load<GameObject>("Prefabs/BadThingPointer"));
    }

    public GameObject InstantiatePlusOneScore()
    {
        return Instantiate(Resources.Load<GameObject>("Prefabs/PlusOneScore"));
    }

    public GameObject InstantiateStarExplosion(Vector3 position)
    {
        return InstantiateVfx("Prefabs/StarExplosion", position);
    }

    public GameObject InstantiateSmokeExplosion(Vector3 position)
    {
        return InstantiateVfx("Prefabs/SmokeExplosionWhite", position);
    }

    public GameObject InstantiateConfettiBlast(Vector3 position)
    {
        return InstantiateVfx("Prefabs/ConfettiBlastRainbow", position);
    }

    public GameObject InstantiateConfettiDirectional(Vector3 position)
    {
        return InstantiateVfx("Prefabs/ConfettiDirectionalRainbow", position);
    }

    public GameObject GetCharacterPrefab(ScDefine.ScGuideCharacter character)
    {
        return characterPrefabs[(int)ScDefine.ScGuideCharacter.Character1];
    }
    
    public GameObject GetCharacterHeadPrefab(ScDefine.ScGuideCharacter character)
    {
        return characterHeadPrefabs[(int)ScDefine.ScGuideCharacter.Character1];
    }



    private GameObject InstantiateVfx(string path, Vector3 position)
    {
        GameObject vfx = Instantiate(Resources.Load<GameObject>(path));
        vfx.transform.position = position;

        var particle = vfx.GetComponent<ParticleSystem>();
        Destroy(vfx, particle.main.duration);

        return vfx;
    }
}