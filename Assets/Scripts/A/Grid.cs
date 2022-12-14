using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour
{
	public delegate void GridUpdateHandler(List<Vector3> vertexList);
	public static GridUpdateHandler OnGridUpdated;
	[SerializeField] private CustomPathFinding pathFinder;
	[SerializeField] private GameObject glow;
	public LayerMask unwalkableMask;
	public Vector2 gridWorldSize;
	public float nodeRadius;
	Node[,] grid;

	float nodeDiameter;
	int gridSizeX, gridSizeY;

	void Awake()
	{
		nodeDiameter = nodeRadius * 2;
		gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
		gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
	}
    private void OnEnable()
    {
		ShipPathManager.OnPathRequestSent += UpdateGrid; 
    }
	private void OnDisable()
    {
		ShipPathManager.OnPathRequestSent -= UpdateGrid; 
    }
    public void UpdateGrid(Vector3 start, Vector3 target)
    {
		StartCoroutine(CreateGrid(start,target));
		var vertexList = new List<Vector3>();
		for(int i = 0; i <path.Count;i++)
        {
			vertexList.Add(path[i].worldPosition);
			//Instantiate(glow, vertexList[i], Quaternion.identity);
        }
		OnGridUpdated?.Invoke(vertexList);
    }
	IEnumerator CreateGrid(Vector3 start, Vector3 target)
	{
		grid = new Node[gridSizeX, gridSizeY];
		Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;

		for (int x = 0; x < gridSizeX; x++)
		{
			for (int y = 0; y < gridSizeY; y++)
			{
				Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
				bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask));
				grid[x, y] = new Node(walkable, worldPoint, x, y);
			}
		}
		pathFinder.UpdatePath(start, target);
		yield return null;
	}

	public List<Node> GetNeighbours(Node node)
	{
		List<Node> neighbours = new List<Node>();

		for (int x = -1; x <= 1; x++)
		{
			for (int y = -1; y <= 1; y++)
			{
				if (x == 0 && y == 0)
					continue;

				int checkX = node.gridX + x;
				int checkY = node.gridY + y;

				if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
				{
					neighbours.Add(grid[checkX, checkY]);
				}
			}
		}

		return neighbours;
	}


	public Node NodeFromWorldPoint(Vector3 worldPosition)
	{
		float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
		float percentY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;
		percentX = Mathf.Clamp01(percentX);
		percentY = Mathf.Clamp01(percentY);

		int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
		int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
		return grid[x, y];
	}

	public List<Node> path;
	//void OnDrawGizmos()
	//{
	//	Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));

	//	if (grid != null)
	//	{
	//		foreach (Node n in grid)
	//		{
	//			Gizmos.color = (n.walkable) ? Color.white : Color.red;
	//			if (path != null)
	//				if (path.Contains(n))
	//					Gizmos.color = Color.black;
	//			Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - .1f));
	//		}
	//	}
	//}
}

public class Node
{
	public bool walkable;
	public Vector3 worldPosition;
	public int gridX;
	public int gridY;

	public int gCost;
	public int hCost;
	public Node parent;

	public Node(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY)
	{
		walkable = _walkable;
		worldPosition = _worldPos;
		gridX = _gridX;
		gridY = _gridY;
	}

	public int fCost
	{
		get
		{
			return gCost + hCost;
		}
	}
}