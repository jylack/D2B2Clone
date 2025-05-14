using System.Collections;
using TMPro;
using UnityEngine;

public enum TrafficLightColor
{
    Red,
    Green
}

public class ScTrafficCtrl : MonoBehaviour
{
    [Header("오브젝트 연결")]
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private MeshRenderer m_MeshRenderer;
    [SerializeField] private Material red, green;

    private Shader litShader, unlitShader;

    [Header("신호등 세팅")]

    [SerializeField] private float MaxTime = 30f;
    private float deltaMaxTime = 0f;
    private float LimitTime ;
    private float deltaLimitTime = 0f;

    [SerializeField] private float blinkInterval = 0.5f;
    

    private float CurrentTime = 0f;
    private float timer = 0f;
    private float interval = 1f;

    [SerializeField] private TrafficLightColor currentColor;
    public TrafficLightColor CurrentColor => currentColor;

    private Coroutine blinkCor;

    //public TrafficLightColor GetCurrentColor()
    //{
    //    return CurrentColor; 
    //}


    private void Start()
    {
        // 2) load URP shaders
        litShader = Shader.Find("Universal Render Pipeline/Lit");
        unlitShader = Shader.Find("Universal Render Pipeline/Unlit");

        if (litShader == null || unlitShader == null)
            Debug.LogError("Failed to load URP shaders. Check shader names.");

        LimitTime = (MaxTime / 4f);

        deltaMaxTime = MaxTime;
        deltaLimitTime = LimitTime;
        CurrentTime = deltaMaxTime;


        SetColor(CurrentColor);
    }

    public bool IsBlink()
    {
        return blinkCor != null;
    }

    void ChangeColor()
    {
        if (blinkCor != null)
        {
            blinkCor = null;
            StopAllCoroutines();
        }

        switch (currentColor)
        {
            case TrafficLightColor.Red:
                currentColor = TrafficLightColor.Green;
                break;

            case TrafficLightColor.Green:
                currentColor = TrafficLightColor.Red;

                break;
            default:
                break;

        }


        Init();
    }

    void Init()
    {

        MaxTime = deltaMaxTime;
        LimitTime = deltaLimitTime;
        CurrentTime = deltaMaxTime;

        SetColor(CurrentColor);
    }

    void SetColor(TrafficLightColor color)
    {
        bool isRed = (color == TrafficLightColor.Red);

        timerText.color = isRed ? Color.red : Color.green;

        m_MeshRenderer.material = isRed ? red : green;

        //셰이더 교체
        m_MeshRenderer.material.shader = litShader;
    }

    IEnumerator ApplyBlink()
    {
        bool highlightOff = false;


        while (true)
        {
            highlightOff = !highlightOff;

            m_MeshRenderer.material.shader = highlightOff ? unlitShader : litShader;

            yield return new WaitForSeconds(blinkInterval);
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= interval)
        {
            CurrentTime -= interval;

            // 깜빡임 처리
            if (CurrentTime <= LimitTime)
            {
                blinkCor = StartCoroutine(ApplyBlink());
            }

            if (CurrentTime <= 0f)
            {
                CurrentTime = 0f;
                ChangeColor();
            }

            timerText.text = CurrentTime.ToString();

            timer = 0f;
        }
    }
}
