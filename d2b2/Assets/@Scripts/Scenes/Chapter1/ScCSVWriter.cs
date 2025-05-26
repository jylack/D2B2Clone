using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ScCSVWriter : MonoBehaviour
{

    private void Start()
    {
        // CSV 파일로 저장
        SaveDictionaryToCSV(ScStringTable.DialogueMap, Application.dataPath + "/@Scenes/03.Privates/JYL/DialogueMap.csv");
    }

    public static void SaveDictionaryToCSV(Dictionary<string, string> data, string filePath)
    {
        // 1. CSV 파일 스트림을 생성합니다.
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            // 2. 딕셔너리의 키와 값을 쉼표로 구분하여 텍스트로 만듭니다.
            foreach (KeyValuePair<string, string> pair in data)
            {
                writer.WriteLine($"{pair.Key},{pair.Value}"); // 키와 값을 쉼표로 구분하여 한 줄에 저장합니다.
            }
        }
    }

}