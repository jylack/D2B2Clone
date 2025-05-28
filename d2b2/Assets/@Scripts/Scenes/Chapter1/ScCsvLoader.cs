using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class ScCsvLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       string text = System.IO.File.ReadAllText(Application.dataPath + "/@Scenes/03.Privates/JYL/StringTable.csv", System.Text.Encoding.UTF8);

        var textList = LoadFromText(text);

        foreach(var row in textList)
        {
            if (row.TryGetValue("ID", out string key) && row.TryGetValue("Str", out string value))
            {
                //Debug.Log($"Key: {key}, Value: {value}");
                ScStringTable.DialogueMap.Add(key, value);
            }
        }
        

    }

    // CSV 전체 텍스트를 받아 헤더행 포함 레코드 단위로 분리한 뒤 파싱
    public static List<Dictionary<string, string>> LoadFromText(string csvText)
    {
        var result = new List<Dictionary<string, string>>();

        // 1) 레코드 단위로 분리 (멀티라인 필드 지원)
        var records = SplitCsvRecords(csvText);
        if (records.Count == 0)
            return result;

        // 2) 첫 행을 헤더로 사용
        var headers = ParseLine(records[0]);

        // 3) 그 외 행을 순회하며 딕셔너리 생성
        for (int i = 1; i < records.Count; i++)
        {
            var fields = ParseLine(records[i]);
            var row = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            for (int j = 0; j < headers.Count; j++)
            {
                string key = headers[j];
                string val = j < fields.Count ? fields[j] : string.Empty;
                row[key] = val;
            }
            result.Add(row);
        }

        return result;
    }

    // 텍스트 전체를 한 글자씩 보면서, 인용부호 안에 들어 있는 줄바꿈은 무시하고
    // 인용부호 밖의 '\r' 또는 '\n' 만을 레코드 구분자로 삼음
    private static List<string> SplitCsvRecords(string text)
    {
        var records = new List<string>();
        var sb = new StringBuilder();
        bool inQuotes = false;

        for (int i = 0; i < text.Length; i++)
        {
            char c = text[i];

            if (c == '"')
            {
                // 이중 인용부호("")는 하나의 인용부호로 처리
                if (inQuotes && i + 1 < text.Length && text[i + 1] == '"')
                {
                    sb.Append('"');
                    i++;
                }
                else
                {
                    inQuotes = !inQuotes;
                }
                sb.Append('"');
            }
            else if ((c == '\r' || c == '\n') && !inQuotes)
            {
                // 레코드 끝
                records.Add(sb.ToString());
                sb.Clear();
                // \r\n 조합일 때 \n 건너뛰기
                if (c == '\r' && i + 1 < text.Length && text[i + 1] == '\n')
                    i++;
            }
            else
            {
                sb.Append(c);
            }
        }

        if (sb.Length > 0)
            records.Add(sb.ToString());

        return records;
    }

    // 한 레코드(한 줄)에서 콤마 단위로 필드를 분리, 인용부호를 벗어나면 분할
    // 인용부호 내부의 콤마나 줄바꿈은 sb 에 그대로 축적
    private static List<string> ParseLine(string line)
    {
        var list = new List<string>();
        var sb = new StringBuilder();
        bool inQuotes = false;

        for (int i = 0; i < line.Length; i++)
        {
            char c = line[i];

            if (c == '"')
            {
                if (inQuotes && i + 1 < line.Length && line[i + 1] == '"')
                {
                    sb.Append('"');
                    i++;
                }
                else
                {
                    inQuotes = !inQuotes;
                }
            }
            else if (c == ',' && !inQuotes)
            {
                list.Add(sb.ToString());
                sb.Clear();
            }
            else
            {
                sb.Append(c);
            }
        }
        list.Add(sb.ToString());
        return list;
    }

    //// CSV 텍스트를 파싱
    //public static List<Dictionary<string, string>> LoadFromText(string csvText)
    //{
    //    var result = new List<Dictionary<string, string>>();
    //    using (var reader = new StringReader(csvText))
    //    {
    //        string headerLine = reader.ReadLine();
    //        if (string.IsNullOrEmpty(headerLine))
    //            return result;

    //        var headers = ParseLine(headerLine);
    //        string line;
    //        while ((line = reader.ReadLine()) != null)
    //        {
    //            var values = ParseLine(line);
    //            var row = new Dictionary<string, string>();
    //            for (int i = 0; i < headers.Count; i++)
    //            {
    //                string key = headers[i];
    //                string val = i < values.Count ? values[i] : string.Empty;
    //                row[key] = val;
    //            }
    //            result.Add(row);
    //        }
    //    }
    //    return result;
    //}

    //// 한 줄을 파싱해서 필드 리스트로 반환
    //private static List<string> ParseLine(string line)
    //{
    //    var list = new List<string>();
    //    var sb = new StringBuilder();
    //    bool inQuotes = false;

    //    for (int i = 0; i < line.Length; i++)
    //    {
    //        char c = line[i];

    //        if (c == '"')
    //        {
    //            // 이스케이프된 따옴표("") 처리
    //            if (inQuotes && i + 1 < line.Length && line[i + 1] == '"')
    //            {
    //                sb.Append('"');
    //                i++;
    //            }
    //            else
    //            {
    //                inQuotes = !inQuotes;
    //            }
    //        }
    //        else if (c == ',' && !inQuotes)
    //        {
    //            list.Add(sb.ToString());
    //            sb.Clear();
    //        }
    //        else
    //        {
    //            sb.Append(c);
    //        }
    //    }

    //    list.Add(sb.ToString());
    //    return list;
    //}
}
