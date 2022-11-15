using UnityEngine;

public class ShipModeManager : MonoBehaviour
{
    [SerializeField] private BoolVariable IsShipDockable;
    [SerializeField] private ShipModeName navigatingMode, parkingMode, dockedMode;
    [SerializeField] private ShipModeNameContainer currentMode;



    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        
    }
    public void OnModeChanged()
    {
        // mode change logic
    }
    public void OnDockToggled()
    {
        if (currentMode.ModeName == dockedMode) currentMode.ModeName = parkingMode;
        else if (currentMode.ModeName == parkingMode && IsShipDockable.Value == true) currentMode.ModeName = dockedMode;
    }
    private void Awake()
    {
        currentMode.ModeName = navigatingMode;
    }
}