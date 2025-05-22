using System.Collections;
using System.Collections.Generic;
using Timeline.Samples;
using UnityEditor;
using UnityEngine;
using UnityEngine.Timeline;

[InitializeOnLoad]
public class ScTextPlayerableHsy
{
    static bool injected = false;

    static ScTextPlayerableHsy()
    {
        EditorApplication.update += InjectTexts;
    }

    static void InjectTexts()
    {
        if (injected) return;

        string[] guids = AssetDatabase.FindAssets("t:TimelineAsset");
        foreach (var guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            var timeline = AssetDatabase.LoadAssetAtPath<TimelineAsset>(path);

            foreach (var track in timeline.GetOutputTracks())
            {
                foreach (var clip in track.GetClips())
                {
                    if (clip.asset is TextPlayableAsset textAsset)
                    {
                        int dialogueId = (int)clip.start; // 예: 0초면 대사 0번
                        if (ScTextHsy.DialogueMap.TryGetValue(dialogueId, out var content))
                        {
                            textAsset.template.text = content;
                            EditorUtility.SetDirty(timeline);
                        }
                    }
                }
            }
        }

        AssetDatabase.SaveAssets();
        injected = true;

        Debug.Log("스크립트에서 텍스트 자동 주입 완료");
    }
}
