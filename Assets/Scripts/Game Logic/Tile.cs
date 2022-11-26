using System;
using UnityEngine;

public class Tile
{
    public int X { get =>(int) (m_position.x); }
    public int Y { get => (int)(m_position.y); }
    public int Cost { get; set; }
    public int Distance { get; set; }
    public int CostDistance => Cost + Distance;
    public Tile Parent { get; set; }
    public bool IsOccupied { get; set; }
    public Vector3 Position { get => m_position; set => m_position = value; }
    private Vector3 m_position;
    public Tile(Vector3 position)
    {
        m_position = position;
    }
    
    public Tile()
    {
    }

    //The distance is essentially the estimated distance, ignoring walls to our target. 
    //So how many tiles left and right, up and down, ignoring walls, to get there. 
    public void SetDistance(int targetX, int targetY)
    {
        this.Distance = Math.Abs(targetX - X)/TileManager.Instance.SideLength + Math.Abs(targetY - Y)/TileManager.Instance.SideLength;
    }
}