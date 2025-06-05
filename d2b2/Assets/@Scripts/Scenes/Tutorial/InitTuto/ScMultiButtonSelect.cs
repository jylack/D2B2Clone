using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScMultiButtonSelect : MonoBehaviour
{
    [SerializeField] Button[] buttons;
    [SerializeField] Sprite selectedSprite;
    [SerializeField] Sprite notSelectedSprite;
    public int Selected { get; private set; }

    public void SelectButton(int index)
    {
        Selected = index;
        for(int i = 0; i< buttons.Length; i++)
        {
            if(i == index)
            {
                buttons[i].image.sprite = selectedSprite;
            }
            else
            {
                buttons[i].image.sprite = notSelectedSprite;
            }
        }
    }

    public void SettingApply(int index)
    {
        buttons[index].onClick?.Invoke();
    }
}
