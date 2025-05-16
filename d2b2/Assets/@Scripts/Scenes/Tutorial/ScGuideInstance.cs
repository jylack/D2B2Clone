using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScGuideInstance : MonoBehaviour
{
    [SerializeField] GameObject[] guides;
    public void InstantiateGuide(ScDefine.ScGuideCharacter guide)
    {
        Instantiate(guides[(int)guide - 1], transform);
    }
}
