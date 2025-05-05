using UnityEngine;

public class UITutPlayerSettings : MonoBehaviour
{
    public void GoToCharacterSelection()
    {
        Manager.Instance.SceneMgr.LoadScene("Tut_CharacterSelection");
    }
}