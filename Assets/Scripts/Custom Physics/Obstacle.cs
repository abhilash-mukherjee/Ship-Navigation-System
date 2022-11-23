using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Obstacle : MonoBehaviour
{
        [SerializeField] private float collisionRadius;

    public float CollisionRadius { get => collisionRadius; set => collisionRadius = value; }

    public static Obstacle LookForObstacles(Vector3 start, Vector3 end, Vector3 forward, float objectCollisionRadius)
    {
        var obstacles = FindObjectsOfType<Obstacle>().ToList();
        var obsData = new List<ObstacleData>();
        foreach(var obstacle in obstacles)
        {
            if(obstacle.IsObstacle( start,  end,  forward, objectCollisionRadius))
            {
                ObstacleData data;
                data.obstacle = obstacle;
                data.distance = obstacle.GetObstacleDistance(obstacle.transform.position);
                obsData.Add(data);
            }
        }
        if (obsData.Count == 0) return null;
        var minDist = obsData.Min(p => p.distance);
        return obsData.Find(p => p.distance == minDist).obstacle;
    }

    public float GetObstacleDistance(Vector3 from)
    {
        return (from - transform.position).magnitude;
    }

    public bool IsObstacle(Vector3 start, Vector3 end, Vector3 targetPathforward, float objectCollisionRadius)
    {
        var dist = GetObstacleDistance(start);
        if (dist > (start - end).magnitude)
        {
            return false;
        }
        else
        {
            var tgtPoint = start + (end-start).normalized * dist;
            if ((tgtPoint - transform.position).magnitude < collisionRadius + objectCollisionRadius)
            {

                return true;
            }
            return false;
        }
    }
    struct ObstacleData
    {
        public Obstacle obstacle;
        public float distance;
    }
}
