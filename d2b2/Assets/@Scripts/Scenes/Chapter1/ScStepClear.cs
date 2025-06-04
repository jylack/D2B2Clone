using UnityEngine;
using UnityEngine.Events;

public class ScStepClear : MonoBehaviour
{
    [SerializeField] private UnityEvent OnStepClear;

    private async void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == ScDefine.Layer.PlayerIndex)
        {
            OnStepClear?.Invoke();

            //ScChapter1.Instance.CurrentSetp++;
            await ScChapter1.Instance.NextStep();

            if (ScChapter1.CurrentSetp >= ScRespawn.Instance.SpawnCount)
            {
                EndGame();
                return;
            }


            ScRespawn.Instance.Init(ScChapter1.CurrentSetp);
            Debug.Log("다음스텝으로 넘어갔음." + ScChapter1.CurrentSetp);
            gameObject.SetActive(false);
        }
    }


    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.layer == ScDefine.Layer.PlayerIndex)
    //    {
    //        OnStepClear?.Invoke();
    //    }
    //}



    private void EndGame()
    {
        Debug.Log("Chapter1 클리어 " + ScChapter1.CurrentSetp);
        Manager.Instance.SceneMgr.LoadScene(ScDefine.ScScene.Ch1Login);
        gameObject.SetActive(false);
    }
}
