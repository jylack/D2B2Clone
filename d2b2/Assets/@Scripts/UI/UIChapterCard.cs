using UnityEngine;
using UnityEngine.UI;

public class UIChapterCard : UIBase
{
    [SerializeField] private Image imgCheck;
    [SerializeField] private ScDefine.ScScene nextScene;

    public bool IsLoaded => ScAdminScene.Instance?.IsLoaded ?? false;



    public void OnClicked()
    {
        if (!IsLoaded)
            return;

        imgCheck.gameObject.SetActive(true);
        Manager.Instance.SceneMgr.LoadScene(nextScene);
    }
}
