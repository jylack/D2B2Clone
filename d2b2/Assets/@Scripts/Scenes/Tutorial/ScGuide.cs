using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScGuide : MonoBehaviour
{
    [SerializeField] GameObject[] guides;
    public ScCharacter character;
    public void InstantiateGuide(ScDefine.ScGuideCharacter guide)
    {
        character = Instantiate(guides[(int)guide - 1], transform).GetComponent<ScCharacter>();
    }
}
