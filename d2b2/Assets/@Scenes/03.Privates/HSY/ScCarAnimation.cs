using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ScCarAnimation : MonoBehaviour
{
    float amplitude = 0.02f;
    float duration = 0.5f;
    void Start()
    {
        transform.DOMoveY(transform.position.y + amplitude, duration)
                 .SetLoops(-1, LoopType.Yoyo)
                 .SetEase(Ease.InOutSine);
    }
}
