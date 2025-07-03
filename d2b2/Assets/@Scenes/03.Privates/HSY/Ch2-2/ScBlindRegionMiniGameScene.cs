using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class ScBlindRegionMiniGameScene : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private Transform playerPos;
    [SerializeField] private Transform cameraOffset;
    [SerializeField] private Transform cameraPos;
    [SerializeField] private ScMirrorPlayerHsy playerScript;
    [Header("Npc")]
    [SerializeField] private ParticleSystem npcCreateParticle;
    [SerializeField] private Vector3 npcSpawnPos;
    [SerializeField] private GameObject gameStartBtn;
    [Header("Child")]
    [SerializeField] private GameObject[] childPrefab;
    [SerializeField] private Transform[] childPosArray;
    [SerializeField] private GameObject[] childArray;
    [SerializeField] public int childMaxCount => childPosArray.Length;
    [Header("TimeLine")]
    [SerializeField] private PlayableAsset miniGameStartTimeLine;
    [SerializeField] private PlayableAsset miniGameEndTimeLine;
    [SerializeField] private PlayableDirector director;
    [Header("Truck")]
    [SerializeField] private ScTruckShader truckShader;
    [Header("navCanvas")]
    [SerializeField] private GameObject playNavInfo;
    [SerializeField] private GameObject playResultNavInfo;
    [Header("PlaySetting")]
    [SerializeField] private float maxTime;
    public event Action<float,float> timer;

    static public ScBlindRegionMiniGameScene Instance { get; private set; }

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        Manager.Instance.GameMgr.SetCurrentPlayerMoveSpeed(0);
        playerScript.isGamePlaying= false;
        playerPos.transform.position = new Vector3 (-1.39f, 1.96f, 0.054f);
        cameraOffset.transform.rotation = Quaternion.Euler(0, -cameraPos.transform.eulerAngles.y, 0);
        PlayBeforeGame();
    }

    // 미니게임 시작버튼 On (타임라인 시그널 용)
    public void OngameStartBtn()
    {
        gameStartBtn.SetActive(true);
    }

    // 미니게임 시작버튼 Off (타임라인 시그널 용)
    public void OffgameStartBtn()
    {
        gameStartBtn.SetActive(false);
    }

    // 미니게임 시작 전 타임라인 시작
    public void PlayBeforeGame()
    {
        director.playableAsset = miniGameStartTimeLine;
        director.Play();
    }

    // 미니게임 종료 후 타임라인 시작
    public void PlayAfterGame()
    {
        director.playableAsset = miniGameEndTimeLine;
        director.Play();
    }


    IEnumerator StartCount()
    {
        UIPlayerHsy.Instance.countDownText.gameObject.SetActive(true);
        int count = 3;
        while (count >= 0)
        {
            yield return new WaitForSeconds(1);
            if (count == 0)
            {
                Manager.Instance.SoundMgr.PlaySfx(ScDefine.ScSound.Ch2_2_GameStart);
                UIPlayerHsy.Instance.countDownText.text = "시작";
            }
            else
            {
                Manager.Instance.SoundMgr.PlaySfx(ScDefine.ScSound.Ch2_2_CountDown);
                UIPlayerHsy.Instance.countDownText.text = count.ToString();
            }
            count--;
        }
        // 미니 게임 시작 후 플레이어 이벤트 활성화, 오브젝트 활성화
        playerScript.isGamePlaying = true;
        UIPlayerHsy.Instance.countDownText.text = "";
        playerScript.ConnectPlayerTriggerEvent();
        playNavInfo.SetActive(true);
        CreateChildNpc();
        yield return StartCoroutine(TimmerCor());
        Manager.Instance.SoundMgr.PlaySfx(ScDefine.ScSound.Ch2_2_GameEnd);
        // 종료 후 이벤트 해지, 오브젝트 비활성화
        playNavInfo.SetActive(false);
        playerScript.DisConnectPlayerTriggerEvent();
        playerScript.isGamePlaying = false;
        UIPlayerHsy.Instance.countDownText.text = "종료";
        yield return StartCoroutine(UIPlayerHsy.Instance.SideFillProduction(0,1,3));
        yield return StartCoroutine(UIPlayerHsy.Instance.SideFillProduction(1,0,3));
        UIPlayerHsy.Instance.countDownText.text = "";
        PlayAfterGame();
    }
    public void OnClickPlayeButton()
    {
        Manager.Instance.SoundMgr.PlaySfx(ScDefine.ScSound.Ok);
        StartCoroutine(StartCount());
        OffgameStartBtn();
    }

    // 미니게임 제한시간 체크
    IEnumerator TimmerCor()
    {
        float currentTime = 0;
        while (currentTime<maxTime)
        {
            currentTime += Time.deltaTime;
            timer?.Invoke(currentTime, maxTime);
            yield return null;
        }
    }

    public void CreateChildNpc()
    {
        childArray = new GameObject[childMaxCount];
        for (int i=0; i< childPosArray.Length; i++)
        {
            childArray[i] = Instantiate(childPrefab[UnityEngine.Random.Range(0, childPrefab.Length)], childPosArray[i].position,Quaternion.identity);
        }
    }
    public void ResetChanter2_2()
    {
        playerScript.findChildCount = 0;
        for (int i = 0; i < childArray.Length; i++)
        {
            Destroy(childArray[i]);
        }
    }
    public void LoadCh2Login()
    {
        Manager.Instance.SceneMgr.LoadScene(ScDefine.ScScene.Ch2Login);
    }
}
