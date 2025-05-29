using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LanguageManager : MonoBehaviour
{

    private ScCsvLoader csvLoader = new ScCsvLoader();
    private Dictionary<string, string> dialogueMap = new Dictionary<string, string>();
    public Dictionary<string, string> DialogueMap => dialogueMap;    



    private void Start()
    {
        dialogueMap = csvLoader.Init("@Scenes/03.Privates/JYL/StringTable.csv");
    }



    public string GetText(string key)
    {
        if (dialogueMap.TryGetValue(key, out string value))
            return value;

        Debug.LogWarning($"Dialogue key '{key}' not found.");
        return string.Empty;
    }
}
