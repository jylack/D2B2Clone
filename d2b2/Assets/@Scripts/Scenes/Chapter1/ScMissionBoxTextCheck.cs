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
    [SerializeField] private MissionBoxTextCheckType checkType;
    private TextMeshProUGUI tmp;
    private bool isCheck;

    void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        isCheck = false;
    }

    public void SetMissionText(string text,MissionBoxTextCheckType checkType)
    {
        if (tmp != null)
        {
            tmp.text = text;
            SetTypeChange(checkType);
        }
        else
        {
            Debug.LogError("TextMeshProUGUI component is not assigned.");
        }

    }


    public void SetCheck(bool check)
    {
        if(checkType == MissionBoxTextCheckType.Title)
        {
            Debug.LogError("Cannot change check state for Title type.");
            return;
        }

        isCheck = check;
        if (tmp != null)
        {
            tmp.fontStyle = check ? FontStyles.Strikethrough : FontStyles.Normal;
            checkType =  check ? MissionBoxTextCheckType.Clear : MissionBoxTextCheckType.Base; 
            SetTypeChange(checkType);
        }
        else
        {
            Debug.LogError("TextMeshProUGUI component is not assigned.");
        }
    }

    private void SetTypeChange(MissionBoxTextCheckType type)
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
                    tmp.color = Color.red;
                    checkType = MissionBoxTextCheckType.Base; 
                    break;
                case MissionBoxTextCheckType.Clear:
                    checkType = MissionBoxTextCheckType.Clear;
                    tmp.color = Color.gray; 
                    break;
                case MissionBoxTextCheckType.Title:
                    tmp.color = Color.white;
                    checkType = MissionBoxTextCheckType.Title;
                    break;
            }
        }
        else
        {
            Debug.LogError("TextMeshProUGUI component is not assigned.");
        }
    }
}
