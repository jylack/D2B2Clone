using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScLoadPreviousScene : MonoBehaviour
{
    [SerializeField] Button btn;
    private void Start()
    {
        btn.onClick.AddListener(() => Manager.Instance.SceneMgr.LoadPreviousScene());
    }
}
