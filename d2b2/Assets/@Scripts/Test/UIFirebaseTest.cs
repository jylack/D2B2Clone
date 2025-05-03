using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;

public class UIFirebaseTest : MonoBehaviour
{
    public TMP_InputField inputField;
    public GameObject content;
    public GameObject itemPrefab;


    private void Start()
    {
        inputField.onValidateInput += OnValidateInput;
    }

    
    
    public void Save()
    {
        string nickName = inputField.text.Trim();
        
        if (string.IsNullOrEmpty(nickName))
        {
            print("nickname is empty.");
            return;
        }

        if (nickName.Length < 2)
        {
            print("nickname length is less than 2.");
            return;
        }
        
        
        ScPlayerEntity entity = new ScPlayerEntity
        {
            nickName = nickName,
            settings = new ScPlayerSettingsEntity()
        };

        Manager.Instance.DbMgr.Save(nickName, entity).Forget();
    }

    public async void CheckExist()
    {
        bool isExist = await Manager.Instance.DbMgr.CheckNickNameExist(inputField.text);
        print($"isExist: {isExist}");
    }
    
    public async void Load()
    {
        try
        {
            ScPlayerEntity entity = await Manager.Instance.DbMgr.Load(inputField.text);
            string json = JsonConvert.SerializeObject(entity);
            print(json);
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
        }
    }
    
    public async void LoadAll()
    {
        try
        {
            (..content.transform.childCount).ForEach(i =>
            {
                Destroy(content.transform.GetChild(i).gameObject);
            });
        
            List<ScPlayerEntity> entities = await Manager.Instance.DbMgr.LoadAll();
            foreach (ScPlayerEntity entity in entities)
            {
                var uiEntity = Instantiate(itemPrefab).GetComponent<UIPlayerTestEntity>();
                uiEntity.nickName = entity.nickName;
                uiEntity.text.text = JsonConvert.SerializeObject(entity);
                uiEntity.transform.SetParent(content.transform, false);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
        }
    }



    private char OnValidateInput(string text, int charIndex, char addedChar)
    {
        if (char.IsWhiteSpace(addedChar))
            return '\0';

        return addedChar;
    }
}