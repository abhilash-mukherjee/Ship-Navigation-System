using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PathInstructionDisplayer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI directionText;
    [SerializeField] private ShipPathManager pathManager;
    [SerializeField] private ShipModeName navigatingMode, parkingMode, dockedMode;
    [SerializeField] private ShipModeNameContainer currentMode;
    [SerializeField] private GameObject driverSeat;
    [SerializeField] private float distanceCorrection = -24f;

    // Start is called before the first frame update
    private Vector3 m_nextMilestone;
    private void Update()
    {
        if (currentMode.ModeName != navigatingMode) return;
        ShowNavInstructions();
    }


    private void ShowNavInstructions()
    {
        var angle = Vector3.SignedAngle(transform.forward, m_nextMilestone - driverSeat.transform.position , Vector3.up);
        var str = angle < 0 ? $"Turn {-angle} degrees left" : $"Turn {angle} degrees right";
        directionText.text = str;

    }
    public void OnDirectionUpdated()
    {
        Debug.Log("Direction updated");
        m_nextMilestone = pathManager.NextMileStone;
    }
}
 