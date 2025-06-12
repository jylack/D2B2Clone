using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public abstract class ScPlayerBase : ScObjectBase
{
    public abstract Camera MainCamera { get; }

    protected AudioSource soundBgm;
    protected AudioSource soundTalk;
    protected AudioSource soundSfx;



    public void InitSound(GameObject obj)
    {
        soundBgm = obj.AddComponent<AudioSource>();
        soundTalk = obj.AddComponent<AudioSource>();
        soundSfx = obj.AddComponent<AudioSource>();
    }

    public async UniTaskVoid PlayBgm(AudioClip audioClip)
    {
        float volume = soundBgm.volume;

        await soundBgm.DOFade(0f, 1f);
        soundBgm.Stop();

        soundBgm.clip = audioClip;
        soundBgm.loop = true;
        soundBgm.Play();
        await soundBgm.DOFade(volume, 1f);
    }

    public void PlayTalk(AudioClip audioClip)
    {
        soundTalk.Stop();

        soundTalk.clip = audioClip;
        soundBgm.Play();
    }

    public void PlaySfx(AudioClip audioClip)
    {
        soundSfx.PlayOneShot(audioClip);
    }
}
