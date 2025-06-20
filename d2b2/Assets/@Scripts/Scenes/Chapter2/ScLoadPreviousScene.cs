using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class ScLoadPreviousScene : MonoBehaviour
{
 
    public void LoadPreviousScene()
    {
        Manager.Instance.SceneMgr.LoadPreviousScene();
    }

}
 