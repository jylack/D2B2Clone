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

        if (meshRenderer != null)
        {
            lightType = ScDefine.ScTrafficLightType.Red;
            meshRenderer.material = redOnMaterial;
        }
    }

    public void OnGreenLightActivated()
    {
        if (meshRenderer != null)
        {
            lightType = ScDefine.ScTrafficLightType.Green;
            meshRenderer.material = greenOnMaterial;
        }
    }

    public void OnBeginGreenLightBlink()
    {
        BlinkGreenRepeatedly().Forget();
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

    private async UniTask BlinkGreenRepeatedly()
    {
        try
        {
            blinkCts?.Cancel();
            blinkCts?.Dispose();
            blinkCts = null;

            blinkCts = new CancellationTokenSource();
            CancellationToken timeUpCts = ScCh3PlayService.Instance.TimeUpCts.Token;
            CancellationTokenSource linkedCts = CancellationTokenSource.CreateLinkedTokenSource(blinkCts.Token, timeUpCts, base.DestroyToken);

            while (!linkedCts.IsCancellationRequested)
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
