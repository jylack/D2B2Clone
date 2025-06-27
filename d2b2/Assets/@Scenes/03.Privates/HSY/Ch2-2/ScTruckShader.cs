using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScTruckShader : MonoBehaviour
{
    public float dissolveSpeed = 1f;
    private Material[] materials;
    [SerializeField] MeshRenderer[] allMats;
    [SerializeField] List<Material> mats;
    [SerializeField] Shader shader;
    [SerializeField] GameObject carRed;
    [SerializeField] MeshRenderer[] busMats;
    [SerializeField] GameObject carYellow;
    [SerializeField] MeshRenderer[] carMats;

    private void Start()
    {
        allMats = gameObject.GetComponentsInChildren<MeshRenderer>();
        busMats = carRed.GetComponentsInChildren<MeshRenderer>();
        carMats = carYellow.GetComponentsInChildren<MeshRenderer>();
        GetMeshRenderer(allMats);
        GetMeshRenderer(busMats);
        GetMeshRenderer(carMats);
    }
    private void GetMeshRenderer(MeshRenderer[] meshArr)
    {
        for (int i = 0; i < meshArr.Length; i++)
        {
            for (int j = 0; j < meshArr[i].materials.Length; j++)
            {
                if (mats.Contains(meshArr[i].materials[j]) == false)
                {
                    mats.Add(meshArr[i].materials[j]);
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
