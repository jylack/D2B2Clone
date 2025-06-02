using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEditor.Rendering;

public class ScHandUpGuide : ScSceneBase
{
    [SerializeField] GameObject guideNpc;
    [SerializeField] GameObject guideNpcContextUI;
    [SerializeField] GameObject problemChildNpc;
    [SerializeField] GameObject truck;
    [SerializeField] List<string> npcTextList;
    [SerializeField] TextMeshProUGUI guideNpcContextTxt;
    public static ScHandUpGuide tetst;
    protected override void Awake()
    {
        tetst = this;  
        base.Awake();
    }
}
