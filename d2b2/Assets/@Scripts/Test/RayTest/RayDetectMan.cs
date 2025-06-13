using TMPro;
using UnityEngine;
using DG.Tweening;

public class RayDetectMan : MonoBehaviour
{

    private Outline outline;
    private int count;
    private Vector3 minSize;
    [SerializeField] private ParticleSystem effect;



    private void Awake()
    {
        effect = Instantiate(effect,transform.position,transform.rotation);
        minSize = new Vector3(0.1f, 0.1f, 0.1f);
        outline = GetComponent<Outline>();
        outline.enabled = false;
    }



    public void SetOutlineVisible(bool isVisible)
    {
        if (outline != null)
        {
            Manager.Instance.SoundMgr.PlaySfx(ScDefine.ScSound.Ch2_2_CountDown);
            outline.enabled = isVisible;
        }
    }

    public void DoSomething()
    {
        transform.DOScale(minSize,0.3f).OnComplete(() => gameObject.SetActive(false));
        Manager.Instance.SoundMgr.PlaySfx(ScDefine.ScSound.CatchSomething);
        effect.Play();
    }
    private void OnDestroy()
    {
        Destroy(effect.gameObject);
    }
}
