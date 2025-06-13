using System.Collections.Generic;
using Timeline.Samples;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class TextTrackMixerBehaviourHsy : PlayableBehaviour
{

    Color m_DefaultColor;
    float m_DefaultFontSize;
    string m_DefaultText;
    TMP_Text m_TrackBinding;
    string lastText;
    string currentText;

    private const string KeyId = "Id";
    private const string KeyText = "Text";
    private const string KeyUseTTS = "TTS";

    // Called every frame that the timeline is evaluated. ProcessFrame is invoked after its' inputs.
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        SetDefaults(playerData as TMP_Text);
        if (m_TrackBinding == null)
            return;

        int inputCount = playable.GetInputCount();

        Color blendedColor = Color.clear;
        float blendedFontSize = 0f;
        float totalWeight = 0f;
        float greatestWeight = 0f;
        string text = m_DefaultText;

        for (int i = 0; i < inputCount; i++)
        {
            float inputWeight = playable.GetInputWeight(i);
            ScriptPlayable<TextPlayableBehaviour> inputPlayable = (ScriptPlayable<TextPlayableBehaviour>)playable.GetInput(i);
            TextPlayableBehaviour input = inputPlayable.GetBehaviour();

            blendedColor += input.color * inputWeight;
            blendedFontSize += input.fontSize * inputWeight;
            totalWeight += inputWeight;

            if (inputWeight > greatestWeight)
            {
                currentText = input.key;
                greatestWeight = inputWeight;
            }
        }

        // 텍스트가 변경될 때만 갱신
        if (currentText != lastText)
        {
            m_TrackBinding.text = GetText(currentText);
            lastText = currentText;
        }

        m_TrackBinding.color = Color.Lerp(m_DefaultColor, blendedColor, totalWeight);
        m_TrackBinding.fontSize = Mathf.RoundToInt(Mathf.Lerp(m_DefaultFontSize, blendedFontSize, totalWeight));
        //m_TrackBinding.text = text;
    }

    public override void OnPlayableDestroy(Playable playable)
    {
        RestoreDefaults();
    }

    void SetDefaults(TMP_Text text)
    {
        if (text == m_TrackBinding)
            return;

        RestoreDefaults();

        m_TrackBinding = text;
        if (m_TrackBinding != null)
        {
            m_DefaultColor = m_TrackBinding.color;
            m_DefaultFontSize = m_TrackBinding.fontSize;
            m_DefaultText = m_TrackBinding.text;
        }
    }

    void RestoreDefaults()
    {
        if (m_TrackBinding == null)
            return;

        m_TrackBinding.color = m_DefaultColor;
        m_TrackBinding.fontSize = m_DefaultFontSize;
        m_TrackBinding.text = m_DefaultText;
    }

    private string GetText(string textKey)
    {
#if UNITY_EDITOR
        if (Application.isPlaying)
        {
            Manager.Instance.SoundMgr.PlayVoice(textKey);
            return Manager.Instance.LanguageMgr.GetText(textKey);
        }
        else
        {
            Dictionary<string, string> dialogueMap = new Dictionary<string, string>();
            string path = Application.dataPath + "/Resources/LocalizationTable.csv";
            string CsvText = System.IO.File.ReadAllText(path, System.Text.Encoding.UTF8);
            var textList = ScCsvLoader.LoadFromText(CsvText);
            foreach (var row in textList)
            {
                if (row.TryGetValue(KeyId, out string key)
                    && row.TryGetValue(KeyText, out var textValue))
                {
                    dialogueMap.Add(key, textValue);
                }
            }

            return dialogueMap[textKey];
        }
#else
    Manager.Instance.SoundMgr.PlayVoice(textKey);
    return Manager.Instance.LanguageMgr.GetText(textKey);
#endif

    }
}
