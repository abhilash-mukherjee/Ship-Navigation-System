using TMPro;
using UnityEngine;

public class ShipParkingManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI directionText;
    [SerializeField]
    private float parkingPositionError = 2f, parkingOrientationError = 5f, maxParkingAreaEnteringAngle = 20;
    [SerializeField] private ShipModeName navigatingMode, parkingMode, dockedMode;
    [SerializeField] private ShipModeNameContainer currentMode;
    [SerializeField] private ShipPathManager pathManager;

    private Transform m_targetParkingSlot;

    [SerializeField]
    private Transform shipCOM;
    [SerializeField]
    private BoolVariable IsShipDockable;
    private bool m_hasReachedPosition, m_hasReachedOrientation;
    private string m_instructions;
    private void OnEnable()
    {
        ShipPathManager.OnTargetParkingAreaUpdated += UpdateParkingTransform;
    }
    private void OnDisable()
    {
        ShipPathManager.OnTargetParkingAreaUpdated -= UpdateParkingTransform;
    }

    private void UpdateParkingTransform(ShipPathManager pathManager, Transform targetParkingTransform)
    {
        if (pathManager != this.pathManager) return;
        m_targetParkingSlot = targetParkingTransform;

    }
    private void Update()
    {
        if (currentMode.ModeName != parkingMode || m_targetParkingSlot == null) return;
        var m_tempVec = (shipCOM.position - m_targetParkingSlot.position);
        m_tempVec.y = 0;
        m_hasReachedPosition = m_tempVec.magnitude < parkingPositionError;
        m_hasReachedOrientation = Mathf.Abs(Vector3.SignedAngle(shipCOM.forward, m_targetParkingSlot.forward, Vector3.up)) < parkingOrientationError ;
        m_instructions = "Slow Down. You are about to dock the ship.";
        if(m_hasReachedOrientation && m_hasReachedPosition)
        {
            m_instructions = "Perfect. You have parked successfully. Now, dock the ship";
            IsShipDockable.Value = true;
            directionText.text = m_instructions;
            return;
        }

        else if (!m_hasReachedOrientation && m_hasReachedPosition)
        {
            var angle = Vector3.SignedAngle(shipCOM.forward, m_targetParkingSlot.forward, Vector3.up);
            var dirStr = angle < 0 ? $"Just turn {-angle} degrees left" : $"Just turn {angle} degrees right";
            m_instructions = "Great! You have reached the right spot. Now Just adjust your orientation. " + dirStr; 

        }
        else
        {
            var angle = Vector3.SignedAngle(transform.forward, m_targetParkingSlot.forward, Vector3.up);
            var translationStr = (m_targetParkingSlot.position - shipCOM.position).z * shipCOM.forward.z> 0 ? " Go forward" : " Go Backward";
            string dirStr = "";
            if (angle < maxParkingAreaEnteringAngle)
            {
                dirStr = angle < 0 ? $"Turn {-angle} degrees left" : $"Turn {angle} degrees right";
                m_instructions += " " + dirStr + translationStr;
            }
            else
            {
                m_instructions = "You have entered the docking area with wrong orientation. Parking may be difficult. Dont rotate your ship. Just" + translationStr;
            }
        }
        directionText.text = m_instructions;
        IsShipDockable.Value = false;
    }

   public void OnDocked()
    {
        m_instructions = "Ship Docked";
        directionText.text = m_instructions;
    }
}

