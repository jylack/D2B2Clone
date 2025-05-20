using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using UnityEngine;

public class ScCharacter : MonoBehaviour
{
    private const int handSideLayerIndex = 1;
    private static int AnimStateHash { get; } = Animator.StringToHash("State");
    private static int AnimIsRaiseHandHash { get; } = Animator.StringToHash("IsRaiseHand");

    [SerializeField] private Animator animator;

    private ScDefine.ScHandSide currentHandSide;



    public void SetAnimation(ScDefine.ScNpcAnimState animState)
    {
        animator.SetInteger(AnimStateHash, (int)animState);
    }

    public void SetRaiseHandAnimation(ScDefine.ScHandSide handSide)
    {
        if (currentHandSide != ScDefine.ScHandSide.Right && handSide == ScDefine.ScHandSide.Right
         || currentHandSide == ScDefine.ScHandSide.Right && handSide != ScDefine.ScHandSide.Right)
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }

        float targetWeight = handSide == ScDefine.ScHandSide.None ? 0 : 1;

        animator.SetBool(AnimIsRaiseHandHash, targetWeight == 1);

        float curWeight = animator.GetLayerWeight(handSideLayerIndex);
        if (curWeight != targetWeight)
        {
            DOTween.To(
                () => curWeight,
                w => animator.SetLayerWeight(handSideLayerIndex, w),
                targetWeight,
                0.2f)
                .SetLink(gameObject);
        }

        currentHandSide = handSide;
    }



    // 테스트 코드 ===========================================================================
    //private void Start()
    //{
    //    Run().Forget();
    //}

    //private async UniTask Run()
    //{
    //    try
    //    {
    //        //const int max = 5;
    //        //int flag = 0;

    //        //while (true)
    //        //{
    //        //    SetAnimation((ScDefine.ScNpcAnimState)flag);

    //        //    if (flag == 3)
    //        //        await UniTask.Delay(5000);
    //        //    else
    //        //        await UniTask.Delay(3000);

    //        //    flag = ++flag % max;
    //        //}

    //        int handSide = 0;
    //        SetAnimation(ScDefine.ScNpcAnimState.Walking);

    //        while (true)
    //        {
    //            print(handSide);
    //            SetRaiseHandAnimation((ScDefine.ScHandSide)handSide);
    //            handSide = ++handSide % 3;
                
    //            await UniTask.Delay(3000);
    //        }
    //    }
    //    catch (OperationCanceledException ex)
    //    {
    //        Debug.Log(ex.Message);
    //    }
    //    catch (Exception ex)
    //    {
    //        Debug.LogException(ex);
    //    }
    //}
    // 테스트 코드 ===========================================================================
}
