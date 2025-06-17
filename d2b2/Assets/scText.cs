using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class scText : MonoBehaviour
{
    [SerializeField] private ScMissionList list;
    [SerializeField] private TextMeshProUGUI text; 
    bool isActive = false;

    private void Start()
    {
        text.text = isActive.ToString();
    }

    private void Update()
    {
        if(list.GetMissionTextList().Count > 0)
        {
            isActive = true;            
        }
        else
        {
            isActive = false;
        } 

        text.text = isActive.ToString();
    }

}
