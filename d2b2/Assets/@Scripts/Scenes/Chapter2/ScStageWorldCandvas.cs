using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScStageWorldCandvas : MonoBehaviour
{
    [SerializeField] private GameObject goalUI;
    [SerializeField] private float GoalUITime;

    private void Start()
    {
        GoalUITime = 5;
        StartCoroutine(goalArrowUI());
    }
    IEnumerator goalArrowUI()
    {
        goalUI.SetActive(true);
        yield return new WaitForSeconds(GoalUITime);
        goalUI.SetActive(false);
    }
}
