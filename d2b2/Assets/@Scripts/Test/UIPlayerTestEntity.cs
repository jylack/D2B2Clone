using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public class UIPlayerTestEntity : MonoBehaviour
{
    public string nickName;
    public TMP_Text text;
    
    

    public void Remove()
    {
        print($"{nickName} clicked!");
        Manager.Instance.DbMgr.Remove(nickName).Forget();
        Destroy(gameObject);
    }
}