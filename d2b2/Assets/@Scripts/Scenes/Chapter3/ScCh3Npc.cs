using Cysharp.Threading.Tasks;
using DG.Tweening;
using Pathfinding;
using Photon.Pun;
using UnityEngine;

public class ScCh3Npc : MonoBehaviour
{
    private const float WALK_SPEED = 1f;
    private const float RUN_SPEED = 3f;
    private const float ROTATE_DURATION = 0.4f;

    private int id;
    private RichAI ai;
    private AIDestinationSetter destinationSetter;
    private ScPathPoint previousDestinationPoint;
    private ScPathPoint currentDestinationPoint;
    private ScDefine.ScPathPointNextAction currentAction;
    private ScCharacter character;
    private bool canUpdateMethod;   // Only For MasterClient
    private bool canUpdateAnimation = true;


    
    private void Awake()
    {
        ai = GetComponent<RichAI>();
        destinationSetter = GetComponent<AIDestinationSetter>();
        character = GetComponent<ScCharacter>();
    }
    
    private void Update()
    {
        if (ai.reachedEndOfPath && !ai.pathPending && canUpdateAnimation)
            character.SetAnimation(ScDefine.ScNpcAnimState.Idle);
        
        if (!PhotonNetwork.IsMasterClient)
            return;
        
        if (canUpdateMethod)
            return;
        
        if (ai.reachedEndOfPath && !ai.pathPending)
        {
            ScDefine.ScPathPointNextAction nextAct;

            if (currentAction == ScDefine.ScPathPointNextAction.Move)
                nextAct = currentDestinationPoint.GetNextRandomAction();
            else
                nextAct = currentDestinationPoint.GetNextRandomAction(currentAction);

            switch (nextAct)
            {
                case ScDefine.ScPathPointNextAction.Move:
                    {
                        ScPathPoint newPathPoint = currentDestinationPoint.GetNextRandomPathPoint(previousDestinationPoint);
                        ScCh3NpcService.Instance.BroadcastUpdateNpcNextAction(id, nextAct, newPathPoint);
                        break;
                    }
                default:
                    {
                        ScCh3NpcService.Instance.BroadcastUpdateNpcNextAction(id, nextAct);
                        break;
                    }
            }

            canUpdateMethod = true;
        }
    }



    public void Init(int npcId, ScPathPoint spawnPoint)
    {
        id = npcId;
        ai.Teleport(spawnPoint.transform.position);

        currentDestinationPoint = spawnPoint;
        ScPathPoint newPathPoint = spawnPoint.GetNextRandomPathPoint(previousDestinationPoint);
        Move(newPathPoint, false);
    }

    public void UpdateNextAction(ScDefine.ScPathPointNextAction nextAction, ScPathPoint newPathPoint, bool isRun)
    {
        switch (nextAction)
        {
            case ScDefine.ScPathPointNextAction.Move:
                {
                    Move(newPathPoint, isRun);

                    canUpdateMethod = false;
                    break;
                }
            case ScDefine.ScPathPointNextAction.Crosswalk:
                {
                    ReadyForCrosswalk().Forget();
                    break;
                }
            case ScDefine.ScPathPointNextAction.LookAround:
                {
                    LookAround().Forget();
                    break;
                }
            case ScDefine.ScPathPointNextAction.Destroy:
                {
                    ScCh3NpcService.Instance.OnNpcDestroy(id);
                    Destroy(gameObject);
                    break;
                }
            default:
                break;
        }
    }



    private void Move(ScPathPoint newPathPoint, bool isRun)
    {
        currentAction = ScDefine.ScPathPointNextAction.Move;

        previousDestinationPoint = currentDestinationPoint;
        currentDestinationPoint = newPathPoint;

        destinationSetter.target = currentDestinationPoint.transform;
        ai.SearchPath();

        character.SetRaiseHandAnimation(false);

        if (isRun)
        {
            character.SetAnimation(ScDefine.ScNpcAnimState.Running);
            ai.maxSpeed = RUN_SPEED;
        }
        else
        {
            character.SetAnimation(ScDefine.ScNpcAnimState.Walking);
            ai.maxSpeed = WALK_SPEED;
        }
    }

    private async UniTaskVoid ReadyForCrosswalk()
    {
        currentAction = ScDefine.ScPathPointNextAction.Crosswalk;

        ai.updateRotation = false;
        await transform.DOLookAt(currentDestinationPoint.trafficLightOppositePoint.transform.position, ROTATE_DURATION);
        
        currentDestinationPoint.trafficLight.onGreenLightActivated.AddListener(OnGreenActivated);
    }

    private void OnGreenActivated()
    {
        // 이벤트 구독해제
        currentDestinationPoint.trafficLight.onGreenLightActivated.RemoveListener(OnGreenActivated);

        // PathPoint 업데이트
        previousDestinationPoint = currentDestinationPoint;
        currentDestinationPoint = previousDestinationPoint.trafficLightOppositePoint;

        // 목적지 업데이트
        ai.updateRotation = true;
        ai.maxSpeed = WALK_SPEED;
        destinationSetter.target = currentDestinationPoint.transform;
        ai.SearchPath();

        // 애니메이션
        character.SetRaiseHandAnimation(true);
        character.SetAnimation(ScDefine.ScNpcAnimState.Walking);

        canUpdateMethod = false;
    }

    private async UniTaskVoid LookAround()
    {
        currentAction = ScDefine.ScPathPointNextAction.LookAround;

        canUpdateAnimation = false;
        ai.updateRotation = false;
        await transform.DOLookAt(currentDestinationPoint.oppositePoint.transform.position, ROTATE_DURATION);
        
        character.SetRaiseHandAnimation(false);
        character.SetAnimation(ScDefine.ScNpcAnimState.LookAround);

        await UniTask.Delay(5500);

        ai.updateRotation = true;
        canUpdateAnimation = true;
        canUpdateMethod = false;
    }
}
