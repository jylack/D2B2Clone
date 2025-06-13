using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScSoundPlayer : MonoBehaviour
{
    [SerializeField] private ScDefine.ScSound sound;
    public void PlaySound()
    {
        Manager.Instance.SoundMgr.PlaySfx(sound);
    }
}
