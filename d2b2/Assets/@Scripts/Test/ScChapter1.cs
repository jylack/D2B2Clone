using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScChapter1 : MonoBehaviour
{
    private static ScChapter1 instance;

    public static ScChapter1 Instance => instance;

    public int CurrentSetp = 0;

    private void Awake()
    {
        if(instance == null)
            instance = this;
    }
}
