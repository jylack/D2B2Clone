using System.Collections.Generic;
using UnityEngine;

public class LanguageManager : MonoBehaviour
{
    private static string languageFilePath;
    public static string LanguageFilePath => languageFilePath ??= $"{Application.dataPath}/Resources/LocalizationTable.csv";

    public Dictionary<string, KeyData> DialogueMap => dialogueMap;
    public ScLanguageSet LanguageSet { get; } = new();
    
    private Dictionary<string, KeyData> dialogueMap = new();
    private ScTTSSetting tts;


    
    private void Start()
    {
        dialogueMap = ScCsvLoader.Parse(LanguageFilePath);
        tts = GetComponent<ScTTSSetting>(); 
    }



    public string GetText(string key)
    {
        if (dialogueMap.TryGetValue(key, out KeyData value))
            return value.Text;

        Debug.LogWarning($"Dialogue key '{key}' not found.");
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
}
