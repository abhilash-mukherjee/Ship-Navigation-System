using System.Collections.Generic;
using UnityEngine;

public class MapElementUI : MonoBehaviour
{
    [SerializeField] GameObject glow;
    [SerializeField] private GameObject thisParkingArea;
    [SerializeField] List<GameObject> otherParkingAreaData;
    [SerializeField] private ElementID m_elementID;
    public static MapElementUI selectedElement;
    private void OnEnable()
    {
        ParkingAreaSelector.OnAreaSelected += UpdateGlow;
    }

    private void UpdateGlow(ElementID ID)
    {
        if (ID != m_elementID) return;
        if (selectedElement != null) selectedElement.RemoveGlow();
        glow.SetActive(true);
        thisParkingArea.SetActive(true);
        otherParkingAreaData.ForEach(p => p.SetActive(false));
        selectedElement = this;
    }

    public void RemoveGlow()
    {
        glow.SetActive(false);
    }

    private void OnDisable()
    {
        ParkingAreaSelector.OnAreaSelected -= UpdateGlow;
    }


}