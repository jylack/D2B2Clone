using System.IO;
using UnityEditor;
using UnityEngine;

public class ScLanguageSetGenerator : MonoBehaviour
{
    [MenuItem("Tools/Generate ScLanguageSet Class")]
    public static void Generate()
    {
        Debug.Log("===== 클래스 파일 생성 시작 =====");
        
        var langDict = ScCsvLoader.Parse(LanguageManager.LanguageFilePath);
        
        string classCode = "public class ScLanguageSet\n{\n";

        foreach (var pair in langDict)
        {
            string typeName = nameof(ScLanguageData);
            string propName = pair.Key;
            string useTTS = pair.Value.UseTTS.ToString().ToLower();
            
            classCode += $"    public {typeName} {propName} {{ get; }} = new(\"{pair.Key}\", {useTTS});\n";
        }
        
        classCode += "}";

        string outputPath = Application.dataPath + "/@Scripts/Datas/ScLanguageSet.cs";
        File.WriteAllText(outputPath, classCode);
        AssetDatabase.Refresh();

        Debug.Log($"===== 클래스 파일 생성 완료: {outputPath} =====");
    }
}