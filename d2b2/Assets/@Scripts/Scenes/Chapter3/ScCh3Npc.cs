using Cysharp.Threading.Tasks;
using DG.Tweening;
using Pathfinding;
using Photon.Pun;
using UnityEngine;

public class ScCh3Npc : MonoBehaviour
{
    private const float WALK_SPEED = 1f;
    private const float RUN_SPEED = 2.2f;
    private const float ROTATE_DURATION = 0.4f;

    private int id;
    private RichAI ai;
    private AIDestinationSetter destinationSetter;
    private ScPathPoint previousDestinationPoint;
    private ScPathPoint currentDestinationPoint;
    private ScDefine.ScPathPointNextAction currentAction;
    private ScCharacter character;
    private bool canDoUpdateMethod = true;    // Only For MasterClient
    private bool canUpdateAnimation = true;
    private bool isDoingBadThing;
    private GameObject badThingPointer;
    private Outline outline;


    
    private void Awake()
    {
        ai = GetComponent<RichAI>();
        destinationSetter = GetComponent<AIDestinationSetter>();
        character = GetComponent<ScCharacter>();
        outline = GetComponent<Outline>();
    }

    private void Update()
    {
        if (ai.reachedEndOfPath && !ai.pathPending && canUpdateAnimation)
            character.SetAnimation(ScDefine.ScNpcAnimState.Idle);
        
        if (!PhotonNetwork.IsMasterClient)
            return;
        
        if (!canDoUpdateMethod)
            return;
        
        if (ai.reachedEndOfPath && !ai.pathPending)
        {
            ScDefine.ScPathPointNextAction nextAct;

            if (currentAction != ScDefine.ScPathPointNextAction.DoBadThing && currentDestinationPoint.CanDoBadThing() && ScCh3NpcService.Instance.DecreaseBadThingToken())
            {
                nextAct = ScDefine.ScPathPointNextAction.DoBadThing;
            }
            else if (currentAction == ScDefine.ScPathPointNextAction.DoBadThing || currentAction == ScDefine.ScPathPointNextAction.Crosswalk)
            {
                nextAct = currentDestinationPoint.GetNextRandomAction(ScDefine.ScPathPointNextAction.Crosswalk);
            }
            else
            {
                if (currentAction == ScDefine.ScPathPointNextAction.Move)
                    nextAct = currentDestinationPoint.GetNextRandomAction();
                else
                    nextAct = currentDestinationPoint.GetNextRandomAction(currentAction);
            }

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

            canDoUpdateMethod = false;
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
        if (badThingPointer != null)
            Destroy(badThingPointer);

        switch (nextAction)
        {
            case ScDefine.ScPathPointNextAction.Move:
                {
                    currentAction = ScDefine.ScPathPointNextAction.Move;
                    isDoingBadThing = false;

                    Move(newPathPoint, isRun);
                    canDoUpdateMethod = true;
                    break;
                }
            case ScDefine.ScPathPointNextAction.Crosswalk:
                {
                    currentAction = ScDefine.ScPathPointNextAction.Crosswalk;
                    isDoingBadThing = false;

                    ReadyForCrosswalk().Forget();
                    break;
                }
            case ScDefine.ScPathPointNextAction.LookAround:
                {
                    currentAction = ScDefine.ScPathPointNextAction.LookAround;
                    isDoingBadThing = false;

                    LookAround().Forget(); 
                    break;
                }
            case ScDefine.ScPathPointNextAction.DoBadThing:
                {
                    currentAction = ScDefine.ScPathPointNextAction.DoBadThing;
                    isDoingBadThing = true;

                    DoBadThing();

                    canDoUpdateMethod = true;
                    break;
                }
            case ScDefine.ScPathPointNextAction.Destroy:
                {
                    currentAction = ScDefine.ScPathPointNextAction.Destroy;
                    isDoingBadThing = false;

                    ScCh3NpcService.Instance.OnNpcDestroy(id);
                    Destroy(gameObject);
                    break;
                }
            default:
                break;
        }
    }

    public void Select()
    {
        // vfx
        // +1 텍스트
    }
    
    public void OnRayHoverEnter()
    {
        if (isDoingBadThing)
            outline.enabled = true;
    }

    public void OnRayHoverExit()
    {
        outline.enabled = false;
    }



    private async UniTaskVoid ReadyForCrosswalk()
    {
        ai.updateRotation = false;
        await transform.DOLookAt(currentDestinationPoint.trafficLightOppositePoint.transform.position, ROTATE_DURATION);

        currentDestinationPoint.trafficLight.onGreenLightActivated.AddListener(OnGreenActivated);
    }

    private async UniTaskVoid LookAround()
    {
        canUpdateAnimation = false;
        ai.updateRotation = false;
        await transform.DOLookAt(currentDestinationPoint.oppositePoint.transform.position, ROTATE_DURATION);

        character.SetRaiseHandAnimation(false);
        character.SetAnimation(ScDefine.ScNpcAnimState.LookAround);

        await UniTask.Delay(5500);

        ai.updateRotation = true;
        canUpdateAnimation = true;
        canDoUpdateMethod = true;
    }

    private void Move(ScPathPoint newPathPoint, bool isRun)
    {
        previousDestinationPoint = currentDestinationPoint;
        currentDestinationPoint = newPathPoint;

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

        destinationSetter.target = currentDestinationPoint.transform;
        ai.SearchPath();
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

        canDoUpdateMethod = true;
    }

    private void DoBadThing()
    {
        previousDestinationPoint = currentDestinationPoint;
        currentDestinationPoint = previousDestinationPoint.oppositePoint;

        destinationSetter.target = currentDestinationPoint.transform;
        ai.maxSpeed = RUN_SPEED;
        ai.SearchPath();

        character.SetAnimation(ScDefine.ScNpcAnimState.Running);

        badThingPointer = Manager.Instance.ResourceMgr.InstantiateBadThingPointer();
        badThingPointer.transform.SetParent(transform);
        badThingPointer.transform.localPosition = new Vector3(0f, 2f, 0f);
    }
}
