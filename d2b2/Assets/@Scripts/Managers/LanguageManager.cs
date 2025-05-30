using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LanguageManager : MonoBehaviour
{

    private ScCsvLoader csvLoader = new ScCsvLoader();
    private Dictionary<string, KeyData> dialogueMap = new Dictionary<string, KeyData>();
    public Dictionary<string, KeyData> DialogueMap => dialogueMap;
    //public string CsvPath => Application.dataPath + "/@Scenes/03.Privates/JYL/LocalizationTable.csv";
    public string CsvPath => Application.dataPath + "/Resources/LocalizationTable.csv";


    private void Start()
    {
        dialogueMap = csvLoader.Init(CsvPath);
    }



    public string GetText(string key)
    {
        if (dialogueMap.TryGetValue(key, out KeyData value))
            return value.Text;

        Debug.LogWarning($"Dialogue key '{key}' not found.");
        return string.Empty;
    }
}
