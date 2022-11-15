using UnityEngine;

public class MapElementUI : MonoBehaviour
{
    [SerializeField] GameObject glow;
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