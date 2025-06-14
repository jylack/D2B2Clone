using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public delegate void OnPlayerHeadTurnHandler(ScDefine.ScHeadTurn headTurn);
public delegate void OnPlayerHandsUpHandler(bool leftHandUp, bool rightHandUp, float distance);
public delegate void OnPlayerMovingHandler(bool isMoving);

public class GameManager : MonoBehaviour
{
    public event OnPlayerHeadTurnHandler OnPlayerHeadTurn;
    public event OnPlayerHandsUpHandler OnPlayerHandsUp;
    public event OnPlayerMovingHandler OnPlayerMoving;

    public ScPlayerBase Player { get; private set; }
    public string NickName => playerEntity?.nickName ?? "";
    public ScDefine.ScGuideCharacter GuideCharacterType => playerEntity?.guideCharacter ?? ScDefine.ScGuideCharacter.Character1;
    public ScPlayerSettingsEntity PlayerSettings => playerEntity?.settings;

    private ScPlayerEntity playerEntity;



    public void SetPlayer(ScPlayerBase player)
    {
        Player = player;
    }

    public void SetCurrentPlayerInfo(ScPlayerEntity playerInfo)
    {
        playerEntity = playerInfo;
    }

    public void SetCurrentPlayerMoveSpeed(float speed)
    {
        GameObject moveObj = GameObject.Find("Move");
        if (moveObj != null)
            moveObj.GetComponent<DynamicMoveProvider>().moveSpeed = speed;
        else
            Debug.Log("Move Object not found.");
    }

    public void RaisePlayerHeadTurnEvent(ScDefine.ScHeadTurn headTurn)
    {
        OnPlayerHeadTurn?.Invoke(headTurn);
    }

    public void RaisePlayerHandsUpEvent(bool isLeftHandUp, bool isRightHandUp, float distance)
    {
        OnPlayerHandsUp?.Invoke(isLeftHandUp, isRightHandUp, distance);
    }

    public void RaisePlayerMovingEvent(bool isMoving)
    {
        OnPlayerMoving?.Invoke(isMoving);
    }

    public void ApplyCurrentSetting()
    {
        try
        {
            if (playerEntity?.settings != null)
            {
                ApplyLoadedSetting(playerEntity.settings);
            }
            else
            {
                ScPlayerSettingsEntity entity = new();
                entity.renderScale = 1f;
                entity.brightness = 2f;

                entity.colorWeakMode = 0;
                entity.colorWeakCompensate = 100f;

                entity.rotateMode = 2;
                entity.rotationAngle = 3f;
                entity.rotationSpeed = 6f;

                entity.masterVolume = 1f;
                entity.bgmVolume = 1f;
                entity.voiceVolume = 1f;
                entity.soundEffectVolume = 1f;

                Manager.Instance.GameMgr.ApplyLoadedSetting(entity);
            }
        }
        catch (Exception ex)
        {
            //Debug.LogException(ex);
            Debug.Log(ex.Message);
        }
    }

    public void ApplyLoadedSetting(ScPlayerSettingsEntity playerSetting)
    {
        UniversalRenderPipelineAsset urp = GraphicsSettings.currentRenderPipeline as UniversalRenderPipelineAsset;
        if (urp != null)
        {
            urp.renderScale = playerSetting.renderScale;
        }

        Light[] sceneLights = FindObjectsByType<Light>(FindObjectsSortMode.None);
        if (sceneLights != null)
        {
            foreach (var light in sceneLights)
            {
                light.intensity = playerSetting.brightness + 1;
            }
        }

        Volume[] volumes = FindObjectsByType<Volume>(FindObjectsSortMode.None);
        if (volumes != null)
        {
            foreach (var volume in volumes)
            {
                if (!volume.isGlobal) continue;
                if (volume.sharedProfile.TryGet(out ChannelMixer channelMixer))
                {
                    float curValue = playerSetting.colorWeakCompensate;
                    bool red, green, blue;
                    switch (playerSetting.colorWeakMode)
                    {
                        case 1: //red
                            red = true;
                            green = false;
                            blue = false;
                            channelMixer.redOutBlueIn.value = -curValue;
                            channelMixer.redOutGreenIn.value = -curValue;
                            channelMixer.redOutRedIn.value = curValue + 100;
                            break;
                        case 2: //green
                            red = false;
                            green = true;
                            blue = false;
                            channelMixer.greenOutBlueIn.value = -curValue;
                            channelMixer.greenOutGreenIn.value = curValue + 100;
                            channelMixer.greenOutRedIn.value = -curValue;
                            break;
                        case 3: //blue
                            red = false;
                            green = false;
                            blue = true;
                            channelMixer.blueOutBlueIn.value = curValue + 100;
                            channelMixer.blueOutGreenIn.value = -curValue;
                            channelMixer.blueOutRedIn.value = -curValue;
                            break;
                        default:
                            red = false;
                            green = false;
                            blue = false;
                            break;
                    }
                    channelMixer.redOutBlueIn.overrideState = red;
                    channelMixer.redOutGreenIn.overrideState = red;
                    channelMixer.redOutRedIn.overrideState = red;

                    channelMixer.greenOutBlueIn.overrideState = green;
                    channelMixer.greenOutGreenIn.overrideState = green;
                    channelMixer.greenOutRedIn.overrideState = green;

                    channelMixer.blueOutBlueIn.overrideState = blue;
                    channelMixer.blueOutGreenIn.overrideState = blue;
                    channelMixer.blueOutRedIn.overrideState = blue;
                }
            }
        }
        XRInputModalityManager imm = FindAnyObjectByType<XRInputModalityManager>();
        ActionBasedControllerManager abcm = null;
        imm?.rightController.TryGetComponent(out abcm);
        SnapTurnProviderBase snap = FindAnyObjectByType<ActionBasedSnapTurnProvider>();
        ContinuousTurnProviderBase cont = FindAnyObjectByType<ActionBasedContinuousTurnProvider>();
        if (abcm != null && snap != null && cont != null)
        {
            switch (playerSetting.rotateMode)
            {
                case 0:
                    abcm.smoothTurnEnabled = false;
                    snap.turnAmount = 0;
                    break;
                case 1:
                    abcm.smoothTurnEnabled = false;
                    snap.turnAmount = playerSetting.rotationAngle * 15;
                    break;
                case 2:
                    abcm.smoothTurnEnabled = true;
                    cont.turnSpeed = playerSetting.rotationSpeed * 10;
                    break;
            }
        }

        Manager.Instance.SoundMgr.SetBgmVolume(playerSetting.bgmVolume * playerSetting.masterVolume);
        Manager.Instance.SoundMgr.SetVoiceVolume(playerSetting.voiceVolume * playerSetting.masterVolume);
        Manager.Instance.SoundMgr.SetSfxVolume(playerSetting.soundEffectVolume * playerSetting.masterVolume);
    }
}