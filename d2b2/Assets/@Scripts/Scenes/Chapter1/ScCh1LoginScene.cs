using UnityEngine;

public class ScCh1LoginScene : ScSceneBase
{
    private void Start()
    {
        //Resopne pos reset
        if (ScChapter1.CurrentSetp > 0)
        {
            ScChapter1.CurrentSetp = 0;
        }                       
    }
}