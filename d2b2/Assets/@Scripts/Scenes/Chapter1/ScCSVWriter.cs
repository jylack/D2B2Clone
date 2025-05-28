using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ScCSVWriter 
{
    public void Init(Dictionary<string, string> data)
    {
        // CSV 파일로 저장
        SaveDictionaryToCSV(data, Application.dataPath + "/@Scenes/03.Privates/JYL/DialogueMap.csv");
    }

    public void SaveDictionaryToCSV(Dictionary<string, string> data, string filePath)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (KeyValuePair<string, string> pair in data)
            {
                //한줄에 저장
                writer.WriteLine($"{pair.Key},{pair.Value}"); 
            }
        }
    }

}