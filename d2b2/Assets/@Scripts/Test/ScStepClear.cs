using Cysharp.Threading.Tasks.Triggers;
using UnityEngine;

public class ScStepClear : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.layer);
        Debug.Log(ScDefine.Layer.PlayerIndex);

        if (other.gameObject.layer == ScDefine.Layer.PlayerIndex)
        {
            Manager.Instance.GameMgr.CurrentStep++;
            other.gameObject.GetComponent<ScRespawn>().NextPos(Manager.Instance.GameMgr.CurrentStep);
            Debug.Log("¥Ÿ¿ΩΩ∫≈‹¿∏∑Œ ≥—æÓ∞¨¿Ω.");

        }
    }

}
