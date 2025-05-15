using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTutorialManager : MonoBehaviour
{
    [SerializeField] GameObject guideObject;
    [SerializeField] GameObject[] guides;
    [SerializeField] ScNotifyFairy notifyFairy;
    [SerializeField] GameObject walkObjs;
    [SerializeField] GameObject lookAroundObjs;
    [SerializeField] ScLookAroundRegion lookAround;
    void Start()
    {
        Instantiate(guides[(int)TutorialManager.Instance.playerEntity.guideCharacter - 1], guideObject.transform);
    }

    public void WalkSuccess()
    {
        walkObjs.SetActive(false);
        lookAroundObjs.SetActive(true);
    }
 }
