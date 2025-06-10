using UnityEngine;

public abstract class ScPlayerBase : ScObjectBase
{
    public abstract Camera MainCamera { get; }

    public abstract void PlaySound(AudioClip audioClip);
}
