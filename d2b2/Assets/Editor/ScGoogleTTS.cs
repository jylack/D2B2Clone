using Cysharp.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

public class ScGoogleTTS : MonoBehaviour
{
    private const string TOKEN_URI = "https://oauth2.googleapis.com/token";
    private const string TTS_URI = "https://texttospeech.googleapis.com/v1/text:synthesize";
    private const string SAVE_FOLDER_PATH = "Resources/TTS/Temp";


    
    // [MenuItem("Tools/RequestGoogleTTS_All")]
    private static async void RequestAll()
    {
        int index = 0;
        
        try
        {
            Debug.Log("===== 모든 TTS 요청 시작 =====");
            
            Dictionary<string, KeyData> langDict = ScCsvLoader.Parse(LanguageManager.LanguageFilePath);
            foreach (var lang in langDict)
            {
                if (lang.Value.UseTTS)
                {
                    await RequestTTS(lang.Value.Text, lang.Key);
                    Debug.Log($"{index++:00}: TTS 저장 완료: {lang.Key}.wav");
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
        finally
        {
            Debug.Log($"===== TTS 요청 완료: {index + 1}개 =====");
        }
    }
    
     [MenuItem("Tools/RequestGoogleTTS")]
    private static async void RequestSingle()
    {
        try
        {
            Debug.Log("===== TTS 요청 =====");

            string key = "Mini15";

            Dictionary<string, KeyData> langDict = ScCsvLoader.Parse(LanguageManager.LanguageFilePath);
            
            if (langDict.TryGetValue(key, out KeyData lang))
                await RequestTTS(lang.Text, key);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
        finally
        {
            Debug.Log("===== TTS 요청 완료 =====");
        }
    }
    
    private static async UniTask RequestTTS(string text, string fileName)
    {
        try
        {
            string credPath = Path.Combine(Application.streamingAssetsPath, "unity-tts-462712-76584afbc113.json");
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
                    ["text"] = text 
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
            string outputPath = Path.Combine(Application.dataPath, SAVE_FOLDER_PATH, $"{fileName}.wav");
            await File.WriteAllBytesAsync(outputPath, audioBytes);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
            throw;
        }
    }
}
