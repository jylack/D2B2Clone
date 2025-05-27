using Pathfinding;
using System.Reflection;
using UnityEngine;

public class ScCh3Npc : MonoBehaviour
{
    [SerializeField] private GameObject[] pathObjects;

    private RichAI ai;
    private AIDestinationSetter destinationSetter;
    private bool hasArrived;
    private int posIndex;
    private bool isManualMove;
    private Vector3 source;
    private Vector3 destination;
    private float moveSpeed = 2f;



    private void Awake()
    {
        ai = GetComponent<RichAI>();
        destinationSetter = GetComponent<AIDestinationSetter>();
    }

    private void Start()
    {
        if (pathObjects?.Length > 0)
            destinationSetter.target = GetNextDestination();

        //InvokeRepeating(nameof(MoveToRandomPosition), 1f, 5f);
    }

    private void Update()
    {
        if (isManualMove)
        {
            Vector3 diff = destination - source;

            if (diff.magnitude < 0.1f)
            {
                StartPathFinding();
                hasArrived = false;
                return;
            }

            Vector3 dir = diff.normalized;
            transform.position = dir * 5f * Time.deltaTime;
        }
        //else if (!hasArrived && ai.reachedEndOfPath && !ai.pathPending)
        //{
        //    hasArrived = true;
        //    destinationSetter.target = GetNextDestination();
        //    hasArrived = false;
        //}
    }



    public void StopPathFinding()
    {
        source = transform.position;
        ai.canMove = false;
        ai.canSearch = false;

        typeof(RichAI).GetMethod("ClearPath", BindingFlags.Instance | BindingFlags.NonPublic)?.Invoke(ai, null);

        ai.Teleport(source, true);

        destination = GetNextDestination().position;
        isManualMove = true;

        hasArrived = true;
    }

    public void StartPathFinding()
    {
        ai.Teleport(transform.position);
        ai.canMove = true;
        ai.canSearch = true;
    }



    private Transform GetNextDestination()
    {
        return pathObjects[(posIndex++)].transform;
    }

    private void MoveToRandomPosition()
    {
        Vector3 randomOffset = Random.insideUnitSphere * 10f;
        randomOffset.y = 0f;

        Vector3 randomPos = transform.position + randomOffset;

        NNInfo info = AstarPath.active.GetNearest(randomPos);
        ai.destination = info.position;
        ai.SearchPath();
    }
}
