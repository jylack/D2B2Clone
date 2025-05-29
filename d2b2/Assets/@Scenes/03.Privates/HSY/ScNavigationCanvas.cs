using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScNavigationCanvas : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI findChildCountText;
    [SerializeField] ScMirrorPlayerHsy playerScript;
    [SerializeField] ScBlindRegionMiniGameScene miniGameManagerScript;

    private void OnEnable()
    {
        playerScript.findChildChildEvent += OnChangeFindChildCountText;
        miniGameManagerScript.timer += OnChangeTimerText;

    }
    private void OnDisable()
    {
        playerScript.findChildChildEvent -= OnChangeFindChildCountText;
        miniGameManagerScript.timer -= OnChangeTimerText;
    }

    public void OnChangeTimerText(float currentTime)
    {
        timerText.text = currentTime.ToString();
    }
    public void OnChangeFindChildCountText(int findChildCount)
    {
        findChildCountText.text = findChildCount.ToString();
    }
}
