using Cysharp.Threading.Tasks;
using UnityEngine;

//챕터1 정보관리 클래스
public class ScChapter1 : MonoBehaviour
{
    private static ScChapter1 instance;
    [SerializeField] private GameObject[] arrowImg;
    [SerializeField] private int ImgViewTime = 2000;
    [SerializeField] private ScChapter1_NpcMoveController npc;

    public static ScChapter1 Instance => instance;
    public static int CurrentSetp = 0; //챕터1 현재 스텝 정보

    private bool lookAroundMissionClear = false;
    public bool LookAroundMissionClear
    {
        get => lookAroundMissionClear;
        set
        {
            lookAroundMissionClear = value;
        }
    }

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

    //다음 스폰포인트 정보 갱신 및 화살표 이미지 활성화
    public async UniTask NextStep()
    {
        CurrentSetp++;

        if (CurrentSetp < arrowImg.Length)
        {
            await ArrowImageView();
        }

    }

    //목표 화살표 이미지 활성화
    private async UniTask ArrowImageView()
    {
        arrowImg[CurrentSetp].SetActive(true);

        await UniTask.Delay(ImgViewTime);

        arrowImg[CurrentSetp].SetActive(false);
    }


}
