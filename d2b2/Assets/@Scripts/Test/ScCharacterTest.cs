using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class ScCharacterTest : ScObjectBase
{
    [SerializeField] private Animator animator;




    private void Start()
    {
        Run().Forget();
    }



    private async UniTask Run()
    {
        try
        {
            bool flag = false;

            while (true)
            {
                flag = !flag;
                animator.SetInteger(Animator.StringToHash("State"), flag ? 1 : 0);
                await UniTask.Delay(5000);
            }
        }
        catch (OperationCanceledException ex)
        {
            Debug.Log(ex.Message);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }
}
