using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScMissionListPanel : MonoBehaviour
{
    //[SerializeField] private List<string> missionText;
    private List<ScMissionBoxTextCheck> missionTexts = new List<ScMissionBoxTextCheck>();
    private GameObject MissionPrebs;
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
            MissionPrebs = ResourceManager.InstantiatePrefab("Prefabs/MissionText", transform);
            MissionPrebs.GetComponent<ScMissionBoxTextCheck>().SetMissionText(mission.Value, MissionBoxTextCheckType.Base);
            MissionPrebs.name = mission.Key;
            missionTexts.Add(MissionPrebs.GetComponent<ScMissionBoxTextCheck>());
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

    public void SetMissionText(string key, string text, MissionBoxTextCheckType type)
    {
        if (missionText.ContainsKey(key))
        {
            missionText[key] = text;
            foreach (var mission in missionTexts)
            {
                if (mission.name == key)
                {
                    mission.SetMissionText(text, type);
                    break;
                }
            }
        }
        else
        {
            Debug.LogError($"Mission text with key {key} does not exist.");
        }
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
