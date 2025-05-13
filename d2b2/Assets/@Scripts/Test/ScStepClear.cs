using Cysharp.Threading.Tasks.Triggers;
using UnityEngine;

public class ScStepClear : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.layer);
        //Debug.Log(ScDefine.Layer.PlayerIndex);

        if (other.gameObject.layer == ScDefine.Layer.PlayerIndex)
        {
            ScChapter1.Instance.CurrentSetp++;
            var temp = other.gameObject.GetComponent<ScRespawn>();

            if (ScChapter1.Instance.CurrentSetp >= temp.SpawnCount)
            {
                EndGame();
                return;
            }    

            temp.Init(ScChapter1.Instance.CurrentSetp);            
            //other.gameObject.GetComponent<ScRespawn>().NextPos(ScChapter1.Instance.CurrentSetp);
            Debug.Log("다음스텝으로 넘어갔음.");
            gameObject.SetActive(false);
        }
    }


    private void EndGame()
    {
        Debug.Log("Chapter1 클리어");
    }
}
