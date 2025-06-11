using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScMissionClearUI : MonoBehaviour
{
    [Header("Ŭ���� �ؽ�Ʈ Ű")]
    [SerializeField] private string missionClearTextKey; // �̼� Ŭ���� �ؽ�Ʈ Ű

    [Header("������Ʈ ����")]
    [SerializeField] private GameObject clearMissionListTextPanel;
    [SerializeField] private TextMeshProUGUI missionClearText;
    [SerializeField] private TextMeshProUGUI NickNameText;

    [Header("���� �� ����")]
    [SerializeField] private ScDefine.ScScene nextScene;

    private List<ScMissionBoxTextCheck> missionList;

    private void Start()
    {
        var missionListPanel = GameObject.Find("MissionListPanel");
        
        missionList = missionListPanel.GetComponent<ScMissionListPanel>().GetMissionTextList();
        

        foreach (var mission in missionList)
        {
            // �̼��� �Ϸ�� ���
            if (mission.GetMissionState())
            {
                Instantiate(mission.gameObject, clearMissionListTextPanel.transform);
            }
        }
        
        ManagerSetting().Forget();
    }

    private async UniTask ManagerSetting()
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
