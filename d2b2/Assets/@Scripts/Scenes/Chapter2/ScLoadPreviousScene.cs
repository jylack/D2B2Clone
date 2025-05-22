using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class ScLoadPreviousScene : MonoBehaviour
{
    [SerializeField] private Button BackBtn;
    [SerializeField] private Button RetryBtn;
    [SerializeField] private PlayableDirector director;

    private void Start()
    {
        BackBtn.onClick.AddListener(() => Manager.Instance.SceneMgr.LoadPreviousScene());
        if (director != null)
        {
            RetryBtn.onClick.AddListener(() =>
                RestartTimeline());
        }
    }


    public void RestartTimeline()
    {
        director.Stop();              // 재생 중단
        director.time = 0;            // 처음으로 이동
        director.Evaluate();          // 즉시 상태 반영
        director.Play();              // 다시 재생
    }
}
