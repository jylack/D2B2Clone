using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class ScDisplaySetting : MonoBehaviour
{
    [SerializeField] Slider renderScaleSlider;
    [SerializeField] Slider brightnessSlider;
    UniversalRenderPipelineAsset urp;
    Light[] sceneLights;
    // Start is called before the first frame update
    void Awake()
    {
        urp = GraphicsSettings.currentRenderPipeline as UniversalRenderPipelineAsset;
        sceneLights = FindObjectsByType<Light>(FindObjectsSortMode.None);
        ApplyRenderScaleSetting();
        ApplyBrightnessSetting();
        gameObject.SetActive(false);
    }

    public void ApplyRenderScaleSetting()
    {
        if (!urp) return;
        urp.renderScale = renderScaleSlider.value;
    }

    public void ApplyBrightnessSetting()
    {
        if (!urp) return;
        if (sceneLights != null)
        {
            foreach (var light in sceneLights)
            {
                light.intensity = brightnessSlider.value + 1;
            }
        }
    }
}
