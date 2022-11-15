using System.Collections.Generic;
using UnityEngine;

public class ParkingAreaSelector : MonoBehaviour
{
    public delegate void ParkingAreaSelectionHandler(ElementID parkingAreaID);
    public static event ParkingAreaSelectionHandler OnAreaSelected;
    [SerializeField] private List<ParkingAreaMapElement> parkingAreaElements;
    private int m_parkingAreaIndex;
    private void Awake()
    {
        m_parkingAreaIndex = 0;
    }
    public void OnNewParkingAreaSelected()
    {
        if (parkingAreaElements.Count < 1) return;
        OnAreaSelected?.Invoke(parkingAreaElements[m_parkingAreaIndex].ParkingID);
        m_parkingAreaIndex = m_parkingAreaIndex + 1 >= parkingAreaElements.Count ? 0 : m_parkingAreaIndex + 1;
    }
}
