using System.Collections.Generic;
using UnityEngine;

public class ParkingAreaCameraManager : MonoBehaviour
{
    [SerializeField] private List<Camera> parkingCameras;
    [SerializeField] private ParkingArea parkingArea;
    private void Awake()
    {
        foreach (var cam in parkingCameras) cam.gameObject.SetActive(false);

    }
    private void OnEnable()
    {
        ParkingArea.OnParkingEntered += OnParkingAreaReached;
        ParkingArea.OnParkingLeft += OnParkingAreaLeft;
    }
    private void OnDisable()
    {
        ParkingArea.OnParkingEntered -= OnParkingAreaReached;
        ParkingArea.OnParkingLeft -= OnParkingAreaLeft;
        
    }
    public void OnParkingAreaReached(ParkingArea parkingArea)
    {
        if (parkingArea != this.parkingArea) return;
        foreach (var cam in parkingCameras) cam.gameObject.SetActive(true);
    }
    
    public void OnParkingAreaLeft(ParkingArea parkingArea)
    {
        if (parkingArea != this.parkingArea) return;
        foreach (var cam in parkingCameras) cam.gameObject.SetActive(false);
    }
}