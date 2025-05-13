using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum UIType { LookAroundUI, HandUpUi}

public class UIPlayerHsy : MonoBehaviour
{
    [SerializeField] private GameObject lookAroundLeftProgress;
    [SerializeField] private GameObject lookAroundRightProgress;
    [SerializeField] private GameObject handUpProgressUI;


    



    //private Dictionary<UIType, GameObject> uiDict;
    //private void Awake()
    //{
    //    uiDict = new Dictionary<UIType, GameObject>();
    //
    //    GameObject look = Instantiate(lookAroundProgressUI);
    //    look.SetActive(false);
    //    uiDict.Add(UIType.LookAroundUI, look);
    //
    //
    //    GameObject hand = Instantiate(handUpProgressUI);
    //    hand.SetActive(false);
    //    uiDict.Add(UIType.HandUpUi, hand);
    //}
    //public void ShowUI(UIType type)
    //{
    //    uiDict[type].SetActive(true);
    //}
    //public void HideUI(UIType type)
    //{
    //    uiDict[type].SetActive(false);
    //}


}
