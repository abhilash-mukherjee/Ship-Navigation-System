using System.Collections.Generic;
using UnityEngine;

public class ParkingElementsManager : MonoBehaviour
{
    [SerializeField] List<GameObject> parkingObjects;
    private void Start()
    {
        foreach (var obj in parkingObjects) obj.SetActive(false);
    }
    public void OnParkingAreaReached()
    {
        foreach (var obj in parkingObjects) obj.SetActive(true);
    }
    public void OnParkingAreaLeft()
    {
        foreach (var obj in parkingObjects) obj.SetActive(false);
    }
}
