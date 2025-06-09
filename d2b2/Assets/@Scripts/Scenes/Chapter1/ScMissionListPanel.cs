using Cysharp.Threading.Tasks;
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
    [Tooltip("챕터(접두어)를 선택하세요.")]
    [SerializeField] private Chapter ChapterPrefix = Chapter.Ch1;

    [Tooltip("카테고리를 선택하세요.")]
    [SerializeField] private Category CategoryFilter = Category.MissionText;

    private readonly List<ScMissionBoxTextCheck> missionTexts = new();
    private readonly Dictionary<string, string> missionTextMap = new();
    private string defaultKey = string.Empty;

    private void Start()
    {
        // 기본 로드
        LoadMissionsAsync().Forget();
        defaultKey = ChapterPrefix.ToString() + "_" + CategoryFilter.ToString() + "_";

        Setup(ChapterPrefix, CategoryFilter);
    }

    /// <summary>
    /// 외부에서 필터를 설정하고 즉시 로드합니다.
    /// </summary>
    public void Setup(Chapter chapterPrefix, Category category)
    {
        ChapterPrefix = chapterPrefix;
        CategoryFilter = category;
        LoadMissionsAsync().Forget();
    }

    public void CheckMission(Chapter ch,int index, bool isChecked)
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
        SetMissionTextCheck(key, true);
    }

    public void OnMissionFailed(int index)
    {
        string key = defaultKey + index.ToString();
        SetMissionTextCheck(key, false);
    }

    private async UniTask LoadMissionsAsync()
    {
        if (Manager.Instance != null)
            await new WaitUntil(() => Manager.Instance.LanguageMgr != null);

        missionTextMap.Clear();
        missionTexts.Clear();

        foreach (Transform child in transform)
            Destroy(child.gameObject);

        // DialogueMap 중에서 prefix_category_* 키만 추출
        foreach (var kv in Manager.Instance.LanguageMgr.DialogueMap)
        {
            var parts = kv.Key.Split('_');

            if (parts.Length > 2
             && parts[0] == ChapterPrefix.ToString()
             && parts[1] == CategoryFilter.ToString())
            {
                string text = parts[2] == "0" ? kv.Value.Text : $"{parts[2]}. {kv.Value.Text}";
                missionTextMap[kv.Key] = text;
            }
        }

        // UI 생성
        foreach (var kv in missionTextMap)
            CreateMissionEntry(kv.Key, kv.Value);

        // 첫 항목만 Title 타입으로 변경
        if (missionTexts.Count > 0)
            missionTexts[0].SetTypeChange(MissionBoxTextCheckType.Title);
    }    

    private void CreateMissionEntry(string key, string text)
    {
        var go = ResourceManager.InstantiatePrefab("Prefabs/MissionText", transform);
        var ctrl = go.GetComponent<ScMissionBoxTextCheck>();
        ctrl.SetMissionText(text, MissionBoxTextCheckType.Base);
        go.name = key;
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
