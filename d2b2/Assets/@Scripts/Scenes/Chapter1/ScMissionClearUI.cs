using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public class ScMissionClearUI : MonoBehaviour
{
    [Header("클리어 텍스트 키")]
    [SerializeField] private string missionClearTextKey;

    [Header("오브젝트 연결")]
    [SerializeField] private GameObject clearMissionListTextPanel;
    [SerializeField] private TextMeshProUGUI missionClearText;
    [SerializeField] private TextMeshProUGUI nickNameText;
    [SerializeField] private ScMissionListPanel missionListPanel;
    [SerializeField] private GameObject missionClearUI;

    [Header("다음 씬 설정")]
    [SerializeField] private ScDefine.ScScene nextScene;

    [Header("미션클리어 증서 팝업 딜레이")]
    [SerializeField] private float missionClearUIDelay = 1f;


    private void Start()
    {
        Manager.Instance.SoundMgr.PlaySfx(ScDefine.ScSound.ChapterFinished);

        if (missionListPanel != null)
        {

            foreach (var mission in missionListPanel.MissionListTests)
            {
                // 미션이 완료된 경우
                if (mission.GetMissionState())
                {
                    Instantiate(mission.gameObject, clearMissionListTextPanel.transform);
                }
            }

            ManagerSetting().Forget();
        }

        MissionClearImageFadeOut().Forget();
    }

    private async UniTask MissionClearImageFadeOut()
    {
        int waitTime = (int)(missionClearUIDelay * 1000);

        await UniTask.Delay(waitTime);

        missionClearUI.gameObject.SetActive(true);
    }


    private async UniTask ManagerSetting()
    {

        await UniTask.WaitUntil(() => Manager.Instance != null);
        await UniTask.WaitUntil(() => Manager.Instance.LanguageMgr != null);

        missionClearText.text = Manager.Instance.LanguageMgr.GetText(missionClearTextKey);

        await UniTask.WaitUntil(() => Manager.Instance.GameMgr != null);

        nickNameText.text += Manager.Instance.GameMgr.NickName;
    }

    public void NextSceneLoad()
    {
        Manager.Instance.SoundMgr.PlaySfx(ScDefine.ScSound.Ok);

        Manager.Instance.SceneMgr.LoadScene(nextScene);
    }
}
