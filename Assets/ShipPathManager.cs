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

    [SerializeField]
    private PathMarker pathMarker;
    [SerializeField]
    private GameEvent OnMilestoneUpdated;

    [SerializeField] 
    private float markerGap;
    [SerializeField] private float  yOffset = 5f;
    [SerializeField] private GameObject driverSeat;
    [SerializeField] private List<ParkingToPath> paths;
    public Queue<PathMarker> pathMarkers;
    public List<PathMarker> pathMarkerList;

    public Vector3 NextMileStone { get => m_nextMileStone; }
    public GameObject DriverSeat { get => driverSeat; }

    private Vector3 m_nextMileStone;
    private GameObject m_pathObjectContainer, m_cachedGameObject;
    private TargetPath m_currentPath;
    private Transform m_currentParkingArea;

    private void OnEnable()
    {
        ParkingAreaSelector.OnAreaSelected += OnPathŪpdated;
    }
    private void OnDisable()
    {
        ParkingAreaSelector.OnAreaSelected -= OnPathŪpdated;
    }

    private void OnPathŪpdated(ElementID parkingAreaID)
    {
        if (m_pathObjectContainer != null) Destroy(m_pathObjectContainer);
        if (m_cachedGameObject == null) m_cachedGameObject = Instantiate(new GameObject());
        var pathElement = paths.Find(p => p.parkingAreaID == parkingAreaID);
        m_currentParkingArea = pathElement.parkingAreaTransform;
        var path = new TargetPath(transform.position,pathElement.parkingAreaTransform.position);
        UpdateTargetPath(path);
        OnTargetParkingAreaUpdated?.Invoke(this,m_currentParkingArea);
    }

    private Vector3[] GetPathPoints(Vector3 position, Transform parkingAreaTransform)
    {
        var list = new List<Vector3>();
        list.Add(position);
        list.Add((position + parkingAreaTransform.position) / 2);
        list.Add(parkingAreaTransform.position);
        return PathCreationHelper.Vector3Array(list);
    }

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
        return pathMarkers.Peek().gameObject.transform.position;

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
    private readonly Vector3 forwardDir;
    public  float Length { get => Vector3.Magnitude(end - start); }
    public TargetPath(Vector3 start, Vector3 end)
    {
        this.start = start;
        this.end = end;
        forwardDir = (end - start).normalized;
    }

    public Vector3 GetPointAtDistance(float dist)
    {
        return start + dist * forwardDir;
    }
    public Quaternion GetRotationAtDistance(float dist)
    {
        return Quaternion.LookRotation(forwardDir);
    }
}