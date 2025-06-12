using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private AudioClip ok;
    [SerializeField] private AudioClip cancel;
    [Header("Game")]
    [SerializeField] private AudioClip trafficLightChanged;
    [SerializeField] private AudioClip chapterFinished;
    [SerializeField] private AudioClip catchSomething;
    [SerializeField] private AudioClip positiveNotification;
    [SerializeField] private AudioClip negativeNotification;
    [Header("BGM")]
    [SerializeField] private AudioClip defaultBgm;
    [SerializeField] private AudioClip chapterBgm;
    

    
    
    public void Play(ScDefine.ScSound sound)
    {
        switch (sound)
        {
            case ScDefine.ScSound.Ok:                   PlaySound(ok);                      break;
            case ScDefine.ScSound.Cancel:               PlaySound(cancel);                  break;
            case ScDefine.ScSound.TrafficLightChanged:  PlaySound(trafficLightChanged);     break;
            case ScDefine.ScSound.ChapterFinished:      PlaySound(chapterFinished);         break;
            case ScDefine.ScSound.CatchSomething:       PlaySound(catchSomething);          break;
            case ScDefine.ScSound.PositiveNotification: PlaySound(positiveNotification);    break;
            case ScDefine.ScSound.NegativeNotification: PlaySound(negativeNotification);    break;
        }
    }



    private void PlaySound(AudioClip audioClip)
    {
        Manager.Instance.GameMgr.Player?.PlayBgm(audioClip);
    }
}