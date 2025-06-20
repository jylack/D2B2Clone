using System;
using UnityEngine;

/// <summary>
/// Android 네이티브 TTS(Text-to-Speech)를 호출하는 컴포넌트
/// - 에디터에선 Stub 모드로 동작
/// - Android 기기에선 실제 TTS 엔진을 사용
/// </summary>
public class ScTTSSetting : MonoBehaviour
{
    // AndroidJavaObject 로 생성한 TextToSpeech 인스턴스
    private AndroidJavaObject tts;
    private bool isReady = false;

    // (Optional) AudioSource를 통해 Spatial Audio 등 Unity 오디오 시스템으로 라우팅할 때 사용
    [SerializeField] private AudioSource audioSource;

    void Awake()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        InitializeAndroidTTS();
#else
        Debug.Log("[Editor] ScTTSSetting: 에디터 모드, TTS는 실제로 동작하지 않습니다.");
#endif
    }

    /// <summary>
    /// Android TTS 초기화
    /// </summary>
    private void InitializeAndroidTTS()
    {
        Debug.Log("ScTTSSetting: Android TTS 초기화 시작");

        // UnityPlayer.currentActivity 가져오기
        using (var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            var activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

            // TextToSpeech 생성 → OnInitListener 전달
            tts = new AndroidJavaObject(
                "android.speech.tts.TextToSpeech",
                activity,
                new TTSInitListener(status =>
                {
                    const int SUCCESS = 0;
                    if (status == SUCCESS)
                    {
                        isReady = true;
                        Debug.Log("ScTTSSetting: TTS init succeeded");

                        // (선택) 한국어 설정
                        var locale = new AndroidJavaObject("java.util.Locale", "ko", "KR");
                        int result = tts.Call<int>("setLanguage", locale);
                        Debug.Log($"ScTTSSetting: setLanguage result = {result}");
                    }
                    else
                    {
                        Debug.LogError($"ScTTSSetting: TTS init failed: {status}");
                    }
                })
            );
        }
    }

    /// <summary>
    /// 텍스트 읽기 요청
    /// </summary>
    /// <param name="text">읽을 문자열</param>
    /// <param name="flush">기존 읽기를 중단하고 바로 재생할지 여부 (true: QUEUE_FLUSH)</param>
    public void Speak(string text, bool flush = true)
    {
        Debug.Log($"ScTTSSetting: Speak 호출 text=\"{text}\" ready={isReady}");

#if UNITY_ANDROID && !UNITY_EDITOR
        if (!isReady) return;

        int queueMode = flush ? 1 /* QUEUE_FLUSH */ : 0 /* QUEUE_ADD */;
        tts.Call<int>("speak", text, queueMode, null, null);
#else
        // 에디터 모드용 Stub
        Debug.Log($"[Editor Stub] Would speak: {text}");
#endif
    }

    void OnDestroy()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        if (tts != null)
        {
            tts.Call("stop");
            tts.Call("shutdown");
        }
#endif
    }
}

/// <summary>
/// Android TextToSpeech.OnInitListener 콜백을 C#에서 처리하기 위한 Proxy 클래스
/// </summary>
public class TTSInitListener : AndroidJavaProxy
{
    private readonly Action<int> onInitCallback;

    public TTSInitListener(Action<int> callback)
        : base("android.speech.tts.TextToSpeech$OnInitListener")
    {
        onInitCallback = callback;
    }

    // 자바 인터페이스 시그니처 그대로 구현해야 콜백이 나옵니다.
    public void onInit(int status)
    {
        onInitCallback(status);
    }
}
