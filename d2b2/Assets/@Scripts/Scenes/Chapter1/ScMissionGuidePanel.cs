using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ScMissionGuidePanel : MonoBehaviour
{
    //[SerializeField] private List<string> missionText;
    private List<ScMissionBoxTextCheck> missionTexts = new List<ScMissionBoxTextCheck>();
    private GameObject MissionPrebs;
    private Dictionary<string, string> missionText = new Dictionary<string, string>();
    private async UniTask Start()
    {
        if(Manager.Instance)
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

        missionTexts[0].SetTypeChange(MissionBoxTextCheckType.Title);

    }

    //여기서 좌우확인 멈추기 등등 이벤트 등록해서 사용함될듯?

}
