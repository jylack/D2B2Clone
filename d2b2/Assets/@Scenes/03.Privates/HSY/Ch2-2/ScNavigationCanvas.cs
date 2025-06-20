using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScNavigationCanvas : MonoBehaviour
{
    [Header("playingNav")]
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI findChildCountText;
    [SerializeField] private ScMirrorPlayerHsy playerScript;
    private void OnEnable()
    {
        playerScript.findChildChildEvent += OnChangeFindChildCountText;
        ScBlindRegionMiniGameScene.Instance.timer += OnChangeTimerText;

    }
    private void OnDisable()
    {
        playerScript.findChildChildEvent -= OnChangeFindChildCountText;
        ScBlindRegionMiniGameScene.Instance.timer -= OnChangeTimerText;
    }

    public void OnChangeTimerText(float currentTime,float maxTime)
    {
        timerText.text = ((int)currentTime).ToString() + $" / {maxTime}";
    }
    public void OnChangeFindChildCountText(int findChildCount)
    {
        findChildCountText.text = findChildCount.ToString();
    }
}
