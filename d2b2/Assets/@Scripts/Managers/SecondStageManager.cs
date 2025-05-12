using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondStageManager : MonoBehaviour
{
    public static SecondStageManager instance;
    //private bool isEnterBlindSpot = false;
    //private bool isClearMissionHandUp = false;
    //private bool isClearMissionLookAround = false;

    private void Awake()
    {
        if (instance== null)
        {
            instance = this;
        }
    }

}
