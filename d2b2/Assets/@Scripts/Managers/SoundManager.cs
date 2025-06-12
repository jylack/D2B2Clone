using Cysharp.Threading.Tasks;
using DG.Tweening;
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

    private AudioSource soundBgm;
    private AudioSource soundTalk;
    private AudioSource soundBgmSfx;



    private void Awake()
    {
        soundBgm = gameObject.AddComponent<AudioSource>();
        soundTalk = gameObject.AddComponent<AudioSource>();
        soundBgmSfx = gameObject.AddComponent<AudioSource>();
    }

    private void Start()
    {
        PlayDefaultBgm().Forget();
    }



    public async UniTaskVoid PlayDefaultBgm()
    {
        await PlayBgm(defaultBgm);
    }

    public async UniTaskVoid PlayChapterBgm()
    {
        await PlayBgm(chapterBgm);
    }

    public void PlayTalk()
    {
        // TODO
    }

    public void PlaySfx(ScDefine.ScSound sound)
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

    public void SetBgmVolume(float volume)
    {
        soundBgm.volume = volume;
    }

    public void SetSfxVolume(float volume)
    {
        soundBgmSfx.volume = volume;
    }



    private void PlaySound(AudioClip audioClip)
    {
        soundBgmSfx.PlayOneShot(audioClip);
    }

    private async UniTask PlayBgm(AudioClip bgm)
    {
        float volume = soundBgm.volume;

        await soundBgm.DOFade(0f, 1f);
        soundBgm.Stop();

        soundBgm.clip = bgm;
        soundBgm.loop = true;
        soundBgm.Play();
        await soundBgm.DOFade(volume, 1f);
    }
}