using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip success;
    [SerializeField] private AudioClip fail;

    private ScPlayerBase Player => Manager.Instance.GameMgr.Player;
    
    
    
    public void PlaySuccess()
    {
        PlaySound(success);
    }



    private void PlaySound(AudioClip audioClip)
    {
        Player?.PlaySound(audioClip);
    }
}