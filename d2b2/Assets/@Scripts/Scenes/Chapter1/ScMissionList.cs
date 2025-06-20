using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;



public class ScMissionList : MonoBehaviour
{
    private Chapter ChapterPrefix;
    private Category CategoryFilter;

    private readonly List<ScMissionBoxTextCheck> missionTexts = new();
    private readonly Dictionary<string, string> missionTextMap = new();

    private StringTable table;

    private const string LocalizationTableName = "LocalizationTable";
    private string defaultKey = string.Empty;


    /// <summary>
    /// 외부에서 필터를 설정하고 즉시 로드합니다.
    /// </summary>
    public void Setup(Chapter chapterPrefix, Category category)
    {
        ChapterPrefix = chapterPrefix;
        CategoryFilter = category;
        // 기본 로드
        LoadMissions();
        defaultKey = ChapterPrefix.ToString() + "_" + CategoryFilter.ToString() + "_";
    }

    public void CheckMission(Chapter ch, int index, bool isChecked)
    {
        string key = ch.ToString() + "_" + CategoryFilter.ToString() + "_" + index.ToString();
        SetMissionTextCheck(key, isChecked);
    }

    /// <summary>
    /// 키와 체크 값만 전달하면 해당 미션에 체크를 설정합니다.
    /// </summary>
    public void CheckMission(string key, bool isChecked)
    {
        SetMissionTextCheck(key, isChecked);
    }

    public void OnMissionClear(int index)
    {

        string key = defaultKey + index.ToString();

        Manager.Instance.SoundMgr.PlaySfx(ScDefine.ScSound.ChapterMissionClear);

        SetMissionTextCheck
            (key, true);
    }

    public void OnMissionFailed(int index)
    {
        string key = defaultKey + index.ToString();
        SetMissionTextCheck(key, false);
    }

    private void LoadMissions()
    {

        missionTextMap.Clear();
        missionTexts.Clear();

        Locale locale = LocalizationSettings.SelectedLocale;
        table = LocalizationSettings.StringDatabase.GetTable(LocalizationTableName, locale);

        foreach (Transform child in transform)
            Destroy(child.gameObject);

        var langMgr = Manager.Instance.LanguageMgr;

        if (table == null)
        {
            Debug.LogError("Localization table is not loaded.");
            return;
        }

        foreach (var kv in table.Values)
        {
            var parts = kv.Key.Split('_');

            if (parts.Length > 2 &&
                parts[0] == ChapterPrefix.ToString() &&
                parts[1] == CategoryFilter.ToString())
            {
                string text = parts[2] == "0" ? kv.Value : $"{parts[2]}. {kv.Value}";
                missionTextMap[kv.Key] = text;
            }
        }

        var tempTextMap = missionTextMap.OrderBy(k => k.Key);

        //if (ChapterPrefix == Chapter.Ch1)
        //{
        //    string titleKey       = "Ch1_MissionText_0";
        //    string mission1Key    = "Ch1_MissionText_1";
        //    string mission2Key    = "Ch1_MissionText_2";
        //    string mission3Key    = "Ch1_MissionText_3";

        //    missionTextMap[titleKey] = langMgr.GetText(titleKey);
        //    missionTextMap[mission1Key] = $"1.{langMgr.GetText(mission1Key)}";
        //    missionTextMap[mission2Key] = $"2.{langMgr.GetText(mission2Key)}";
        //    missionTextMap[mission3Key] = $"3.{langMgr.GetText(mission3Key)}";
        //}
        //else if (ChapterPrefix == Chapter.Ch2)
        //{
        //    string titleKey = "Ch2_MissionText_0";
        //    string mission1Key = "Ch2_MissionText_1";
        //    string mission2Key = "Ch2_MissionText_2";

        //    missionTextMap[titleKey] = langMgr.GetText(titleKey);
        //    missionTextMap[mission1Key] = $"1.{langMgr.GetText(mission1Key)}";
        //    missionTextMap[mission2Key] = $"2.{langMgr.GetText(mission2Key)}";
        //}

        // UI 생성
        foreach (var kv in tempTextMap)
            CreateMissionEntry(kv);

        // 첫 항목만 Title 타입으로 변경
        if (missionTexts.Count > 0)
            missionTexts[0].SetTypeChange(MissionBoxTextCheckType.Title);
    }

    private void CreateMissionEntry(KeyValuePair<string, string> kv)
    {
        var go = ResourceManager.InstantiatePrefab("Prefabs/MissionText", transform);
        var ctrl = go.GetComponent<ScMissionBoxTextCheck>();

        ctrl.SetMissionText(kv.Value);
        go.name = kv.Key;
        missionTexts.Add(ctrl);
    }

    private void SetMissionTextCheck(string key, bool check)
    {
        var ctrl = missionTexts.Find(m => m.name == key);
        if (ctrl != null)
            ctrl.SetCheck(check);
        else
            Debug.LogError($"ScMissionListPanel: 키 '{key}'의 미션 텍스트를 찾을 수 없습니다.");
    }

    public List<ScMissionBoxTextCheck> GetMissionTextList()
    {
        return missionTexts;
    }
}
