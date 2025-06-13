using CsvHelper;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

public class ScLanguageAPI : MonoBehaviour
{
    private static string apiKey = "AIzaSyAIl4i3N-Eje4uGgieAxmmmAPckvJECVxo";



    //void Start()
    //{
    //    TranslateTextAsync("I love game development", "en", "ko").Forget();
    //}

    [MenuItem("Tools/Set BaseLanguages")]
    public static async void SetBaseLanguages()
    {
        try
        {
            Debug.Log("===== 변역 시작 =====");

            using var reader = new StreamReader(LanguageManager.LanguageFilePath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            var list = csv.GetRecords<ReadItem>().ToList();

            List<BaseLanguageItem> langItems = new();
            int index = 0;

            foreach (ReadItem item in list)
            {
                string value = await TranslateTextAsync(item.Text, "ko", "en");
                langItems.Add(new BaseLanguageItem
                {
                    id = item.Id,
                    ko = item.Text,
                    en = value,
                });

                Debug.Log($"{index++}: {value}");
            }

            using StreamWriter writer = new(Application.dataPath + "/Localization/BaseLanguages.csv");
            using CsvWriter csvWriter = new(writer, CultureInfo.InvariantCulture);
            csvWriter.WriteHeader<BaseLanguageItem>();
            csvWriter.NextRecord();
            csvWriter.WriteRecords(langItems);
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.Message);
        }
        finally
        {
            Debug.Log("===== 변역 완료 =====");
        }
    }

    [MenuItem("Tools/Translate")]
    public static async void Translate()
    {
        DateTime dtStart = DateTime.Now;

        try
        {
            Debug.Log("===== 변역 시작 =====");

            using var reader = new StreamReader(Application.dataPath + "/Localization/BaseLanguages.csv");
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            var list = csv.GetRecords<BaseLanguageItem>().ToList();

            List<FinalLanguageItem> langItems = new();
            int index = 0;

            //var item = list[0];
            foreach (BaseLanguageItem item in list)
            {
                //string ja = await TranslateTextAsync(item.en, "en", "ja");
                //string zh = await TranslateTextAsync(item.en, "en", "zh");
                string fr = await TranslateTextAsync(item.en, "en", "fr");
                //string hi = await TranslateTextAsync(item.en, "en", "hi");
                string ru = await TranslateTextAsync(item.en, "en", "ru");
                string de = await TranslateTextAsync(item.en, "en", "de");

                langItems.Add(new FinalLanguageItem
                {
                    Key = item.id,
                    ko = item.ko,
                    en = item.en,
                    //ja = ja,
                    //zh = zh,
                    fr = fr,
                    //hi = hi,
                    ru = ru,
                    de = de,
                });

                Debug.Log($"{index++}:");
            }

            using StreamWriter writer = new(Application.dataPath + "/Localization/TotalLanguages.csv");
            using CsvWriter csvWriter = new(writer, CultureInfo.InvariantCulture);
            csvWriter.WriteHeader<FinalLanguageItem>();
            csvWriter.NextRecord();
            csvWriter.WriteRecords(langItems);
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.Message);
        }
        finally
        {
            Debug.Log($"===== 변역 완료 ({DateTime.Now - dtStart}) =====");
        }
    }



    private static async UniTask<string> TranslateTextAsync(string inputText, string sourceLang, string targetLang)
    {
        string url = $"https://translation.googleapis.com/language/translate/v2?key={apiKey}";

        var requestData = new TranslationRequest
        {
            q = inputText,
            source = sourceLang,
            target = targetLang,
            format = "text"
        };

        string jsonBody = JsonConvert.SerializeObject(requestData);
        var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonBody);

        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        await request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            var responseJson = request.downloadHandler.text;
            var response = JsonConvert.DeserializeObject<TranslationResponse>(responseJson);
            return response.data.translations[0].translatedText;

            //Debug.Log($"번역 결과: {translatedText}");
        }

        return "?";
    }

    // 요청 구조
    public class TranslationRequest
    {
        public string q;
        public string source;
        public string target;
        public string format = "text";
    }

    // 응답 구조
    public class TranslationResponse
    {
        public Data data;

        public class Data
        {
            public Translation[] translations;
        }

        public class Translation
        {
            public string translatedText;
        }
    }

    public class ReadItem
    {
        public string Id { get; set; }
        public string TTS { get; set; }
        public string Text { get; set; }
    }

    public class BaseLanguageItem
    {
        public string id { get; set; }
        public string ko { get; set; }
        public string en { get; set; }
    }

    public class FinalLanguageItem
    {
        public string Key { get; set; }
        public string ko { get; set; }
        public string en { get; set; }
        //public string ja { get; set; }  // 일본
        //public string zh { get; set; }  // 중국 간체
        public string fr { get; set; }  // 프랑스
        //public string hi { get; set; }  // 힌디
        public string ru { get; set; }  // 러시아
        public string de { get; set; }  // 독일
    }
}
