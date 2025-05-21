using Cysharp.Threading.Tasks;
using UnityEngine;


//public static class Ch1_Step
//{
//    public static int CurrentSetp = 0;
//}

public class ScChapter1 : MonoBehaviour
{
    private static ScChapter1 instance;
    [SerializeField] private GameObject[] arrowImg;
    //[SerializeField] private float ImgViewTime = 2f;
    [SerializeField] private int ImgViewTime = 2000;
    //[SerializeField] private ScTrafficCtrl[] traffic;
    [SerializeField] private ScChapter1_NpcMoveController npc;

    public static ScChapter1 Instance => instance;
    public static int CurrentSetp = 0;
    //public int CurrentSetp { get; private set; } = 0;
    //public ScNpcCtrl Npc => npc;

    public bool lookAroundMissionClear = false;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private async void Start()
    {
        await ArrowImageView();
    }

 
    public void NpcCheck()
    {
        if (npc.gameObject.activeSelf == true) return;

        if (npc.gameObject.activeSelf == false)
        {
            npc.gameObject.SetActive(true);
        }
    }

    public async UniTask NextStep()
    {
        CurrentSetp++;

        if (CurrentSetp < arrowImg.Length)
        {
            await ArrowImageView();
        }

    }

    private async UniTask ArrowImageView()
    {
        arrowImg[CurrentSetp].SetActive(true);
        

        await UniTask.Delay(ImgViewTime);

        arrowImg[CurrentSetp].SetActive(false);
        
    }

}
