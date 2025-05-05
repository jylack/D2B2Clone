using UnityEngine;

public class UITutCharacterSelection : MonoBehaviour
{
    public void GoToPlay()
    {
        Manager.Instance.SceneMgr.LoadScene("Tut_Play");
    }
}