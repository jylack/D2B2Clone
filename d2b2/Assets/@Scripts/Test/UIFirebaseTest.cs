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



    public void Save()
    {
        string nickName = inputField.text;
        
        ScPlayerEntity entity = new ScPlayerEntity
        {
            nickName = nickName,
            settings = new ScPlayerSettingsEntity()
        };

        Manager.Instance.DbMgr.Save(nickName, entity).Forget();
    }

    public async void LoadAll()
    {
        (..content.transform.childCount).ForEach(i =>
        {
            Destroy(content.transform.GetChild(i).gameObject);
        });
        
        List<ScPlayerEntity> entities = await Manager.Instance.DbMgr.LoadAll();
        foreach (ScPlayerEntity entity in entities)
        {
            var uiEntity = Instantiate(itemPrefab).GetComponent<UIPlayerTestEntity>();
            uiEntity.text.text = JsonConvert.SerializeObject(entity);
            uiEntity.transform.SetParent(content.transform, false);
        }
    }
}