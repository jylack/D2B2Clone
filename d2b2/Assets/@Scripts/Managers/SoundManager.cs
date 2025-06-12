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
    private AudioSource soundVoice;
    private AudioSource soundBgmSfx;



    private void Awake()
    {
        soundBgm = gameObject.AddComponent<AudioSource>();
        soundVoice = gameObject.AddComponent<AudioSource>();
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

    public void PlayVoice()
    {
        // TODO
    }

    public void PlaySfx(ScDefine.ScSound sound)
    {
        switch (sound)
        {
            case ScDefine.ScSound.Ok:                   PlayOneShot(ok);                    break;
            case ScDefine.ScSound.Cancel:               PlayOneShot(cancel);                break;
            case ScDefine.ScSound.TrafficLightChanged:  PlayOneShot(trafficLightChanged);   break;
            case ScDefine.ScSound.ChapterFinished:      PlayOneShot(chapterFinished);       break;
            case ScDefine.ScSound.CatchSomething:       PlayOneShot(catchSomething);        break;
            case ScDefine.ScSound.PositiveNotification: PlayOneShot(positiveNotification);  break;
            case ScDefine.ScSound.NegativeNotification: PlayOneShot(negativeNotification);  break;
        }
    }

    public void SetBgmVolume(float volume)
    {
        soundBgm.volume = volume;
    }

    public void SetVoiceVolume(float volume)
    {
        soundVoice.volume = volume;
    }

    public void SetSfxVolume(float volume)
    {
        soundBgmSfx.volume = volume;
    }



    private void PlayOneShot(AudioClip audioClip)
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