using System;
using UnityEngine;

public class PathArrow : MonoBehaviour
{
    [SerializeField] private ShipPathManager pathManager;
    [SerializeField] private GameObject driverSeat;
    [SerializeField] private GameObject arrowModel;
    [SerializeField] private ShipModeName navigatingMode, parkingMode, dockedMode;
    [SerializeField] private ShipModeNameContainer currentMode;

    [SerializeField] private float rotationSpeed = 1f;
    // Start is called before the first frame update
    private Vector3 m_nextMilestone;
    private Transform m_targetParkingArea;


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
        m_targetParkingArea = targetParkingTransform;
        
    }

    private void Update()
    {
        if (currentMode.ModeName == navigatingMode)
        {
            var milstone = m_nextMilestone;
            milstone.y = arrowModel.transform.position.y;
            var dir = (milstone - arrowModel.transform.position).normalized;
            var rotGoal = Quaternion.LookRotation(dir);
            var ang = Quaternion.Slerp(transform.rotation,rotGoal, Time.deltaTime * rotationSpeed);
            arrowModel.transform.rotation = (ang);
        }
        if (currentMode.ModeName == parkingMode && m_targetParkingArea != null)
        {
            var targetRot = Quaternion.LookRotation(m_targetParkingArea.forward);
            var ang = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * rotationSpeed);
            arrowModel.transform.rotation = (ang);
            return; 
        }

    }
    public void OnDirectionUpdated()
    {
        m_nextMilestone = pathManager.NextMileStone;
    }
}
