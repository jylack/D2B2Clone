using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class ScObjectBase : MonoBehaviour
{
    protected CancellationToken DestroyToken => this.GetCancellationTokenOnDestroy();
}
