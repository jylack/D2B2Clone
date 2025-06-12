using Cysharp.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

public class ScGoogleTTS : MonoBehaviour
{
    private const string TOKEN_URI = "https://oauth2.googleapis.com/token";
    private const string TTS_URI = "https://texttospeech.googleapis.com/v1/text:synthesize";
    private const string SAVE_FOLDER_PATH = "Resources/TTS";



    [MenuItem("Tools/RequestGoogleTTS_All")]
    private static async void Request()
    {
        try
        {
            string inputText = @"정말 대단했어! 안전 보행 교육을 완벽히 마친 모범 보행자에게 수료증을 수여합니다! 앞으로도 안전하게 걸을 수 있겠지? 진심으로 축하해!";

            string credPath = Path.Combine(Application.streamingAssetsPath, "unity-auth-a2ac0-cf270e3093d2.json");
            string credJson = File.ReadAllText(credPath);
            var parsed = JObject.Parse(credJson);
            string clientEmail = parsed["client_email"].ToString();
            string privateKey = parsed["private_key"].ToString().Replace("\\n", "\n");

            string jwt = GoogleJwtHelper.CreateSignedJwt(clientEmail, privateKey, TOKEN_URI);
            WWWForm form = new WWWForm();
            form.AddField("grant_type", "urn:ietf:params:oauth:grant-type:jwt-bearer");
            form.AddField("assertion", jwt);

            UnityWebRequest tokenReq = UnityWebRequest.Post(TOKEN_URI, form);
            await tokenReq.SendWebRequest().ToUniTask();
            string token = JObject.Parse(tokenReq.downloadHandler.text)["access_token"].ToString();

            JObject requestBody = new JObject
            {
                ["audioConfig"] = new JObject
                {
                    ["audioEncoding"] = "LINEAR16",
                    ["effectsProfileId"] = "small-bluetooth-speaker-class-device",
                    ["pitch"] = 0,
                    ["speakingRate"] = 1,
                },
                ["input"] = new JObject { 
                    ["text"] = inputText 
                },
                ["voice"] = new JObject
                {
                    ["name"] = "ko-KR-Chirp3-HD-Leda",
                    ["languageCode"] = "ko-KR",
                },
            };

            UnityWebRequest ttsReq = new UnityWebRequest(TTS_URI, "POST");
            ttsReq.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(requestBody.ToString()));
            ttsReq.downloadHandler = new DownloadHandlerBuffer();
            ttsReq.SetRequestHeader("Authorization", "Bearer " + token);
            ttsReq.SetRequestHeader("Content-Type", "application/json");

            await ttsReq.SendWebRequest().ToUniTask();

            if (ttsReq.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("TTS 실패: " + ttsReq.error);
                return;
            }

            string audioBase64 = JObject.Parse(ttsReq.downloadHandler.text)["audioContent"].ToString();
            byte[] audioBytes = Convert.FromBase64String(audioBase64);
            string outputPath = Path.Combine(Application.dataPath, SAVE_FOLDER_PATH, "tts_output.wav");
            File.WriteAllBytes(outputPath, audioBytes);

            Debug.Log("TTS 저장 완료: " + outputPath);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }
}
