using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class ScSettingApplier : MonoBehaviour
{
    [SerializeField] private bool isTutorial;
    [SerializeField] private Slider renderScaleSlider;
    [SerializeField] private Slider brightnessSlider;

    [SerializeField] private ScMultiButtonSelect colorWeakButtons;
    [SerializeField] private Slider colorWeakCompensateSlider;

    [SerializeField] private TMP_Dropdown rotateMode;
    [SerializeField] private Slider rotationAngleSlider;
    [SerializeField] private Slider rotationSpeedSlider;

    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider bgmVolumeSlider;
    [SerializeField] private Slider voiceVolumeSlider;
    [SerializeField] private Slider soundEffectSlider;


    void Start()
    {
        gameObject.SetActive(false);
    }

    public void LoadPlayer()
    {
        try
        {
            ScPlayerSettingsEntity settings = Manager.Instance.GameMgr.PlayerSettings;
            if (settings != null)
                ApplySettings(settings);
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }

    public void SavePlayer()
    {
        ScPlayerSettingsEntity playerSetting = new();

        playerSetting.renderScale = renderScaleSlider.value;
        playerSetting.brightness = brightnessSlider.value;

        playerSetting.colorWeakMode = colorWeakButtons.Selected;
        playerSetting.colorWeakCompensate = colorWeakCompensateSlider.value;

        playerSetting.rotateMode = rotateMode.value;
        playerSetting.rotationAngle = rotationAngleSlider.value;
        playerSetting.rotationSpeed = rotationSpeedSlider.value;

        playerSetting.masterVolume = masterVolumeSlider.value;
        playerSetting.bgmVolume = bgmVolumeSlider.value;
        playerSetting.voiceVolume = voiceVolumeSlider.value;
        playerSetting.soundEffectVolume = soundEffectSlider.value;

        ScPlayerEntity entity = new ScPlayerEntity
        {
            nickName = Manager.Instance.GameMgr.NickName,
            settings = playerSetting
        };

        Manager.Instance.GameMgr.SetCurrentPlayerInfo(entity);
        Manager.Instance.DbMgr.Save(Manager.Instance.GameMgr.NickName, entity).Forget();
    }

    public void ApplySettings(ScPlayerSettingsEntity playerSettings)
    {
        renderScaleSlider.value = playerSettings.renderScale;
        brightnessSlider.value = playerSettings.brightness;

        colorWeakCompensateSlider.value = playerSettings.colorWeakCompensate;

        rotateMode.value = playerSettings.rotateMode;
        rotationAngleSlider.value = playerSettings.rotationAngle;
        rotationSpeedSlider.value = playerSettings.rotationSpeed;

        masterVolumeSlider.value = playerSettings.masterVolume;
        bgmVolumeSlider.value = playerSettings.bgmVolume;
        voiceVolumeSlider.value = playerSettings.voiceVolume;
        soundEffectSlider.value = playerSettings.soundEffectVolume;


        renderScaleSlider.onValueChanged?.Invoke(renderScaleSlider.value);
        brightnessSlider.onValueChanged?.Invoke(brightnessSlider.value);

        colorWeakButtons.SettingApply(playerSettings.rotateMode);
        colorWeakCompensateSlider.onValueChanged?.Invoke(colorWeakCompensateSlider.value);

        rotateMode.onValueChanged?.Invoke(rotateMode.value);
        rotationAngleSlider.onValueChanged?.Invoke(rotationAngleSlider.value);
        rotationSpeedSlider.onValueChanged?.Invoke(rotationSpeedSlider.value);

        masterVolumeSlider.onValueChanged?.Invoke(masterVolumeSlider.value);
        bgmVolumeSlider.onValueChanged?.Invoke(bgmVolumeSlider.value);
        voiceVolumeSlider.onValueChanged?.Invoke(voiceVolumeSlider.value);
        soundEffectSlider.onValueChanged?.Invoke(soundEffectSlider.value);
    }
}
