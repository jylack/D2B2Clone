using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScSoundSetting : MonoBehaviour
{
    [SerializeField] private ScSettingSlider masterVolume;
    [SerializeField] private ScSettingSlider bgmVolume;
    [SerializeField] private ScSettingSlider voiceVolume;
    [SerializeField] private ScSettingSlider sfxVolume;

    private void Awake()
    {
        bgmVolume.slider.onValueChanged.AddListener(v => Manager.Instance.SoundMgr.SetBgmVolume(v * masterVolume.Value));
        voiceVolume.slider.onValueChanged.AddListener(v => Manager.Instance.SoundMgr.SetVoiceVolume(v * masterVolume.Value));
        sfxVolume.slider.onValueChanged.AddListener(v => Manager.Instance.SoundMgr.SetSfxVolume(v * masterVolume.Value));

        gameObject.SetActive(false);
    }

    public void ApplyMasterVolume()
    {
        Manager.Instance.SoundMgr.SetBgmVolume(bgmVolume.Value * masterVolume.Value);
        Manager.Instance.SoundMgr.SetVoiceVolume(voiceVolume.Value * masterVolume.Value);
        Manager.Instance.SoundMgr.SetSfxVolume(sfxVolume.Value * masterVolume.Value);
    }
}
