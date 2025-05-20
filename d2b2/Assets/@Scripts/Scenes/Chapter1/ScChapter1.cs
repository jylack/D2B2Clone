using System.Collections;
using UnityEngine;

public class ScChapter1 : MonoBehaviour
{
    private static ScChapter1 instance;
    [SerializeField] private GameObject[] arrowImg;
    [SerializeField] private float ImgViewTime = 2f;
    //[SerializeField] private ScTrafficCtrl[] traffic;
    [SerializeField] private ScChapter1_NpcMoveController npc;

    public static ScChapter1 Instance => instance;

    public int CurrentSetp { get; private set; }
    //public ScNpcCtrl Npc => npc;

    public bool lookAroundMissionClear = false;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        CurrentSetp = 0;
        StartCoroutine(ImgStart());
    }

    public void NpcCheck()
    {
        if (npc.gameObject.activeSelf == true) return;

        if (npc.gameObject.activeSelf == false)
        {
            //if (traffic[0].CurrentColor == TrafficLightColor.Green)
            //{
                npc.gameObject.SetActive(true);
            //}
        }
    }

    public void NextStep()
    {
        CurrentSetp++;

        if (CurrentSetp < arrowImg.Length)
        {
            StartCoroutine(ImgStart());
        }
    }

    private IEnumerator ImgStart()
    {
        arrowImg[CurrentSetp].SetActive(true);
        Manager.Instance.GameMgr.canMove = false;

        yield return new WaitForSeconds(ImgViewTime);

        arrowImg[CurrentSetp].SetActive(false);
        Manager.Instance.GameMgr.canMove = true;

    }


}
