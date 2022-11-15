using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialPathMarker : PathMarker
{
     private void OnTriggerEnter(Collider other)
    {
        Debug.Log(PathManager);
        Debug.Log(PathManager.gameObject);

        if(other.gameObject == PathManager.gameObject)
        {
            OnShipHitMarker();
        }
    }
    protected override void OnShipHitMarker(){
            Debug.Log("Special Marker Reached");
        PathManager.OnNewMarkerReached();
            Destroy(gameObject);
    }
}
