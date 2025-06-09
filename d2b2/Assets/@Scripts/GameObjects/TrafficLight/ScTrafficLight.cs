using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class ScTrafficLight : ScObjectBase
{
    public UnityEvent onGreenLightActivated;

    [SerializeField] private MeshRenderer redMeshRenderer;
    [SerializeField] private MeshRenderer greenMeshRenderer;
    [SerializeField] private Material redOnMaterial;
    [SerializeField] private Material greenOnMaterial;
    [SerializeField] private UnityEvent onGreenLightActivatedBefore;
    [SerializeField] private UnityEvent onRedLightActivated;
    [SerializeField] private UnityEvent onBeginGreenLightBlink;

    private Material redOffMaterial;
    private Material greenOffMaterial;
    private ScDefine.ScTrafficLightType lightType;
    private CancellationTokenSource blinkCts;



    private void Awake()
    {
        redOffMaterial = redMeshRenderer.material;
        greenOffMaterial = greenMeshRenderer.material;

        SetColor(ScDefine.ScTrafficLightType.Red);
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
                SetRed();
                break;
            case ScDefine.ScTrafficLightType.Green:
                SetGreen();
                break;
            default:
                SetRed();
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

    public void OnStartGreenBlink()
    {
        onBeginGreenLightBlink?.Invoke();
    }

    public void StartBlinkGreen()
    {
        SetGreen();
        BlinkGreenRepeatly().Forget();
    }

    public void SetRed()
    {
        blinkCts?.Cancel();
        blinkCts = null;

        redMeshRenderer.material    = redOnMaterial;
        greenMeshRenderer.material  = greenOffMaterial;
        onRedLightActivated?.Invoke();
    }

    public void SetGreen()
    {
        blinkCts?.Cancel();
        blinkCts = null;

        redMeshRenderer.material    = redOffMaterial;
        greenMeshRenderer.material  = greenOnMaterial;
        onGreenLightActivated?.Invoke();
    }



    private async UniTask BlinkGreenRepeatly()
    {
        try
        {
            blinkCts?.Dispose();
            blinkCts = new CancellationTokenSource();
            CancellationTokenSource cts = CancellationTokenSource.CreateLinkedTokenSource(blinkCts.Token, base.DestroyToken);

            while (!cts?.IsCancellationRequested ?? false)
            {
                await UniTask.WaitForSeconds(0.5f, cancellationToken: cts.Token);
                InvertColor();
            }

            cts.Dispose();
        }
        catch (OperationCanceledException ex)
        {
            Debug.Log(ex.Message);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }
}
