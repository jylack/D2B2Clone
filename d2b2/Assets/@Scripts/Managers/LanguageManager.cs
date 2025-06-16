using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;

public class LanguageManager : MonoBehaviour
{
    private const string LocalizationTableName = "LocalizationTable";

    private static string languageFilePath;
    public static string LanguageFilePath => languageFilePath ??= $"{Application.dataPath}/Resources/LocalizationTable.csv";

    public Dictionary<string, KeyData> DialogueMap => dialogueMap;
    public ScLanguageSet LanguageSet { get; } = new();
    public ScDefine.ScLanguage Language { get; private set; } = ScDefine.ScLanguage.Ko;
    
    private Dictionary<string, KeyData> dialogueMap = new();
    private ScTTSSetting tts;



    private async void Start()
    {
        dialogueMap = ScCsvLoader.Parse(LanguageFilePath);
        tts = GetComponent<ScTTSSetting>();

        await LocalizationSettings.InitializationOperation.Task;
    }



    public string GetText(string key)
    {
        //if (dialogueMap.TryGetValue(key, out KeyData value))
        //    return value.Text;

        //Debug.LogWarning($"Dialogue key '{key}' not found.");
        //return string.Empty;

        Locale locale = LocalizationSettings.SelectedLocale;
        StringTable table = LocalizationSettings.StringDatabase.GetTable(LocalizationTableName, locale);

        if (table != null)
        {
            StringTableEntry entry = table.GetEntry(key);
            if (entry != null)
                return entry.LocalizedValue;
        }

        return string.Empty;
    }

    public async UniTask<string> GetTextAsync(string key)
    {
        try
        {
            await LocalizationSettings.InitializationOperation.Task;

            Locale locale = LocalizationSettings.SelectedLocale;
            StringTable table = await LocalizationSettings.StringDatabase.GetTableAsync(LocalizationTableName, locale);

            if (table != null)
            {
                StringTableEntry entry = table.GetEntry(key);
                if (entry != null)
                    return entry.LocalizedValue;
            }
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }

        return string.Empty;
    }

    public void Speak(string key)
    {

        if (dialogueMap.TryGetValue(key, out KeyData data))
        {
            if (data.UseTTS)
            {
                Debug.Log($"TTS: {data.Text}");
                tts.Speak(data.Text, true); 
            }
            else
            {
                Debug.Log($"Speak: {data.Text}");
            }
        }
        else
        {
            Debug.LogWarning($"Dialogue key '{key}' not found for speaking.");
        }
    }

    public async UniTaskVoid ChangeLanguage(ScDefine.ScLanguage lang)
    {
        try
        {
            await LocalizationSettings.InitializationOperation.Task;

            string localeId = lang.ToString().ToLower();
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.GetLocale(localeId);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }
}
