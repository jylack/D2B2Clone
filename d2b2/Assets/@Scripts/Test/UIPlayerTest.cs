using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;

public class UIPlayerTest : MonoBehaviour
{
    [SerializeField] private TMP_Text lookingText;
    [SerializeField] private TMP_Text leftUpText;
    [SerializeField] private TMP_Text movingText;
    [SerializeField] private TMP_Text rightUpText;

    

    private void Awake()
    {
        Manager.Instance.GameMgr.OnPlayerHeadTurn += OnPlayerOnPlayerHeadTurn;
        Manager.Instance.GameMgr.OnPlayerHandsUp += OnPlayerHandsUp;
        Manager.Instance.GameMgr.OnPlayerMoving += OnPlayerMoving;
        
        LocalizationSettings.SelectedLocaleChanged += LocalizationSettingsOnSelectedLocaleChanged;
    }

    private async void LocalizationSettingsOnSelectedLocaleChanged(Locale obj)
    {
        try
        {
            string txt = await Manager.Instance.LanguageMgr.GetTextAsync("Sg1_1");
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }

    private void OnDestroy()
    {
        Manager.Instance.GameMgr.OnPlayerHeadTurn -= OnPlayerOnPlayerHeadTurn;
        Manager.Instance.GameMgr.OnPlayerHandsUp -= OnPlayerHandsUp;
        Manager.Instance.GameMgr.OnPlayerMoving -= OnPlayerMoving;
    }



    public async void OnChangeLocaleClicked()
    {
        //await LocalizationSettings.InitializationOperation.Task;

        //List<Locale> locales = LocalizationSettings.AvailableLocales.Locales;
        //int index = locales.IndexOf(LocalizationSettings.SelectedLocale);
        //int newIndex = (index + 1) % locales.Count;
        //Locale locale = locales[newIndex];
        //Debug.Log(locale);

        //LocalizationSettings.SelectedLocale = locale;

        //string txt = await GetTextAsync("Sg1_1");
        string txt = await Manager.Instance.LanguageMgr.GetTextAsync("Sg1_1");
        Debug.Log(txt);
    }



    private void OnPlayerOnPlayerHeadTurn(ScDefine.ScHeadTurn headTurn)
    {
        lookingText.text = headTurn.ToString();
    }
    
    private void OnPlayerHandsUp(bool leftHandUp, bool rightHandUp,float distance)
    {
        leftUpText.color = leftHandUp ? Color.blue : Color.gray;
        rightUpText.color = rightHandUp ? Color.blue : Color.gray;
    }
    
    private void OnPlayerMoving(bool isMoving)
    {
        movingText.color = isMoving ? Color.blue : Color.gray;
    }

    public async UniTask<string> GetTextAsync(string key)
    {
        try
        {
            await LocalizationSettings.InitializationOperation.Task;

            Locale locale = LocalizationSettings.SelectedLocale;
            StringTable table = await LocalizationSettings.StringDatabase.GetTableAsync("LocalizationTable", locale);

            if (table != null)
            {
                StringTableEntry entry = table.GetEntry(key);
                if (entry != null)
                    return entry.LocalizedValue;
            }
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }

        return string.Empty;
    }
}