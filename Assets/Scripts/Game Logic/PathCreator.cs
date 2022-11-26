using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathCreator : MonoBehaviour
{
    [SerializeField] private float pathY;
    [SerializeField] private GameObject glow;
    [SerializeField] private GameObject start, end;
    [SerializeField] private ObstacleManager obstacleManager;
    private int SideLength { get => TileManager.Instance.SideLength; }
    private int MinX { get => TileManager.Instance.MinX; }
    private int MinY { get => TileManager.Instance.MinY; }

    public void GeneratePath()
    {
        //obstacleManager.UpdateObstacles();
        CreatePath(start.transform, end.transform);
    }
    public void CreatePath(Transform start, Transform end)
    {

        var activeTiles = new List<Tile>();
        var visitedTiles = new List<Tile>();
        activeTiles.Add(TileManager.GetNearestTile(start));
        ShowPath(activeTiles);
        
    }

    private void ShowPath(List<Tile> tiles)
    {
        foreach (var t in tiles) Instantiate(glow, t.Position, Quaternion.identity);
    }

    private List<Tile> GetWalkableTiles(List<List<Tile>> tileMap, Tile currentTile, Tile targetTile)
    {
        var possibleTiles = new List<Tile>()
    {
        new Tile {Position = new Vector3( currentTile.X,pathY ,currentTile.Y - SideLength), Parent = currentTile, Cost = currentTile.Cost + SideLength },
        new Tile {Position = new Vector3( currentTile.X,pathY ,currentTile.Y + SideLength), Parent = currentTile, Cost = currentTile.Cost + SideLength},
        new Tile {Position = new Vector3( currentTile.X - SideLength,pathY ,currentTile.Y), Parent = currentTile, Cost = currentTile.Cost + SideLength },
        new Tile {Position = new Vector3( currentTile.X + SideLength,pathY ,currentTile.Y), Parent = currentTile, Cost = currentTile.Cost + SideLength },
    };

        possibleTiles.ForEach(tile => tile.SetDistance(targetTile.X, targetTile.Y));

        var maxX = TileManager.Instance.MaxX;
        var maxY = TileManager.Instance.MaxY;

        return possibleTiles
                .Where(tile => tile.X >= MinX && tile.X <= maxX)
                .Where(tile => tile.Y >= MinY && tile.Y <= maxY)
                .Where(tile => tileMap[(tile.X - MinX) / SideLength][(tile.Y - MinY) / SideLength].IsOccupied == false)
                .ToList();
    }
}
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
   