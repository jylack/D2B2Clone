using UnityEngine;

public class UITutPlay : MonoBehaviour
{
    public void GoToPlayerSettings()
    {
        Manager.Instance.SceneMgr.LoadScene("Tut_PlayerSettings");
    }
}