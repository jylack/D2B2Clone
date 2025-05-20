using UnityEngine;
using UnityEngine.Events;

public class ScTrafficLight : MonoBehaviour
{
    [SerializeField] private MeshRenderer redMeshRenderer;
    [SerializeField] private MeshRenderer greenMeshRenderer;
    [SerializeField] private Material redOnMaterial;
    [SerializeField] private Material greenOnMaterial;
    [SerializeField] private UnityEvent onGreenLightActivatedBefore;
    [SerializeField] private UnityEvent onGreenLightActivated;
    [SerializeField] private UnityEvent onRedLightActivated;
    [SerializeField] private UnityEvent onBeginGreenLightBlink;

    private Material redOffMaterial;
    private Material greenOffMaterial;
    private ScDefine.ScTrafficLightType lightType;



    private void Awake()
    {
        redOffMaterial = redMeshRenderer.material;
        greenOffMaterial = greenMeshRenderer.material;
    }



    public void Ready()
    {
        onGreenLightActivatedBefore?.Invoke();
    }

    public void SetColor(ScDefine.ScTrafficLightType light)
    {
        lightType = light;

        switch (light)
        {
            case ScDefine.ScTrafficLightType.Red:
                redMeshRenderer.material    = redOnMaterial;
                greenMeshRenderer.material  = greenOffMaterial;
                onRedLightActivated?.Invoke();
                break;
            case ScDefine.ScTrafficLightType.Green:
                redMeshRenderer.material    = redOffMaterial;
                greenMeshRenderer.material  = greenOnMaterial;
                onGreenLightActivated?.Invoke();
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

    public void StartGreenLightBlink()
    {
        onBeginGreenLightBlink?.Invoke();
    }
}
