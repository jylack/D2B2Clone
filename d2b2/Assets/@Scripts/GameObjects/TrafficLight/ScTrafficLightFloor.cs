using Cysharp.Threading.Tasks;
using System.Threading;
using System;
using UnityEngine;

public class ScTrafficLightFloor : ScObjectBase
{
    [SerializeField] private Material redOnMaterial;
    [SerializeField] private Material greenOnMaterial;

    private MeshRenderer meshRenderer;
    private Material defaultMaterial;
    private CancellationTokenSource blinkCts;
    private ScDefine.ScTrafficLightType lightType;



    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        defaultMaterial = meshRenderer.material;
    }

    private void Start()
    {
        meshRenderer.material = redOnMaterial;
    }



    public void OnRedLightActivated()
    {
        blinkCts?.Cancel();
        blinkCts?.Dispose();
        blinkCts = null;

        lightType = ScDefine.ScTrafficLightType.Red;
        meshRenderer.material = redOnMaterial;
    }

    public void OnGreenLightActivated()
    {
        lightType = ScDefine.ScTrafficLightType.Green;
        meshRenderer.material = greenOnMaterial;
    }

    public void OnBeginGreenLightBlink()
    {
        BlinkGreenRepeatly().Forget();
    }



    private void InvertColor()
    {
        if (lightType == ScDefine.ScTrafficLightType.Green)
        {
            lightType = ScDefine.ScTrafficLightType.None;
            meshRenderer.material = defaultMaterial;
        }
        else
        {
            lightType = ScDefine.ScTrafficLightType.Green;
            meshRenderer.material = greenOnMaterial;
        }
    }

    private async UniTask BlinkGreenRepeatly()
    {
        try
        {
            blinkCts?.Cancel();
            blinkCts?.Dispose();
            blinkCts = null;

            blinkCts = new CancellationTokenSource();
            CancellationTokenSource linkedCts = CancellationTokenSource.CreateLinkedTokenSource(blinkCts.Token, base.DestroyToken);

            while (!linkedCts?.IsCancellationRequested ?? false)
            {
                InvertColor();
                await UniTask.WaitForSeconds(0.4f, cancellationToken: linkedCts.Token);
            }

            linkedCts.Dispose();
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
