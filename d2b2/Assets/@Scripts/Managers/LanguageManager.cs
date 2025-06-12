using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LanguageManager : MonoBehaviour
{
    private Dictionary<string, KeyData> dialogueMap = new Dictionary<string, KeyData>();
    public Dictionary<string, KeyData> DialogueMap => dialogueMap;
    //public string CsvPath => Application.dataPath + "/@Scenes/03.Privates/JYL/LocalizationTable.csv";
    //public string CsvPath => Application.dataPath + "/Resources/LocalizationTable.csv";
    public string CsvPath { get; private set; }
    private ScTTSSetting tts;


    private void Start()
    {
        CsvPath = Path.Combine(Application.dataPath, "Resources/LocalizationTable.csv");
        dialogueMap = ScCsvLoader.Parse(CsvPath);
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
            if (data.useTTS)
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
