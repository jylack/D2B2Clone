using System;
using TMPro;
using UnityEngine;

public enum MissionBoxTextCheckType
{
    none,
    Title,
    Base,
    Clear,
}

public class ScMissionBoxTextCheck : MonoBehaviour
{

    private MissionBoxTextCheckType checkType;
    private TextMeshProUGUI tmp
    {
        get => GetComponent<TextMeshProUGUI>();
    }
    private bool isCheck;

    private void Awake()
    {
        //tmp = GetComponent<TextMeshProUGUI>();
        isCheck = false;
    }

    public void SetMissionText(string key, MissionBoxTextCheckType checkType)
    {
        if (tmp != null)
        {
            try
            {
                tmp.text = Manager.Instance.LanguageMgr.GetText(key);
                SetTypeChange(checkType);
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        }
        else
        {
            Debug.LogError("TextMeshProUGUI component is not assigned.");
        }

    }
    public string GetMissionText()
    {
        if (tmp != null)
        {
            return tmp.text;
        }
        else
        {
            Debug.LogError("TextMeshProUGUI component is not assigned.");
            return string.Empty;
        }
    }

    public bool GetMissionState()
    {
        return isCheck;
    }


    public void SetCheck(bool check)
    {
        if (checkType == MissionBoxTextCheckType.Title)
        {
            Debug.LogError("Cannot change check state for Title type.");
            return;
        }

        isCheck = check;

        if (tmp != null)
        {
            tmp.fontStyle = check ? FontStyles.Strikethrough : FontStyles.Normal;
            checkType = check ? MissionBoxTextCheckType.Clear : MissionBoxTextCheckType.Base;
            SetTypeChange(checkType);
        }
        else
        {
            Debug.LogError("TextMeshProUGUI component is not assigned.");
        }
    }

    public void SetTypeChange(MissionBoxTextCheckType type)
    {
        if (tmp != null)
        {
            switch (type)
            {
                case MissionBoxTextCheckType.none:
                    tmp.color = Color.black;
                    checkType = MissionBoxTextCheckType.none;
                    break;
                case MissionBoxTextCheckType.Base:
                    tmp.color = Color.white;
                    checkType = MissionBoxTextCheckType.Base;
                    break;
                case MissionBoxTextCheckType.Clear:
                    checkType = MissionBoxTextCheckType.Clear;
                    tmp.color = Color.gray;
                    break;
                case MissionBoxTextCheckType.Title:
                    tmp.color = Color.white;
                    checkType = MissionBoxTextCheckType.Title;
                    tmp.fontStyle = FontStyles.Bold;
                    break;
            }
        }
        else
        {
            Debug.LogError("TextMeshProUGUI component is not assigned.");
        }
    }
}
