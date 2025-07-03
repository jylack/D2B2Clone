using UnityEngine;
using UnityEngine.Events;

public class ScStepClear : MonoBehaviour
{
    [SerializeField] private UnityEvent OnStepClear;


    private async void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == ScDefine.Layer.PlayerIndex)
        {
            OnStepClear?.Invoke();

            await ScChapter1.Instance.NextStep();

            ScRespawn.Instance.Init(ScChapter1.CurrentSetp);
            Debug.Log("¥Ÿ¿ΩΩ∫≈‹¿∏∑Œ ≥—æÓ∞¨¿Ω." + ScChapter1.CurrentSetp);
            gameObject.SetActive(false);
        }
    }



    
}
