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

    // [MenuItem("Tools/Convert URP Lit → LT_Dissolve (Preserve Color + Set Float)")] 
    // static void ConvertShadersPreserveColorAndSetFloat()
    // {
    //     Shader targetShader = Shader.Find("Shader Graphs/LT_Desolve2");
    //     if (targetShader == null)
    //     {
    //         Debug.LogError("Shader not found: Shader Graphs/LT_Desolve2");
    //         return;
    //     }
    //
    //     var renderers = GameObject.FindObjectsOfType<MeshRenderer>();
    //     
    //     foreach (var renderer in renderers) 
    //     {
    //         var materials = renderer.sharedMaterials;
    //
    //         for (int i = 0; i < materials.Length; i++)
    //         {
    //             Material mat = materials[i];
    //             if (mat == null || mat.shader == null) continue;
    //
    //             if (mat.shader.name != "Shader Graphs/LT_Desolve") continue;
    //
    //             // 색상 백업
    //             Color baseColor = mat.HasProperty("_BaseColor") ? mat.GetColor("_BaseColor") : Color.white;
    //
    //             // Shader 변경
    //             mat.shader = targetShader;
    //
    //             // 색상 복원
    //             if (mat.HasProperty("_BaseColor"))
    //                 mat.SetColor("_BaseColor", baseColor);
    //
    //             // float 값 설정
    //             if (mat.HasProperty("_Float"))
    //             {
    //                 mat.SetFloat("_Float", 7f);
    //             }
    //             else
    //             {
    //                 Debug.Log("아ㅏㅏㅏㅏㅏㅏㅏㅏㅏㅏㅏㅏㅏㅏㅏㅏㅏㅏㅏㅏㅏㅏㅏㅏㅏㅏㅏㅏㅏㅏㅏㅏㅏㅏㅏㅏ");
    //             }
    //
    //             EditorUtility.SetDirty(mat);
    //         }
    //     }
    //
    //     AssetDatabase.SaveAssets();
    //     Debug.Log("Shader 변경, 색상 복원, float 세팅 완료");
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
        Debug.Log("길이 : "  + allMats.Length);
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
