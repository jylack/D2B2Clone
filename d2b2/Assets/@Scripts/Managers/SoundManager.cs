using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip success;
    [SerializeField] private AudioClip fail;

    private ScPlayer Player => Manager.Instance.GameMgr.Player;
    
    
    
    public void PlaySuccess()
    {
        PlaySound(success);
    }



    private void PlaySound(AudioClip audioClip)
    {
        Manager.Instance.GameMgr.Player?.PlaySound(audioClip);
    }
}