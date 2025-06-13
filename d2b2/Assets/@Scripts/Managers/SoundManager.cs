using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private AudioClip ok;
    [SerializeField] private AudioClip cancel;
    [SerializeField] private AudioClip simpleNotification;
    [Header("Game")]
    [SerializeField] private AudioClip trafficLightChanged;
    [SerializeField] private AudioClip chapterFinished;
    [SerializeField] private AudioClip catchSomething;
    [SerializeField] private AudioClip positiveNotification;
    [SerializeField] private AudioClip negativeNotification;
    [SerializeField] private AudioClip chapterMissionClear;
    [SerializeField] private AudioClip carSkid;
    [SerializeField] private AudioClip miniGamecountDown;
    [SerializeField] private AudioClip miniGameStart;
    [SerializeField] private AudioClip miniGameEnd;

    [Header("BGM")]
    [SerializeField] private AudioClip defaultBgm;
    [SerializeField] private AudioClip chapterBgm;

    private AudioSource soundBgm;
    private AudioSource soundVoice;
    private AudioSource soundBgmSfx;
    private Dictionary<string, AudioClip> voiceDict = new();

    

    private void Awake()
    {
        soundBgm = gameObject.AddComponent<AudioSource>();
        soundVoice = gameObject.AddComponent<AudioSource>();
        soundBgmSfx = gameObject.AddComponent<AudioSource>();

        SetDefaultSettings(soundBgm);
        SetDefaultSettings(soundVoice);
        SetDefaultSettings(soundBgmSfx);

        AudioClip[] audioClips = Resources.LoadAll<AudioClip>("TTS");
        foreach (AudioClip audioClip in audioClips)
            voiceDict.Add(audioClip.name, audioClip);
    }

    private void Start()
    {
        PlayDefaultBgm();
    }



    public void PlayDefaultBgm()
    {
        PlayBgm(defaultBgm).Forget();
    }

    public void PlayChapterBgm()
    {
        PlayBgm(chapterBgm).Forget();
    }

    public void PlayVoice(string voiceKey)
    {
        if (voiceDict.TryGetValue(voiceKey, out AudioClip audioClip))
            soundVoice.PlayOneShot(audioClip);        
    }

    public void PlaySfx(ScDefine.ScSound sound)
    {
        switch (sound)
        {
            case ScDefine.ScSound.Ok:                   PlayOneShot(ok);                    break;
            case ScDefine.ScSound.Cancel:               PlayOneShot(cancel);                break;
            case ScDefine.ScSound.SimpleNotification:   PlayOneShot(simpleNotification);    break;

            case ScDefine.ScSound.TrafficLightChanged:  PlayOneShot(trafficLightChanged);   break;
            case ScDefine.ScSound.ChapterFinished:      PlayOneShot(chapterFinished);       break;
            case ScDefine.ScSound.CatchSomething:       PlayOneShot(catchSomething);        break;
            case ScDefine.ScSound.PositiveNotification: PlayOneShot(positiveNotification);  break;
            case ScDefine.ScSound.NegativeNotification: PlayOneShot(negativeNotification);  break;
            case ScDefine.ScSound.ChapterMissionClear:  PlayOneShot(chapterMissionClear);   break;
            case ScDefine.ScSound.CarSkid:              PlayOneShot(carSkid);               break;
            case ScDefine.ScSound.Ch2_2_CountDown: PlayOneShot(miniGamecountDown);          break;
            case ScDefine.ScSound.Ch2_2_GameStart: PlayOneShot(miniGameStart);              break;
            case ScDefine.ScSound.Ch2_2_GameEnd: PlayOneShot(miniGameEnd);                  break;
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



    private void SetDefaultSettings(AudioSource audioSource)
    {
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0;
    }

    private void PlayOneShot(AudioClip audioClip)
    {
        soundBgmSfx.PlayOneShot(audioClip);
    }

    private async UniTask PlayBgm(AudioClip bgm)
    {
        if (soundBgm.clip == bgm)
            return;

        float volume = soundBgm.volume;

        await soundBgm.DOFade(0f, 0.5f);
        soundBgm.Stop();

        soundBgm.clip = bgm;
        soundBgm.loop = true;
        soundBgm.Play();
        await soundBgm.DOFade(volume, 0.5f);
    }
}