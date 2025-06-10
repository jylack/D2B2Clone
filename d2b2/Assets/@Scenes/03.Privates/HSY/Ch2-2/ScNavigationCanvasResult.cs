using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScNavigationCanvasResult : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI unFindChildCountText;
    [SerializeField] private TextMeshProUGUI findChildCountText;
    [SerializeField] private ScMirrorPlayerHsy player;
    private void OnEnable()
    {
        unFindChildCountText.text = (ScBlindRegionMiniGameScene.Instance.childMaxCount - player.findChildCount).ToString();
        findChildCountText.text = player.findChildCount.ToString();
    }
}
