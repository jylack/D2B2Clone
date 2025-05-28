using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LanguageManager : MonoBehaviour
{

    //private Dictionary<string, string> dialogueMap = new Dictionary<string, string>();
    private ScCsvLoader csvLoader = new ScCsvLoader();

    public Dictionary<string, string> DialogueMap { get; private set; }

    private void Start()
    {
        DialogueMap = csvLoader.Init();
    }

    public string GetLanguage(string key)
    {
        if (DialogueMap.TryGetValue(key, out string value))
        {
            return value;
        }
        else
        {
            Debug.LogWarning($"Dialogue key '{key}' not found.");
            return string.Empty;
        }
    }

     
}
