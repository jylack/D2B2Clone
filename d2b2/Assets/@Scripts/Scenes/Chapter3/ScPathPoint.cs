using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

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



    public ScDefine.ScPathPointNextAction GetNextRandomAction()
    {
        return nextPathType[Random.Range(0, nextPathType.Length)];
    }

    public ScDefine.ScPathPointNextAction GetNextRandomAction(ScDefine.ScPathPointNextAction exceptAction)
    {
        List<ScDefine.ScPathPointNextAction> newActions = nextPathType.ToList();
        newActions.Remove(exceptAction);

        return newActions[Random.Range(0, newActions.Count)];
    }
    
    public ScPathPoint GetNextRandomPathPoint(ScPathPoint previousPathPoint)
    {
        List<ScPathPoint> newList = nearPoints.ToList();
        
        if (previousPathPoint != null)
            newList.Remove(previousPathPoint);
        
        int randValue = Random.Range(0, newList.Count);
        return newList[randValue];
    }
}
