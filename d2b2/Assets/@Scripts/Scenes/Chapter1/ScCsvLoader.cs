using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class KeyData
{
    public bool useTTS { get; private set; }
    public string Text { get; private set; }

    public KeyData(bool use, string text)
    {
        useTTS = use;
        Text = text;
    }
}

public class ScCsvLoader
{
    private const string KeyId = "Id";
    private const string KeyText = "Text";
    private const string KeyUseTTS = "TTS";

    public static Dictionary<string, KeyData> Parse(string path)
    {
        Dictionary<string, KeyData> dict = new();

        string csvText = File.ReadAllText(path, Encoding.UTF8);
        List<Dictionary<string, string>> textList = LoadFromText(csvText);

        if (textList == null) 
            return null;

        foreach (Dictionary<string, string> row in textList)
        {
            // Id가 없으면 건너뜀
            if (!row.TryGetValue(KeyId, out string id))
                continue;

            // Text가 없으면 빈 문자열로 처리
            string text = row.TryGetValue(KeyText, out var textValue) ? textValue : string.Empty;

            bool useTts = false;
            if (row.TryGetValue(KeyUseTTS, out var flagStr))// TTS 사용 여부 불러옴
                bool.TryParse(flagStr, out useTts);// TTS 사용 여부가 없으면 false로 처리

            if (dict.ContainsKey(id))
            {                
                Debug.LogWarning($"Duplicate key found in CSV: {id}. Overwriting existing value.");
            }
            else
            {
                dict.Add(id, new KeyData(useTts, text));
            }
        }

        return dict;
    }

    // CSV 전체 텍스트를 받아 헤더행 포함 레코드 단위로 분리한 뒤 파싱
    public static List<Dictionary<string, string>> LoadFromText(string csvText)
    {
        var result = new List<Dictionary<string, string>>();

        //  레코드 단위로 분리 (줄바꿈 필드 지원)
        var records = SplitCsvRecords(csvText);
        if (records.Count == 0)
            return result;

        //  첫 행을 헤더로 사용
        var headers = ParseLine(records[0]);

        //  그 외 행을 순회하며 딕셔너리 생성
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
                // 이중 인용부호("")는 하나의 string(인용부호)로 처리
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

}
