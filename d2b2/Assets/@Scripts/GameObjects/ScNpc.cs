using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class ScNpc : MonoBehaviour
{
    private static int AnimStateHash { get; } = Animator.StringToHash("State");
    
    [SerializeField] private Animator animator;

    private ScDefine.ScHandSide currentHandSide;
    
    
    
    public void SetAnimation(ScDefine.ScNpcAnimState animState)
    {
        animator.SetInteger(AnimStateHash, (int)animState);
    }

    public void SetRaiseHandAnimation(ScDefine.ScHandSide handSide)
    {
        if (currentHandSide != ScDefine.ScHandSide.Left && handSide == ScDefine.ScHandSide.Left
         || currentHandSide == ScDefine.ScHandSide.Left && handSide != ScDefine.ScHandSide.Left)
        {
            transform.localScale.Scale(Vector3.left);
        }
        
        // set raise hand animation
    }
    
    
    
    // 테스트 코드 ===========================================================================
    // private void Start()
    // {
    //     Run().Forget();
    // }
    //
    // private async UniTask Run()
    // {
    //     try
    //     {
    //         const int max = 5;
    //         int flag = 0;
    //
    //         while (true)
    //         {
    //             SetAnimation((ScDefine.ScNpcAnimState)flag);
    //             
    //             if (flag == 3)
    //                 await UniTask.Delay(5000);
    //             else
    //                 await UniTask.Delay(3000);
    //             
    //             flag = ++flag % max;
    //         }
    //     }
    //     catch (OperationCanceledException ex)
    //     {
    //         Debug.Log(ex.Message);
    //     }
    //     catch (Exception ex)
    //     {
    //         Debug.LogException(ex);
    //     }
    // }
    // 테스트 코드 ===========================================================================
}
