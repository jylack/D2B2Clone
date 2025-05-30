using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ScCharaterActiveEffect : MonoBehaviour
{
    [SerializeField] private float appearDuration = 0.5f;
    [SerializeField] private float bounceScale = 1.1f;
    [SerializeField] private float bounceDuration = 0.2f;

    private void OnEnable()
    {
        transform.localScale = Vector3.zero;

        transform.DOScale(Vector3.one, appearDuration)
            .SetEase(Ease.OutBack)
            .OnComplete(OnAppearComplete);
    }

    private void OnAppearComplete()
    {
        transform.DOScale(Vector3.one * bounceScale, bounceDuration)
            .SetEase(Ease.OutQuad)
            .OnComplete(OnBounceOutComplete);
    }

    private void OnBounceOutComplete()
    {
        transform.DOScale(Vector3.one, bounceDuration / 2f)
            .SetEase(Ease.InQuad);
    }
}
