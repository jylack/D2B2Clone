using Pathfinding;
using Photon.Pun;
using UnityEngine;

public class ScCh3Npc : MonoBehaviour
{
    public int Id { get; private set; }
    
    private RichAI ai;
    private AIDestinationSetter destinationSetter;
    private ScPathPoint previousDestinationPoint;
    private ScPathPoint currentDestinationPoint;
    private ScCharacter character;
    private bool isSendBroadcast;


    
    private void Awake()
    {
        ai = GetComponent<RichAI>();
        destinationSetter = GetComponent<AIDestinationSetter>();
        character = GetComponent<ScCharacter>();
    }
    
    private void Update()
    {
        if (ai.reachedEndOfPath && !ai.pathPending)
            character.SetAnimation(ScDefine.ScNpcAnimState.Idle);
        
        if (!PhotonNetwork.IsMasterClient)
            return;
        
        if (isSendBroadcast)
            return;
        
        if (ai.reachedEndOfPath && !ai.pathPending)
        {
            // 다음 액션 선택
            // 브로드캐스트 전달
            
            ScCh3PlayService.Instance.BroadcastUpdateNpcPathPoint(Id);
            isSendBroadcast = true;
        }
    }



    public void Init(int npcId, ScPathPoint spawnPoint)
    {
        Id = npcId;
        ai.Teleport(spawnPoint.transform.position);
        UpdateDestination(spawnPoint);
    }

    public void UpdateNextAction()
    {
        ScDefine.ScPathPointNextAction action = currentDestinationPoint.GetNextRandomAction();

        switch (action)
        {
            case ScDefine.ScPathPointNextAction.Move:
            {
                UpdateDestination(currentDestinationPoint);
                break;
            }
            case ScDefine.ScPathPointNextAction.Crosswalk:
            {
                break;
            }
            case ScDefine.ScPathPointNextAction.Destroy:
            {
                ScCh3NpcService.Instance.OnNpcDestroy(Id);
                Destroy(gameObject);
                break;
            }
            default:
                break;
        }

        isSendBroadcast = false;
    }



    private void UpdateDestination(ScPathPoint curPathPoint)
    {
        currentDestinationPoint = curPathPoint.GetNextRandomPathPoint(previousDestinationPoint);
        previousDestinationPoint = curPathPoint;
        destinationSetter.target = currentDestinationPoint.transform;
        ai.SearchPath();
        
        character.SetAnimation(ScDefine.ScNpcAnimState.Walking);
    }
}
