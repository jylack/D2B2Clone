using System.Collections.Generic;
using UnityEngine;

public class ScPathPoint : ScObjectBase
{
    [Header("TrafficLight")]
    [SerializeField] private ScTrafficLight trafficLight;
    [SerializeField] private ScPathPoint trafficLightOppositePoint;
    [Header("Etc")]
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



    public ScPathPoint GetNextPathPoint()
    {
        return nearPoints[0];
    }
}
