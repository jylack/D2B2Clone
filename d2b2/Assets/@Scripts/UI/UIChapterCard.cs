using UnityEngine;
using UnityEngine.UI;

public class UIChapterCard : UIBase
{
    [SerializeField] private Image imgCheck;
    [SerializeField] private ScDefine.ScScene nextScene;



    public void Select()
    {
        imgCheck.gameObject.SetActive(true);
        Manager.Instance.SceneMgr.LoadScene(nextScene);
    }
}
