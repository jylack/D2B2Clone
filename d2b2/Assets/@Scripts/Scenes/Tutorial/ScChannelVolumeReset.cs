using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using static Pathfinding.Drawing.Palette.Colorbrewer;

public class ScChannelVolumeReset : MonoBehaviour
{
    [SerializeField] private Volume volume;
    [SerializeField] private bool resetWhenStart;
    private ChannelMixer channelMixer;

    private void Awake()
    {
        volume.sharedProfile.TryGet(out channelMixer);
        if(resetWhenStart)
        {
            ResetColorVolume();
        }
    }

    public void ResetColorVolume()//채널믹서 초기로 돌림
    {
        channelMixer.redOutBlueIn.value = 0;
        channelMixer.redOutGreenIn.value = 0;
        channelMixer.redOutRedIn.value = 100;
        channelMixer.redOutBlueIn.overrideState = false;
        channelMixer.redOutGreenIn.overrideState = false;
        channelMixer.redOutRedIn.overrideState = false;

        channelMixer.greenOutBlueIn.value = 0;
        channelMixer.greenOutGreenIn.value = 100;
        channelMixer.greenOutRedIn.value = 0;
        channelMixer.greenOutBlueIn.overrideState = false;
        channelMixer.greenOutGreenIn.overrideState = false;
        channelMixer.greenOutRedIn.overrideState = false;

        channelMixer.blueOutBlueIn.value = 100;
        channelMixer.blueOutGreenIn.value = 0;
        channelMixer.blueOutRedIn.value = 0;
        channelMixer.blueOutBlueIn.overrideState = false;
        channelMixer.blueOutGreenIn.overrideState = false;
        channelMixer.blueOutRedIn.overrideState = false;
    }
}
