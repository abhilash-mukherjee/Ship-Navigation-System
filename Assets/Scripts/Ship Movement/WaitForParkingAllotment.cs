using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForParkingAllotment : MonoBehaviour
{
    [SerializeField] private GameObject directionText, arrow, initialText,waitingText;
    [SerializeField] private float waitTime = 5f;
    [SerializeField] private AudioSource parkingAreaRequest, parkingAreaAlloted;
    [SerializeField] private List<ElementID> parkingIDs;
    private bool m_isPathAllotted = false;

    public delegate void ParkingAreaSelectHandler(ElementID parkingID);
    public static event ParkingAreaSelectHandler OnParkingAreaSelected;
    private void Start()
    {
        directionText.SetActive(false);
        arrow.SetActive(false);
        initialText.SetActive(true);
        waitingText.SetActive(false);
    }

    public void OnParkingAreaAllotmentRequested()
    {
        if (m_isPathAllotted) return;
        directionText.SetActive(false);
        arrow.SetActive(false);
        initialText.SetActive(false);
        waitingText.SetActive(true);
        parkingAreaRequest.Play();
        StartCoroutine(AllotParkingArea(5));
    }

    IEnumerator AllotParkingArea(float time)
    {
        yield return new WaitForSeconds(time);
        var index = Random.Range(0, parkingIDs.Count);
        OnParkingAreaSelected?.Invoke(parkingIDs[index]);
        OnParkingAreaAlloted();
        m_isPathAllotted = true;

    }
    public void OnParkingAreaAlloted()
    {
        directionText.SetActive(true);
        arrow.SetActive(true);
        initialText.SetActive(false);
        waitingText.SetActive(false);
        parkingAreaAlloted.Play();

    }
}