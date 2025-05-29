using System;
using System.Collections;
using System.Collections.Generic;
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
    public event Action<float> timer;



    IEnumerator Start()
    {
        yield return new WaitForSeconds(2);
        playerScript.isGamePlaying= false;
        playerPos.transform.position = new Vector3 (-1.5f, 2f, 0);
        cameraOffset.transform.rotation = Quaternion.Euler(0, -camera.transform.eulerAngles.y, 0);
        //PlayBeforeGame();
        OnClickPlayeButton();
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

    public void OnClickPlayeButton()
    {
        OffgameStartBtn();
        CreateChildNpc();
        StartCoroutine(TimmerCor());
        navCanvas.SetActive(true);
        playerScript.ConnectPlayerTriggerEvent();
        playerScript.isGamePlaying= true;
    }
    IEnumerator TimmerCor()
    {
        float currentTime = 0;
        while (true)
        {
            currentTime += Time.deltaTime;
            timer?.Invoke(currentTime);
            yield return null;
        }
        playerScript.DisConnectPlayerTriggerEvent();
        PlayAfterGame();
    }

    public void CreateChildNpc()
    {
        for (int i=0; i< childPosArray.Length; i++)
        {
            Instantiate(childPrefab[UnityEngine.Random.Range(0, childPrefab.Length)], childPosArray[i].position,Quaternion.identity);
        }
    }

}
