using TMPro;
using UnityEngine;

public class UICh3RankItem : UIBase
{
    [SerializeField] private TMP_Text nickNameText;
    [SerializeField] private TMP_Text scoreText;



    public void SetData(string nickName, int score)
    {
        nickNameText.text = nickName;
        scoreText.text = score.ToString();
    }
}
