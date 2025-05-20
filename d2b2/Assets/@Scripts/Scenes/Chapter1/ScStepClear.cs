using UnityEngine;

public class ScStepClear : MonoBehaviour
{

    private async void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == ScDefine.Layer.PlayerIndex)
        {
            //ScChapter1.Instance.CurrentSetp++;
            await ScChapter1.Instance.NextStep();

            if (Ch1_Step.CurrentSetp >= ScRespawn.Instance.SpawnCount)
            {
                EndGame();
                return;
            }

            ScRespawn.Instance.Init(Ch1_Step.CurrentSetp);
            Debug.Log("다음스텝으로 넘어갔음." + Ch1_Step.CurrentSetp);
            gameObject.SetActive(false);
        }
    }


    private void EndGame()
    {
        Debug.Log("Chapter1 클리어 " + Ch1_Step.CurrentSetp);
        Manager.Instance.SceneMgr.LoadScene(ScDefine.ScScene.Ch1Login);
        gameObject.SetActive(false);
    }
}
