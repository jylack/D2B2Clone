using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class ScDisplaySetting : MonoBehaviour
{
    [SerializeField] Slider renderScaleSlider;
    [SerializeField] Slider brightnessSlider;
    UniversalRenderPipelineAsset urp;
    Light sceneLight;
    // Start is called before the first frame update
    void Start()
    {
        urp = GraphicsSettings.currentRenderPipeline as UniversalRenderPipelineAsset;
        sceneLight = GameObject.Find("Directional Light").GetComponent<Light>();
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
        sceneLight.intensity = brightnessSlider.value + 1;
    }
}
