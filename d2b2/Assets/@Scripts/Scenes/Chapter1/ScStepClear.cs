using Cysharp.Threading.Tasks.Triggers;
using System.Threading.Tasks;
using UnityEngine;

public class ScStepClear : MonoBehaviour
{

    private async void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == ScDefine.Layer.PlayerIndex)
        {
            //ScChapter1.Instance.CurrentSetp++;
            await ScChapter1.Instance.NextStep();

            if (ScChapter1.Instance.CurrentSetp >= ScRespawn.Instance.SpawnCount)
            {
                EndGame();
                return;
            }

            ScRespawn.Instance.Init(ScChapter1.Instance.CurrentSetp);            
            //other.gameObject.GetComponent<ScRespawn>().NextPos(ScChapter1.Instance.CurrentSetp);
            Debug.Log("다음스텝으로 넘어갔음." + ScChapter1.Instance.CurrentSetp);
            gameObject.SetActive(false);
        }
    }


    private void EndGame()
    {
        Debug.Log("Chapter1 클리어 " + ScChapter1.Instance.CurrentSetp);
        Manager.Instance.SceneMgr.LoadScene(ScDefine.ScScene.Ch1Login);
        gameObject.SetActive(false);
    }
}
