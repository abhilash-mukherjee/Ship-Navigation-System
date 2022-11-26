using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using System.Linq;
using System;

public class ShipPathManager : MonoBehaviour
{
    public delegate void TargetParkingUpdateHandler(ShipPathManager pathManager, Transform targetParkingTransform);
    public static event TargetParkingUpdateHandler OnTargetParkingAreaUpdated;
    public delegate void PathUpdateRequestHandler(Vector3 start, Vector3 target);
    public static event PathUpdateRequestHandler OnPathRequestSent;

    [SerializeField]
    private PathMarker pathMarker;
    [SerializeField]
    private GameEvent OnMilestoneUpdated;

    [SerializeField]
    private float markerGap;
    [SerializeField] private float yOffset = 5f;
    [SerializeField] private GameObject driverSeat;
    [SerializeField] private List<ParkingToPath> paths;
    [SerializeField] private LayerMask parkingAreaLayerMask;
    [SerializeField] private float shipRadius;
    [SerializeField] private int maximumPathFindingTries;
    public Queue<PathMarker> pathMarkers;
    public List<PathMarker> pathMarkerList;
    public ElementID m_currentParkingAreaID;
    public ElementID CurrentParkingArea{ get => m_currentParkingAreaID; }
    public Vector3 NextMileStone { get => m_nextMileStone; }
    public GameObject DriverSeat { get => driverSeat; }

    private Vector3 m_nextMileStone;
    private GameObject m_pathObjectContainer, m_cachedGameObject;
    private TargetPath m_currentPath;
    private Transform m_currentParkingArea;

    private void OnEnable()
    {
        ParkingAreaSelector.OnAreaSelected += OnPathUpdated;
        WaitForParkingAllotment.OnParkingAreaSelected += OnPathUpdated;
        Grid.OnGridUpdated += StartSpawning;
    }
    private void OnDisable()
    {
        ParkingAreaSelector.OnAreaSelected -= OnPathUpdated;
        WaitForParkingAllotment.OnParkingAreaSelected -= OnPathUpdated;
        Grid.OnGridUpdated -= StartSpawning;
    }


    private void StartSpawning(List<Vector3> vertexList)
    {
        var path = new TargetPath(vertexList.ToArray());
        UpdateTargetPath(path);
        OnTargetParkingAreaUpdated?.Invoke(this, m_currentParkingArea);
    }

    public void OnPathUpdated(ElementID parkingAreaID)
    {
        StartCoroutine(PathUpdated(parkingAreaID));
        m_currentParkingAreaID = parkingAreaID;
    }

    IEnumerator PathUpdated(ElementID parkingAreaID)
    {
        if (m_pathObjectContainer != null) Destroy(m_pathObjectContainer);
        if (m_cachedGameObject == null) m_cachedGameObject = Instantiate(new GameObject());
        var pathElement = paths.Find(p => p.parkingAreaID == parkingAreaID);
        m_currentParkingArea = pathElement.parkingAreaTransform;
        //var vertices = new List<Vector3>();
        //vertices.Add(transform.position);
        //var otherVertices = GetPathVertices(transform.position, pathElement.parkingAreaTransform.position).ToList();
        //for(int i = 0; i <  otherVertices.Count; i++) vertices.Add(otherVertices[i]);
        //PrintList(vertices);
        OnPathRequestSent?.Invoke(transform.position, m_currentParkingArea.position);
        
        yield return null;
    }

    private void PrintList(List<Vector3> list)
    {
        string str = "";
        for(int i = 0;i < list.Count;i++)
        {
            str = str + $"{i} : {list[i]}, ";
        }
        Debug.Log(str);
    }

    //private Vector3[] GetPathVertices(Vector3 start, Vector3 end)
    //{
    //    List<Vector3> vertexList = new List<Vector3>();
    //    var obstacle = Obstacle.LookForObstacles(start, end, (start - end).normalized, shipRadius);
    //    // Cast character controller shape 10 meters forward to see if it is about to hit anything.
    //    if (obstacle != null)
    //    {
    //        Debug.Log("found obstacles");
    //        var distanceToObstacle = obstacle.GetObstacleDistance(start);
    //        Vector3 forward = (end - start).normalized;
    //        forward.y = 0;
    //        Vector3 currentForward = forward;
    //        var ang = Vector3.SignedAngle(transform.forward, (end - start).normalized, Vector3.up);
    //        Vector3 currentEnd = end;
    //        int randomDir = UnityEngine.Random.Range(0,2);
    //        var radians = (obstacle.CollisionRadius+ shipRadius) / distanceToObstacle;
    //        var thetaDegrees = (180 / 3.14f) * radians;
    //        randomDir = randomDir == 0 ? -1 : 1;
    //        for(int i = 1; i <= maximumPathFindingTries/2; i++)
    //        {
    //            currentForward = Quaternion.AngleAxis(i * thetaDegrees *randomDir , transform.up) * forward;
    //            currentEnd = start + currentForward * distanceToObstacle;
    //            var new_obstacle = Obstacle.LookForObstacles(start, currentEnd, currentForward, shipRadius);
    //            if (!new_obstacle)
    //            {
    //                var new_start = currentEnd;
    //                vertexList.Add(new_start);
    //                return vertexList.Concat(GetPathVertices(new_start, end).ToList()).ToArray();
    //            }
    //            randomDir *= -1;
    //            currentForward = Quaternion.AngleAxis(i * randomDir* thetaDegrees, transform.up) * forward;
    //            currentEnd = start + currentForward * distanceToObstacle;
    //            new_obstacle = Obstacle.LookForObstacles(start, currentEnd, currentForward, shipRadius);
    //            if (!new_obstacle)
    //            {
    //                var new_start = currentEnd;
    //                vertexList.Add(new_start);
    //                return vertexList.Concat(GetPathVertices(new_start, end).ToList()).ToArray();
    //            }


