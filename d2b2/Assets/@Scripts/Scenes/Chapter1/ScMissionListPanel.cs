using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;

public enum Chapter
{
    Ch1,
    Ch2,
    Ch3,
    Tutorial,
}

public enum Category
{
    MissionText,
}

public class ScMissionListPanel : MonoBehaviour
{

    /// <summary>
    /// _기준으로 앞과 뒤를 정해준뒤 맨뒤에 숫자로 순서만 정해주면 됩니다.
    /// </summary>
    [Header("Mission 필터")]
    [Tooltip("챕터를 선택하세요.")]
    [SerializeField] private Chapter ChapterPrefix = Chapter.Ch1;

    [Tooltip("카테고리를 선택하세요.")]
    [SerializeField] private Category CategoryFilter = Category.MissionText;

    [Header("List")]
    [SerializeField] private ScMissionList missionListPrefab;

    public List<ScMissionBoxTextCheck> MissionListTests => missionListPrefab.GetMissionTextList();

    private void Start()
    {
        try
        {
            missionListPrefab.Setup(ChapterPrefix, CategoryFilter);
        }
        catch(Exception ex)
        {
            Debug.Log(ex);
        }
    }

}
