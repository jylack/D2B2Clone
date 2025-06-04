using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScMissionClearUI : MonoBehaviour
{
    private List<ScMissionBoxTextCheck> missionList;
    [SerializeField] private GameObject missionClearTextPanel;


    //[SerializeField] private List<Image> Seals;

    private void Start()
    {
        missionList = GameObject.Find("MissionListPanel").GetComponent<ScMissionListPanel>().GetMissionTextList();
        foreach(var mission in missionList)
        {
            // 미션이 완료된 경우
            if (mission.GetMissionState())
            {
                Instantiate(mission.gameObject, missionClearTextPanel.transform);
            }
        }

        //for(int i = 0 ; i < missionList.Count; i++)
        //{
        //    if (missionList[i].GetMissionState())
        //    {
        //        // 미션이 완료된 경우
                
        //    }
        //    else
        //    {
        //        // 미션이 완료되지 않은 경우
                
        //    }
        //}

        //foreach (var mission in missionList)
        //{
        //    if (mission.GetMissionState())
        //    {
        //        // 미션이 완료된 경우
        //        Image seal = Instantiate(Seals[0], transform);
        //        seal.gameObject.SetActive(true);
        //        seal.transform.localScale = Vector3.one;
        //        seal.transform.localPosition = Vector3.zero;
        //    }
        //    else
        //    {
        //        // 미션이 완료되지 않은 경우
        //        Image seal = Instantiate(Seals[1], transform);
        //        seal.gameObject.SetActive(true);
        //        seal.transform.localScale = Vector3.one;
        //        seal.transform.localPosition = Vector3.zero;
        //    }
        //}
    }
}