    //        }
            
    //        Debug.LogError("Couldn't find path");
    //        vertexList.Add(currentEnd);
    //        return vertexList.Concat(GetPathVertices(currentEnd, end).ToList()).ToArray();
    //    }
    //    else
    //    {
    //        vertexList.Add(end);
    //    }
    //    return vertexList.ToArray();
    //}

    

    private void UpdateTargetPath(TargetPath path)
    {
        m_currentPath = path;
        m_pathObjectContainer = SpawnObjects(path);
        UpdateMilestone(path);
    }

    private GameObject SpawnObjects(TargetPath path)
    {
        pathMarkers = new Queue<PathMarker>();
        float dist = 0;
        var pathObjectContainer = Instantiate(new GameObject());
        pathObjectContainer.name = "Target Path";
        pathObjectContainer.transform.position = m_cachedGameObject.transform.position;
        while (dist < path.Length)
        {
            Vector3 point = path.GetPointAtDistance(dist);
            Quaternion rot = path.GetRotationAtDistance(dist);
            rot.x = 0f;
            rot.z = 0f;
            var obj = Instantiate(pathMarker.gameObject, point, rot);
            var marker = obj.GetComponent<PathMarker>();
            marker.PathManager = this;
            marker.ParkingArea = m_currentParkingArea;
            pathMarkers.Enqueue(marker);
            marker.transform.SetParent(pathObjectContainer.transform);
            dist += markerGap;
        }
        var positionController = pathObjectContainer.AddComponent<PathPositionController>();
        positionController.YOffset = yOffset;
        positionController.ShipTransform = transform;
        return pathObjectContainer;
    }
    public void OnNewMarkerReached()
    {
        if (pathMarkers.Count <= 1)
            return;
        pathMarkers.Dequeue();
        if(m_currentPath != null) UpdateMilestone(m_currentPath);
    }
    public void UpdateMilestone(TargetPath path)
    {
        if (pathMarkers.Count == 0)
        {
            Debug.Log("ZERO");
            SpawnObjects(path);
            return;
        }
        m_nextMileStone = GetNextMilestone();
        OnMilestoneUpdated.Raise();
    }

    private Vector3 GetNextMilestone()
    {
        
        var currentMilestone = pathMarkers.Peek();
        while (currentMilestone == null)
        {
            currentMilestone = pathMarkers.Dequeue();
        }
        return currentMilestone.gameObject.transform.position;

    }
    [System.Serializable]
    public class ParkingToPath
    {
        public ElementID parkingAreaID;
        public Transform parkingAreaTransform;
    }
}


public class PathCreationHelper
{
    public static Vector3[] Vector3Array(List<Vector3> vectorList)
    {
        if (vectorList.Count <= 0) return new Vector3[0];
        Vector3[] vectorArr = new Vector3[vectorList.Count];
        for(int i = 0; i<vectorList.Count;i++)
        {
            vectorArr[i] = vectorList[i];
        }
        return vectorArr;
    }
}

public class TargetPath
{
    private readonly Vector3 start;
    private readonly Vector3 end;
    private readonly Vector3[] vertices;
    public float Length
    {
        get
        {
            return GetePathLength();
        }
    }

    private float GetePathLength()
    {
        float length = 0;
        for(int i = 0; i < vertices.Length -1; i++)
        {
            length += (vertices[i] - vertices[i+1]).magnitude;
        }
        return length;
    }

    public TargetPath(Vector3 start, Vector3 end)
    {
        this.start = start;
        this.end = end;
        this.vertices = new Vector3[2];
        vertices[0] = start;
        vertices[1] = end;
    }
    public TargetPath(Vector3[] vertices)
    {
        this.start = vertices[0];
        this.end = vertices[vertices.Length-1];
        this.vertices = vertices;
    }

    public Vector3 GetPointAtDistance(float dist)
    {
        float totalDist = 0;
        for(int i = 0; i < vertices.Length - 1; i++)
        {
            
            var segmentLength = (vertices[i + 1] - vertices[i]).magnitude;
            totalDist += segmentLength;
            if(totalDist > dist)
            {
                var remaining = totalDist - dist;
                var backDir = (vertices[i]-vertices[i+1]).normalized;
                var point = vertices[i + 1] + remaining * backDir;
                return point;
            }
        }
        var point1  = vertices[^1];
        Debug.Log("Target vertex" + point1);
        return point1;
    }
    public Quaternion GetRotationAtDistance(float dist)
    {
        var segment = GetSegmentAtDistance(dist);
        return Quaternion.LookRotation(segment[1]- segment[0]);
    }

    private Vector3[] GetSegmentAtDistance(float dist)
    {
        var arr = new Vector3[2];
        float totalDist = 0;
        for (int i = 0; i < vertices.Length - 1; i++)
        {

            var segmentLength = (vertices[i + 1] - vertices[i]).magnitude;
            totalDist += segmentLength;
            if (totalDist > dist)
            {
                arr[0] = vertices[i];
                arr[1] = vertices[i+1];
                return arr;
            }
        }
        arr[0] = vertices[0];
        arr[1] = vertices[1];
        return arr;
    }
}