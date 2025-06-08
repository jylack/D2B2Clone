using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using DG.Tweening;
using static UnityEngine.Rendering.DebugUI;

public class ScTruckShader : MonoBehaviour
{
    public float dissolveSpeed = 1f;
    private float dissolveValue = 0f;
    private Material[] materials;
    [SerializeField] MeshRenderer[] allMats;
    [SerializeField] List<Material> mats;
    [SerializeField] Shader shader;

    private void Start()
    {
        allMats = gameObject.GetComponentsInChildren<MeshRenderer>();
        for (int i=0; i< allMats.Length; i++)
        {
            for (int j=0; j< allMats[i].materials.Length; j++)
            {
                if(mats.Contains(allMats[i].materials[j]) == false)
                {
                    mats.Add(allMats[i].materials[j]);
                }
            }
        }
    }
    public void TruckTransparent(float endHeight)
    {
        float time = 10f;

        for (int i = 0; i < mats.Count; i++)
        {
            int index = i;

            var mat = mats[index];
            if (mat == null){continue;}
            if (mat.shader != shader){continue;}
            if (!mat.HasProperty("_ThresholdY")){continue;}
            float startValue = mat.GetFloat("_ThresholdY");
            DOTween.To
            (
                () => startValue,
                       x =>
                       {
                           startValue = x;
                           mat.SetFloat("_ThresholdY", x);
                       },
                       endHeight,
                       time
            );
        }
    }

    public void OnTruckTransparent()
    {
        TruckTransparent(0);
    }
    public void OffTruckTransparent()
    {
        TruckTransparent(7);
    }
}
