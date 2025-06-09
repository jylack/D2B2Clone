using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScMissionClearUI : MonoBehaviour
{
    [Header("클리어 텍스트 키")]
    [SerializeField] private string missionClearTextKey; // 미션 클리어 텍스트 키

    [Header("오브젝트 연결")]
    [SerializeField] private GameObject clearMissionListTextPanel;
    [SerializeField] private TextMeshProUGUI missionClearText;
    [SerializeField] private TextMeshProUGUI NickNameText;

    [Header("다음 씬 설정")]
    [SerializeField] private ScDefine.ScScene nextScene;

    private List<ScMissionBoxTextCheck> missionList;

    private void Start()
    {
        missionList = GameObject.Find("MissionListPanel").GetComponent<ScMissionListPanel>().GetMissionTextList();
        foreach (var mission in missionList)
        {
            // 미션이 완료된 경우
            if (mission.GetMissionState())
            {
                Instantiate(mission.gameObject, clearMissionListTextPanel.transform);
            }
        }
        
        ManagerSetting();
    }

    private async void ManagerSetting()
    {
        await UniTask.WaitUntil(() => Manager.Instance != null);
        await UniTask.WaitUntil(() => Manager.Instance.LanguageMgr != null);

        missionClearText.text = Manager.Instance.LanguageMgr.GetText(missionClearTextKey);

        
        await UniTask.WaitUntil(()=> Manager.Instance.GameMgr != null);
        
    
        NickNameText.text += Manager.Instance.GameMgr.NickName;

     
    }

    public void EndGame()
    {
        Manager.Instance.SceneMgr.LoadScene(nextScene);
    }
}
