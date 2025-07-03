using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScCh2DiamondAnimation : MonoBehaviour
{
    private void Start()
    {
        transform.DORotate(new Vector3(90, 360f, 0), 2f, RotateMode.FastBeyond360)
    .SetLoops(-1)
    .SetEase(Ease.Linear);
    }
}
