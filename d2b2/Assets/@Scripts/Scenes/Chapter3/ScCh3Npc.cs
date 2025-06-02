using Pathfinding;
using UnityEngine;

public class ScCh3Npc : MonoBehaviour
{
    [SerializeField] private GameObject[] pathObjects;

    private RichAI ai;
    private AIDestinationSetter destinationSetter;
    private ScPathPoint previousPathPoint;
    private ScPathPoint currentPathPoint;
    


    private void Awake()
    {
        ai = GetComponent<RichAI>();
        destinationSetter = GetComponent<AIDestinationSetter>();
    }

    private void Start()
    {

    }

    private void Update()
    {
        if (ai.reachedEndOfPath && !ai.pathPending)
        {
            ScPathPoint nextPathpt = currentPathPoint.GetNextPathPoint();

            previousPathPoint = currentPathPoint;
            currentPathPoint = nextPathpt;

            destinationSetter.target = currentPathPoint.transform;
            ai.SearchPath();
        }
    }



    public void SetDestination(ScPathPoint pathPt)
    {
        currentPathPoint = pathPt;
        destinationSetter.target = pathPt.transform;
    }
}
