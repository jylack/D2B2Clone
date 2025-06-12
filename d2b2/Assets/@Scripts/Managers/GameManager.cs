using System;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
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

    public void RaisePlayerHandsUpEvent(bool isLeftHandUp, bool isRightHandUp,float distance)
    {
        OnPlayerHandsUp?.Invoke(isLeftHandUp, isRightHandUp, distance);
    }

    public void RaisePlayerMovingEvent(bool isMoving)
    {
        OnPlayerMoving?.Invoke(isMoving);
    }

    public async void ApplyCurrentSetting()
    {
        try
        {
            if (NickName != null)
            {
                ScPlayerEntity player = await Manager.Instance.DbMgr.Load(Manager.Instance.GameMgr.NickName);
                if (player != null)
                {
                    ApplyLoadedSetting(player.settings);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }

    public void ApplyLoadedSetting(ScPlayerSettingsEntity playerSetting)
    { 
        UniversalRenderPipelineAsset urp = GraphicsSettings.currentRenderPipeline as UniversalRenderPipelineAsset;
        if (urp != null) {
            urp.renderScale = playerSetting.renderScale;
        }

        Light sceneLight = GameObject.Find("Directional Light").GetComponent<Light>();
        if(sceneLight != null)
        {
            sceneLight.intensity = playerSetting.brightness + 1;
        }

        Volume volume = GameObject.Find("Global Volume")?.GetComponent<Volume>();
        if(volume != null && volume.sharedProfile.TryGet<ChannelMixer>(out ChannelMixer channelMixer))
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

        ActionBasedControllerManager abcm = GameObject.Find("Camera Offset").transform.GetChild(5).GetComponent<ActionBasedControllerManager>();
        GameObject turn = GameObject.Find("Turn");
        SnapTurnProviderBase snap = turn.GetComponent<SnapTurnProviderBase>();
        ContinuousTurnProviderBase cont = turn.GetComponent<ContinuousTurnProviderBase>();
        if(abcm != null && turn != null)
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