using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField] private GameObject glow;
    public void UpdateObstacles()
    {
        TileManager.TileMap.ForEach(t => t.ForEach(tile => tile.IsOccupied = false));
        Debug.Log("Length of Obstacle List : " + PathObstacle.Obstacles.Count);
        foreach (var obs in PathObstacle.Obstacles)
        {
            if (!obs.IsObstacle()) continue;
            var tile = obs.GetTile();
            tile.IsOccupied = true;
            //Instantiate(glow, tile.Position, Quaternion.identity);
        }
    }
}
