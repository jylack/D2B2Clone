using DG.Tweening;
using UnityEngine;

public class ScCharacter : MonoBehaviour
{
    private const int HandSideLayerIndex = 1;
    private static int AnimStateHash { get; } = Animator.StringToHash("State");
    private static int AnimIsRaiseHandHash { get; } = Animator.StringToHash("IsRaisingHand");

    [SerializeField] private Animator animator;

    private bool isRaisingHand;
    private Tweener handTween;

    
    
    public void SetIdleAnim() => SetAnimation(ScDefine.ScNpcAnimState.Idle);
    public void SetWalkingAnim() => SetAnimation(ScDefine.ScNpcAnimState.Walking);
    public void SetRunningAnim() => SetAnimation(ScDefine.ScNpcAnimState.Running);
    public void RaiseHandOn() => SetRaiseHandAnimation(true);
    public void RaiseHandOff() => SetRaiseHandAnimation(false);
    public void SetLookAroundAnim() => SetAnimation(ScDefine.ScNpcAnimState.LookAround);

    public void SetAnimation(ScDefine.ScNpcAnimState animState)
    {
        animator?.SetInteger(AnimStateHash, (int)animState);
    }

    public void SetRaiseHandAnimation(bool raisingHand)
    {
        if (raisingHand != isRaisingHand)
        {
            animator?.SetBool(AnimIsRaiseHandHash, raisingHand);
            float curWeight = 0;

            if (animator != null)
                curWeight = animator.GetLayerWeight(HandSideLayerIndex);

            float targetWeight = raisingHand ? 1 : 0;

            handTween?.Kill();
            handTween = DOTween.To(
                    () => curWeight,
                    w => animator?.SetLayerWeight(HandSideLayerIndex, w),
                    targetWeight,
                    0.4f)
                .SetLink(gameObject);

            isRaisingHand = raisingHand;
        }
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
    //         //const int max = 5;
    //         //int flag = 0;
    //
    //         //while (true)
    //         //{
    //         //    SetAnimation((ScDefine.ScNpcAnimState)flag);
    //
    //         //    if (flag == 3)
    //         //        await UniTask.Delay(5000);
    //         //    else
    //         //        await UniTask.Delay(3000);
    //
    //         //    flag = ++flag % max;
    //         //}
    //
    //         bool flag = false;
    //         SetAnimation(ScDefine.ScNpcAnimState.Running);
    //
    //         while (true)
    //         {
    //             SetRaiseHandAnimation(flag);
    //             flag = !flag;
    //             
    //             await UniTask.Delay(3000);
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
