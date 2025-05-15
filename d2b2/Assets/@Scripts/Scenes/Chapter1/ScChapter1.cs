using System.Collections;
using UnityEngine;

public class ScChapter1 : MonoBehaviour
{
    private static ScChapter1 instance;
    [SerializeField] private GameObject[] img;
    [SerializeField] private float ImgViewTime = 2f;

    public static ScChapter1 Instance => instance;

    public int CurrentSetp = 0;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    private void Start()
    {
        StartCoroutine(ImgStart());
    }

    public void NextStep()
    {
        CurrentSetp++;

        if (CurrentSetp < img.Length)
        {
            StartCoroutine(ImgStart());
        }
    }

    private IEnumerator ImgStart()
    {
        img[CurrentSetp].SetActive(true);
        Manager.Instance.GameMgr.canMove = false;

        yield return new WaitForSeconds(ImgViewTime);

        img[CurrentSetp].SetActive(false);
        Manager.Instance.GameMgr.canMove = true;

    }
}
