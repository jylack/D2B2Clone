using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScPathPoint : ScObjectBase
{
    [Header("TrafficLight")]
    public ScTrafficLight trafficLight;
    public ScPathPoint trafficLightOppositePoint;
    [Header("Etc")]
    public ScPathPoint oppositePoint;
    [SerializeField] private ScDefine.ScPathPointNextAction[] nextPathType;
    [SerializeField] private List<ScPathPoint> nearPoints;



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;

        if (nearPoints?.Count > 0)
        {
            nearPoints.ForEach(x =>
            {
                Vector3 dir = (x.transform.position - transform.position).normalized;
                Gizmos.DrawRay(transform.position + Vector3.up * 0.15f, dir * 1f);
            });
        }

        if (trafficLightOppositePoint != null)
        {
            Vector3 dir = (trafficLightOppositePoint.transform.position - transform.position).normalized;
            Gizmos.DrawRay(transform.position + Vector3.up * 0.15f, dir * 1f);
        }
    }



    public ScDefine.ScPathPointNextAction GetNextRandomAction(params ScDefine.ScPathPointNextAction[] exceptActions)
    {
        List<ScDefine.ScPathPointNextAction> exceptList;

        if (exceptActions?.Length > 0)
            exceptList = exceptActions.ToList();
        else
            exceptList = new();

        exceptList.Add(ScDefine.ScPathPointNextAction.DoBadThing);

        List<ScDefine.ScPathPointNextAction> actions = nextPathType.Except(exceptList).ToList();

        return actions[Random.Range(0, actions.Count)];
    }
    
    public ScPathPoint GetNextRandomPathPoint(ScPathPoint previousPathPoint)
    {
        List<ScPathPoint> newList = nearPoints.ToList();
        
        if (previousPathPoint != null)
            newList.Remove(previousPathPoint);
        
        int randValue = Random.Range(0, newList.Count);
        return newList[randValue];
    }

    public bool CanDoBadThing()
    {
        return nextPathType.Contains(ScDefine.ScPathPointNextAction.DoBadThing);
    }
}
