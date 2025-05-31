using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class ScTruckShader : MonoBehaviour
{
    public float dissolveSpeed = 1f;
    private float dissolveValue = 0f;
    private Material[] materials;
    [SerializeField] MeshRenderer[] allMats;
    [SerializeField] Shader shader;

    // [MenuItem("Tools/Change Shader For All MeshRenderers")]
    // static void ChangeAllShaders()
    // {
    //     Shader targetShader = Shader.Find("Shader Graphs/YourShaderNameHere"); // 원하는 셰이더 경로
    //     if (targetShader == null)
    //     {
    //         Debug.LogError("Shader not found. Check the shader path.");
    //         return;
    //     }
    //
    //     MeshRenderer[] renderers = FindObjectsOfType<MeshRenderer>();
    //
    //     foreach (MeshRenderer renderer in renderers)
    //     {
    //         Material[] materials = renderer.sharedMaterials; // shared 사용해야 에디터 적용
    //         for (int i = 0; i < materials.Length; i++)
    //         {
    //             if (materials[i] != null)
    //             {
    //                 materials[i].shader = targetShader;
    //                 EditorUtility.SetDirty(materials[i]); // 변경 표시
    //             }
    //         }
    //     }
    //
    //     AssetDatabase.SaveAssets();
    //     Debug.Log("Shader 변경 완료");
    // }
    IEnumerator Start()
    {
        yield return new WaitForSeconds(1);
        allMats = gameObject.GetComponentsInChildren<MeshRenderer>();
        yield return new WaitForSeconds(2);
        for (int i = 0; i < allMats.Length; i++)
        {
            for (int j = 0; j < allMats[i].materials.Length; j++)
            {
                allMats[i].materials[j].shader = shader;
            }
        }
        Debug.Log(allMats.Length);
    }

    IEnumerator Ham()
    {
        float currentTime = 0;
        while (true)
        {
            currentTime += Time.deltaTime;
            for (int i = 0; i < allMats.Length; i++)
            {

            }
            yield return null;
        }
    }
}
    //void Update()
    //{
    //    // 디졸브 값 증가
    //    dissolveValue += Time.deltaTime * dissolveSpeed;
    //    dissolveValue = Mathf.Clamp01(dissolveValue);
    //
    //    foreach (Material mat in materials)
    //    {
    //        if (mat.HasProperty("_DissolveThreshold"))
    //            mat.SetFloat("_DissolveThreshold", dissolveValue);
    //    }
    //}

    //private void Test(GameObject gameObject)
    //{
    //    allMats = new List<GameObject>();
    //    Queue<GameObject> queue = new Queue<GameObject>();
    //    int count = 0;
    //    queue.Enqueue(gameObject);
    //    GameObject parent = gameObject;
    //    while (queue.Count > 0)
    //    {
    //        parent = queue.Peek().gameObject;
    //        count = parent.transform.childCount;
    //        for (int i=0; i< count; i++)
    //        {
    //            queue.Enqueue(parent.transform.GetChild(i).gameObject);
    //        }
    //    }
    //    foreach (var a in queue)
    //    {
    //        //allMats.Add(a);
    //    }
    //}
