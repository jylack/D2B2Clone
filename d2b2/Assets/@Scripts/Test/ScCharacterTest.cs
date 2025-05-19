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
            const int max = 4;
            int flag = 0;

            while (true)
            {
                animator.SetInteger(Animator.StringToHash("State"), flag);
                await UniTask.Delay(3000);

                flag = ++flag % max;
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
