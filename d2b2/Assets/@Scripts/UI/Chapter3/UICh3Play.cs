using Photon.Pun;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class UICh3Play : UIBase
{
    public static UICh3Play Instance { get; private set; }

    [Header("Scoreboard")]
    [SerializeField] private GameObject currentPlayerBg;
    [SerializeField] private UICh3RankItem[] rankItems;
    [Header("Information")]
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text messageText;
    [SerializeField] private TMP_Text missedCharacterCountText;

    private int missedCharacterCount;



    private void Awake()
    {
        Instance = this;
    }



    public void GoToLogin()
    {
        base.LoadScene(ScDefine.ScScene.Ch3Login);
    }

    public void UpdateTime(int time)
    {
        timeText.text = time.ToString();
    }

    public void UpdateScores(List<ScCh3ScoreData> scoreDatas)
    {
        List<ScCh3ScoreData> rankScoreDatas = scoreDatas.OrderByDescending(x => x.score).ThenBy(x => x.nickName).ToList();

        (0..rankScoreDatas.Count).ForEach(i =>
        {
            ScCh3ScoreData data = rankScoreDatas[i];
            UICh3RankItem item = rankItems[i];

            item.SetData(data.nickName, data.score);

            if (PhotonNetwork.LocalPlayer.ActorNumber == data.actorNumber)
                currentPlayerBg.transform.position = item.transform.position;
        });
    }

    public void IncreaseMissedCharacterCount()
    {
        missedCharacterCount++;
        missedCharacterCountText.text = $"놓친 캐릭터 수: {missedCharacterCount}";
    }
}