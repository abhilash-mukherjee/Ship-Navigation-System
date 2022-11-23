using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathMarker : MonoBehaviour
{
    [SerializeField] private float distanceCorrection = -24f, cosAngleCorrectionFactor = 0.5f;
    protected ShipPathManager m_pathManager;
    private Transform m_parkingArea;
    public ShipPathManager PathManager
    {
        get
        {
            return m_pathManager;
        }
        set
        {
            m_pathManager = value;
        }
    }

    public Transform ParkingArea { get => m_parkingArea; set => m_parkingArea = value; }

    void Update()
    {
        var dir = m_parkingArea.position - transform.position;
        if (GetDistanceAlongDirection(transform.position, m_pathManager.transform.position, dir) < distanceCorrection
            &&
            Vector3.Dot((m_parkingArea.position - transform.position).normalized, (m_parkingArea.position - m_pathManager.transform.position).normalized) > cosAngleCorrectionFactor
            )
        {
            OnShipHitMarker();

        }
    }

    float GetDistanceAlongDirection(Vector3 to, Vector3 from, Vector3 dir)
    {
        dir = dir.normalized;
        var a = to - from;
        return Vector3.Dot(a, dir);
    }
    // private void OnTriggerEnter(Collider other)
    // {
    //     Debug.Log(Spawnner);
    //     Debug.Log(Spawnner.gameObject);

    //     if(other.gameObject == Spawnner.gameObject)
    //     {
    //         OnShipHitMarker();
    //     }
    // }

    protected virtual void OnShipHitMarker()
    {
        PathManager.OnNewMarkerReached();
        Destroy(gameObject);
    }
}
