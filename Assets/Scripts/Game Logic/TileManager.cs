using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    [SerializeField] private int minX, maxX, minY, maxY,sideLength;
    [SerializeField] private float tileHeight;

    [SerializeField] private GameObject tilePrefab;
    public static List<List<Tile>> TileMap;
    public int len;
    public static TileManager Instance;

    public int MinX { get => minX; set => minX = value; }
    public int MaxX { get => maxX; set => maxX = value; }
    public int MinY { get => minY; set => minY = value; }
    public int MaxY { get => maxY; set => maxY = value; }
    public int SideLength { get => sideLength; set => sideLength = value; }

    // Start is called before the first frame update

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    void Start()
    {
        TileMap = new List<List<Tile>>();
        StartCoroutine( GenerateTiles());
        len = TileMap.Count;
    }
    public static Tile GetNearestTile(Transform obj)
    {
        var x = (int)obj.position.x;
        var y = (int)obj.position.z;
        int relX = x - Instance.MinX;
        int relY = y - Instance.MinY;
        int i = relX / Instance.SideLength;
        int j = relY / Instance.SideLength;
        var tile = TileMap[i][j];
        return tile;
    }
    IEnumerator GenerateTiles()
    {
        var x = minX;
        var y = minY;
        while(x <= maxX)
        {
            var tileColumn = new List<Tile>();
            while(y <= maxY)
            {
                var tile = new Tile(new Vector3(x, tileHeight, y));
                tileColumn.Add(tile);
                Instantiate(tilePrefab, tile.Position, Quaternion.identity);
                y += sideLength;
            }
            x += sideLength;
            y = minY;
            TileMap.Add(tileColumn);
        }
        yield return null;
    }
}
