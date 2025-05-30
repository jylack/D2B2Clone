using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField] private Transform camera;
    [SerializeField] private ScMirrorPlayerHsy playerScript;
    [Header("Npc")]
    [SerializeField] private GameObject npcPrefab;
    [SerializeField] private ParticleSystem npcCreateParticle;
    [SerializeField] private Vector3 npcSpawnPos;
    [SerializeField] private GameObject gameStartBtn;
    [Header("Child")]
    [SerializeField] private GameObject[] childPrefab;
    [SerializeField] private Transform[] childPosArray;
    [Header("TimeLine")]
    [SerializeField] private PlayableAsset miniGameStartTimeLine;
    [SerializeField] private PlayableAsset miniGameEndTimeLine;
    [SerializeField] private PlayableDirector director;
    [Header("navCanvas")]
    [SerializeField] GameObject navCanvas;
    [Header("PlaySetting")]
    [SerializeField] private float maxTime =30;
    public event Action<float,float> timer;



    IEnumerator Start()
    {
        yield return new WaitForSeconds(2);
        //playerScript.isGamePlaying= false;
        //playerPos.transform.position = new Vector3 (-1.39f, 1.96f, 0.054f);
        //cameraOffset.transform.rotation = Quaternion.Euler(0, -camera.transform.eulerAngles.y, 0);
        //PlayBeforeGame();
    }
    public void OngameStartBtn()
    {
        gameStartBtn.SetActive(true);
    }
    public void OffgameStartBtn()
    {
        gameStartBtn.SetActive(false);
    }
    public void PlayBeforeGame()
    {
        director.playableAsset = miniGameStartTimeLine;
        director.Play();
    }

    public void PlayAfterGame()
    {
        director.playableAsset = miniGameEndTimeLine;
        director.Play();
    }
    IEnumerator count()
    {
        UIPlayerHsy.Instance.countDownText.gameObject.SetActive(true);
        int count = 3;
        while (count >= 0)
        {
            if (count == 0)
            {
                UIPlayerHsy.Instance.countDownText.text = "시작";
            }
            else
            {
                UIPlayerHsy.Instance.countDownText.text = count.ToString();
            }
            yield return new WaitForSeconds(1);
            count--;
        }
        playerScript.isGamePlaying = true;
        UIPlayerHsy.Instance.countDownText.text = "";
        navCanvas.SetActive(true);
        CreateChildNpc();
        yield return StartCoroutine(TimmerCor());
        navCanvas.SetActive(false);
        playerScript.DisConnectPlayerTriggerEvent();
        UIPlayerHsy.Instance.countDownText.text = "종료";
        yield return StartCoroutine(UIPlayerHsy.Instance.SideFillProduction(0,1,3));
        yield return StartCoroutine(UIPlayerHsy.Instance.SideFillProduction(1,0,3));
        UIPlayerHsy.Instance.countDownText.text = "";
        PlayAfterGame();
        playerScript.isGamePlaying = false;
        playerScript.ConnectPlayerTriggerEvent();
    }
    public void OnClickPlayeButton()
    {
        StartCoroutine(count());
        OffgameStartBtn();
    }
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
        for (int i=0; i< childPosArray.Length; i++)
        {
            Instantiate(childPrefab[UnityEngine.Random.Range(0, childPrefab.Length)], childPosArray[i].position,Quaternion.identity);
        }
    }

}
