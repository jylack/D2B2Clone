using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondStageManager : MonoBehaviour
{
    public static SecondStageManager instance;

    private void Awake()
    {
        if (instance== null)
        {
            instance = this;
        }
    }

    public void ResetSecondStage()
    {

    }

}
