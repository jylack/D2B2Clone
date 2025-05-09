using TMPro;
using UnityEngine;

public enum TrafficLightColor
{
    Red,
    Green
}

//신호등 클래스
public class ScTrafficCtrl : MonoBehaviour
{
    [Header("오브젝트 연결")]
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] MeshRenderer m_MeshRenderer;
    private Shader defShader, unlitShader;

    [Header("신호등 세팅")]

    [SerializeField] float MaxTime = 30f;
    float deltaMaxTime;
    [SerializeField] float LimitTime = 7f;
    float deltaLimitTime;

    float CurrentTime = 0f;
    float timer = 0f;
    float interval = 1f;

    [SerializeField] TrafficLightColor CurrentColor;


    private void Start()
    {
        // 2) load URP shaders
        defShader = Shader.Find("Universal Render Pipeline/Lit");
        unlitShader = Shader.Find("Universal Render Pipeline/Unlit");

        if (defShader == null || unlitShader == null)
            Debug.LogError("Failed to load URP shaders. Check shader names.");


        deltaMaxTime = MaxTime;
        deltaLimitTime = LimitTime;
        CurrentTime = deltaMaxTime;


        SetColor(CurrentColor);
    }

    void ChangeColor()
    {

        switch (CurrentColor)
        {
            case TrafficLightColor.Red:
                CurrentColor = TrafficLightColor.Green;
                break;

            case TrafficLightColor.Green:
                CurrentColor = TrafficLightColor.Red;

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
        // 1) TMP 텍스트 색
        //switch (color)
        //{
        //    case TrafficLightColor.Red:
        //        timerText.color = Color.red;
        //        color = TrafficLightColor.Red;
        //        break;
        //    case TrafficLightColor.Green:
        //        timerText.color = Color.green;
        //        color = TrafficLightColor.Green;
        //        break;
        //    default:
        //        break;

        //}        
        timerText.color = (color == TrafficLightColor.Red) ? Color.red : Color.green;

        // 2) 머티리얼 슬롯 인덱스 매핑
        //    Red  -> 0
        //    Green-> 1
        int highlightIndex = (color == TrafficLightColor.Red) ? 1 : 0;

        // 3) 각 슬롯 셰이더 교체
        var mats = m_MeshRenderer.materials;
        for (int i = 0; i < mats.Length; i++)
        {
            mats[i].shader = (i == highlightIndex)
                ? unlitShader    // 켜질 때
                : defShader;     // 나머지는 끌 때
        }
        m_MeshRenderer.materials = mats;

        //m_MeshRenderer.material = m_MeshRenderer.materials[(int)color];

    }



    private void Update()
    {
        timer += Time.deltaTime;

        // 1초(또는 interval) 이상 쌓이면
        if (timer >= interval)
        {
            //현재 시간 감소
            CurrentTime -= interval;


            if (CurrentTime <= 0f)
            {
                CurrentTime = 0f;
                ChangeColor();
            }

            //tmp에 적용
            timerText.text = CurrentTime.ToString();

            // 누적된 시간 초기화
            timer = 0f;
        }

    }
}
