using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScMissionClearUI : MonoBehaviour
{
    [Header("클리어 텍스트 키")]
    [SerializeField] private string missionClearTextKey;

    [Header("오브젝트 연결")]
    [SerializeField] private GameObject clearMissionListTextPanel;
    [SerializeField] private TextMeshProUGUI missionClearText;
    [SerializeField] private TextMeshProUGUI nickNameText;
    [SerializeField] private ScMissionListPanel missionListPanel;
    [SerializeField] private Image missionClearImage;
    [SerializeField] private GameObject missionClearUI;

    [Header("다음 씬 설정")]
    [SerializeField] private ScDefine.ScScene nextScene;

    [Header("미션클리어 이미지 띄울 시간")]
    [SerializeField] private float missionClearImageShowTime = 1f;

    private List<ScMissionBoxTextCheck> missionList;

    private float startTime = 0f;

    private void Start()
    {
        Manager.Instance.SoundMgr.PlaySfx(ScDefine.ScSound.ChapterFinished);

        // 미션 클리어 텍스트 활성화 시간
        startTime = Time.time;

        if (missionListPanel != null)
        {
            missionList = missionListPanel.GetMissionTextList();

            foreach (var mission in missionList)
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
        float delayTime = Time.time - startTime;

        if (delayTime < missionClearImageShowTime)
        {
            float tempTime = (missionClearImageShowTime - delayTime);
            int waitTime = (int)(tempTime * 1000);

            await UniTask.Delay(waitTime);
        }

        missionClearImage.gameObject.SetActive(false);
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
