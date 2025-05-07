using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class ScObjectBase : MonoBehaviour
{
    public CancellationToken DestroyToken => this.GetCancellationTokenOnDestroy();
}
