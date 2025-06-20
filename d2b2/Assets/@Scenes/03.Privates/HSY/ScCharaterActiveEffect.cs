using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ScCharaterActiveEffect : MonoBehaviour
{
    private Vector3 targetScale;
    private Tween scaleTween;

    private void OnEnable()
    {
        targetScale = transform.localScale;      // 원래 크기 저장
        transform.localScale = Vector3.zero;     // 0에서 시작
        scaleTween?.Kill();                      // 기존 Tween 제거

        scaleTween = transform.DOScale(targetScale, 0.5f)
            .SetEase(Ease.OutBack);
    }
    public void SkidSoundPlay()
    {
        Manager.Instance.SoundMgr.PlaySfx(ScDefine.ScSound.CarSkid);
    }
}
