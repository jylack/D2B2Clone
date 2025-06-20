using UnityEngine;
using UnityEngine.Events;

public class ScSettingUI : MonoBehaviour
{
    [SerializeField] GameObject settingMenuObj;
    [SerializeField] GameObject displaySettingObj;
    [SerializeField] GameObject colorBlindSettingObj;
    [SerializeField] GameObject controlSettingObj;
    [SerializeField] GameObject soundSettingObj;

    public UnityEvent onClosed;

    public void SetSettingMenu(bool active)
    {
        gameObject.SetActive(active);
        settingMenuObj.SetActive(active);
    }

    public void SetDisplaySetting(bool active)
    {
        displaySettingObj.SetActive(active);
        settingMenuObj.SetActive(!active);

    }

    public void SetColorBlindSetting()
    {
        colorBlindSettingObj.SetActive(!colorBlindSettingObj.activeSelf);
    }

    public void SetControlSetting(bool active)
    {
        controlSettingObj.SetActive(active);
        settingMenuObj.SetActive(!active);
    }

    public void SetSoundSetting(bool active)
    {
        soundSettingObj.SetActive(active);
        settingMenuObj.SetActive(!active);
    }

    public void ReturnToMenu()
    {
        Manager.Instance.SceneMgr.LoadScene(ScDefine.ScScene.Admin);
    }

    public void ExitOption()
    {
        settingMenuObj.SetActive(false);
        onClosed?.Invoke();
    }
}
