using UnityEngine;
using UnityEngine.Events;

public class ScTrafficLight : MonoBehaviour
{
    [SerializeField] private MeshRenderer redMeshRenderer;
    [SerializeField] private MeshRenderer greenMeshRenderer;
    [SerializeField] private Material redOnMaterial;
    [SerializeField] private Material greenOnMaterial;
    [SerializeField] private UnityEvent OnRedLightActivated;
    [SerializeField] private UnityEvent OnGreenLightActivated;

    private Material redOffMaterial;
    private Material greenOffMaterial;
    private ScDefine.ScTrafficLightType lightType;



    private void Awake()
    {
        redOffMaterial = redMeshRenderer.material;
        greenOffMaterial = greenMeshRenderer.material;
    }



    public void SetLight(ScDefine.ScTrafficLightType light)
    {
        lightType = light;

        switch (light)
        {
            case ScDefine.ScTrafficLightType.Red:
                redMeshRenderer.material    = redOnMaterial;
                greenMeshRenderer.material  = greenOffMaterial;
                OnRedLightActivated?.Invoke();
                break;
            case ScDefine.ScTrafficLightType.Green:
                redMeshRenderer.material    = redOffMaterial;
                greenMeshRenderer.material  = greenOnMaterial;
                OnGreenLightActivated?.Invoke();
                break;
            default:
                redMeshRenderer.material    = redOffMaterial;
                greenMeshRenderer.material  = greenOffMaterial;
                break;
        }
    }

    public void InvertColor()
    {
        if (lightType == ScDefine.ScTrafficLightType.Green)
        {
            lightType = ScDefine.ScTrafficLightType.None;
            greenMeshRenderer.material = greenOffMaterial;
        }
        else
        {
            lightType = ScDefine.ScTrafficLightType.Green;
            greenMeshRenderer.material = greenOnMaterial;
        }
    }
}
