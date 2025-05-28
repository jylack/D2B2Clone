using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ScMissionGuidePanel : MonoBehaviour
{    
    //[SerializeField] private List<string> missionText;
    private List<ScMissionBoxTextCheck> missionTexts = new List<ScMissionBoxTextCheck>();
    private GameObject MissionPrebs;


    // Start is called before the first frame update
    void Start()
    {
        //นÝบน
        //MissionPrebs = ResourceManager.InstantiatePrefab("Prefabs/MissionText", transform);
        //MissionPrebs.GetComponent<ScMissionBoxTextCheck>().SetMissionText("Mission Guide", MissionBoxTextCheckType.Title);
        
        //missionTexts.Add(MissionPrebs.GetComponent<ScMissionBoxTextCheck>());

        //foreach(var mission in ScStringTable.DialogueMap)
        //{
        //    missionText.Add(mission.Value);
        //}
        //if(panelText == null || panelText.Count == 0)
        //{
        //    Debug.LogError("Panel text is not set or empty.");
        //    return;
        //}
        //panelText[0].fontStyle = FontStyles.Bold;
        //panelText[0].color = Color.white;

        //for (int i = 1; i < panelText.Count; i++)
        //{
        //    panelText[i].color = Color.red;
        //}
    }

   
}
