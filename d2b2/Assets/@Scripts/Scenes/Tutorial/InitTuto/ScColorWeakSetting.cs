using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using static Pathfinding.Drawing.Palette.Colorbrewer;
using static UnityEngine.Rendering.DebugUI;

public enum VolumeColors
{
    None = 0,
    Red,
    Green,
    Blue
}

public class ScColorWeakSetting : MonoBehaviour
{
    private Volume volume;
    private ChannelMixer channelMixer;
    private VolumeColors colors;
    private float curValue = 0;

    private void Awake()
    {
        volume = GameObject.Find("Global Volume").GetComponent<Volume>();
        //그냥 volume.profile.TryGet을 하면 복사본을 만들어서 그 복사본을 수정, sharedProfile을 써야 원본에 수정가능
        volume.sharedProfile.TryGet(out channelMixer);
        colors = VolumeColors.None;
    }

    public void SetColorChannel(int color)
    {
        colors = (VolumeColors)color;
        bool red, green, blue;
        switch (colors)
        {
            case VolumeColors.Red:
                red = true;
                green = false;
                blue = false;
                channelMixer.redOutBlueIn.value = -curValue;
                channelMixer.redOutGreenIn.value = -curValue;
                channelMixer.redOutRedIn.value = curValue + 100;
                break;
            case VolumeColors.Green:
                red = false;
                green = true;
                blue = false;
                channelMixer.greenOutBlueIn.value = -curValue;
                channelMixer.greenOutGreenIn.value = curValue + 100;
                channelMixer.greenOutRedIn.value = -curValue;
                break;
            case VolumeColors.Blue:
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

    public void SetColorStrength(float value)
    {
        curValue = value;
        switch (colors)
        {
            case VolumeColors.Red:
                channelMixer.redOutBlueIn.value = -value;
                channelMixer.redOutGreenIn.value = -value;
                channelMixer.redOutRedIn.value = value + 100;
                break;
            case VolumeColors.Green:
                channelMixer.greenOutBlueIn.value = -value;
                channelMixer.greenOutGreenIn.value = value + 100;
                channelMixer.greenOutRedIn.value = -value;
                break;
            case VolumeColors.Blue:
                channelMixer.blueOutBlueIn.value = value + 100;
                channelMixer.blueOutGreenIn.value = -value;
                channelMixer.blueOutRedIn.value = -value;
                break;
        }
    }
}
