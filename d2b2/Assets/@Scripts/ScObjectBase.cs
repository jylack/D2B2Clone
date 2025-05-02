using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class ScObjectBase : MonoBehaviour
{
    public CancellationToken DestroyToken => this.GetCancellationTokenOnDestroy();
    
    protected virtual void Awake() { }
    protected virtual void Start() { }
}
