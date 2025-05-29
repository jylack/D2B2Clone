using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class ScMissionListPanel : MonoBehaviour
{
    private List<ScMissionBoxTextCheck> missionTexts = new List<ScMissionBoxTextCheck>();
    private Dictionary<string, string> missionText = new Dictionary<string, string>();


    private void Start()
    {
        InitalizeAsync().Forget();

    }

    private async UniTask InitalizeAsync()
    {
        if (Manager.Instance)
        {
            await new WaitUntil(() => Manager.Instance.LanguageMgr != null);
        }

        foreach (var dir in Manager.Instance.LanguageMgr.DialogueMap)
        {
            string[] parts = dir.Key.Split('_');

            if (parts.Length > 2)
            {
                if (parts[0] == "Ch1" && parts[1] == "MissionText")
                {
                    if (parts[2] == "0")
                    {
                        missionText.Add(dir.Key, dir.Value.Text);
                        continue;
                    }
                    missionText.Add(dir.Key, parts[2] + ". " + dir.Value.Text);
                }
            }
        }

        foreach (var mission in missionText)
        {
            MissionTextCreate(mission.Key, mission.Value);
        }

        SetMissionTextTypeChange(0, MissionBoxTextCheckType.Title);
    }




    public void SetMissionTextTypeChange(int idx, MissionBoxTextCheckType type)
    {
        if (idx < 0 || idx >= missionTexts.Count)
        {
            Debug.LogError("Index out of range for missionTexts.");
            return;
        }

        missionTexts[idx].SetTypeChange(type);
    }

    private void MissionTextCreate(string key, string text)
    {
        var obj = ResourceManager.InstantiatePrefab("Prefabs/MissionText", transform);
        var ctrl = obj.GetComponent<ScMissionBoxTextCheck>();

        ctrl.SetMissionText(text, MissionBoxTextCheckType.Base);
        obj.name = key;

        missionTexts.Add(ctrl);
    }

    public void SetMissionTextCheck(string key, bool check)
    {
        if (missionText.ContainsKey(key))
        {
            foreach (var mission in missionTexts)
            {
                if (mission.name == key)
                {
                    mission.SetCheck(check);
                    break;
                }
            }
        }
        else
        {
            Debug.LogError($"Mission text with key {key} does not exist.");
        }
    }



}
