using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatSpawnner : MonoBehaviour
{
    [SerializeField] private List<GameObject> boats;
    [SerializeField] private List<GameObject> boatData;
    [SerializeField] private float firstBoatEnterDelay=5f;
    int currentBoatIndex = 0;
    private void OnEnable()
    {
        StartCoroutine(ShowFirstBoat());
    }

    IEnumerator ShowFirstBoat()
    {
        yield return new WaitForSeconds(5f);
        ShowNextBoat();
    }
    public void ShowNextBoat()
    {
        if (currentBoatIndex >= boats.Count - 1) return;
        boats[currentBoatIndex].SetActive(true);
        boatData[currentBoatIndex].SetActive(true);
        if(currentBoatIndex != 0)
        {
            boats[currentBoatIndex].SetActive(false);
            boatData[currentBoatIndex].SetActive(false);
        }
    }
}

public class UIBoatMover : MonoBehaviour
{
    public float speed;
    private Transform initialTarget, parkingArea;
    private void OnEnable()
    {
        
    }
}
