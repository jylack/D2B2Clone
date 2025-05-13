using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScLookAroundProgress : MonoBehaviour
{
    [SerializeField] private Image progressImage;

    private void OnEnable()
    {
        //Manager.Instance.InputMgr.OnRightHandPositionChanged += DrawLookArounProgress;
    }
    private void OnDisable()
    {
        //Manager.Instance.GameMgr.OnPlayerHeadTurn -= DrawLookArounProgress;
    }
    private void DrawLookArounProgress(float headAngle,float maxHeaAngle)
    {
        progressImage.fillAmount = Mathf.Clamp01(headAngle / maxHeaAngle);
    }
}
