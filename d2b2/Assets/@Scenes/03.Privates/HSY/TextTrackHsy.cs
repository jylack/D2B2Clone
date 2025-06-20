using Timeline.Samples;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
[TrackBindingType(typeof(TMP_Text))]
[TrackColor(0.1394896f, 0.4411765f, 0.3413077f)]
[TrackClipType(typeof(TextPlayableAsset))]
public class TextTrackHsy : TrackAsset
{
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        return ScriptPlayable<TextTrackMixerBehaviourHsy>.Create(graph, inputCount);
    }
    public override void GatherProperties(PlayableDirector director, IPropertyCollector driver)
    {
        TMP_Text trackBinding = director.GetGenericBinding(this) as TMP_Text;
        if (trackBinding == null)
            return;

        driver.AddFromName<TMP_Text>(trackBinding.gameObject, "m_text");
        driver.AddFromName<TMP_Text>(trackBinding.gameObject, "m_fontSize");
        driver.AddFromName<TMP_Text>(trackBinding.gameObject, "m_fontColor");

        base.GatherProperties(director, driver);
    }
}
