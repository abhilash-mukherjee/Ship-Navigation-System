using System.Collections.Generic;
using UnityEngine;

public class PathObstacle : MonoBehaviour
{
    [SerializeField] private GameObject glow;
    public static List<PathObstacle> Obstacles;
    private void Start()
    {
        if(Obstacles == null)Obstacles = new List<PathObstacle>();
        Obstacles.Add(this);
    }

   
    public bool IsObstacle()
    {
        var x = (int)transform.position.x;
        var y = (int)transform.position.z;
        bool isInX = x >= TileManager.Instance.MinX && x <= TileManager.Instance.MaxX;
        bool isInY = y >= TileManager.Instance.MinY && y <= TileManager.Instance.MaxY;
        bool isObs = isInX && isInY;
        if (!isObs) Debug.Log(this.gameObject.name + " is not obstacle");
        return isObs;
    }

    public Tile GetTile()
    {
        var x = (int)transform.position.x;
        var y = (int)transform.position.z;
        int relX = x - TileManager.Instance.MinX;
        int relY = y - TileManager.Instance.MinY;
        int i = relX / TileManager.Instance.SideLength;
        int j = relY / TileManager.Instance.SideLength;
        var tile = TileManager.TileMap[i][j];
        return tile;
    }
   
}
