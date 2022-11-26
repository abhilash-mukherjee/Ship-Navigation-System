using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkingUI : MonoBehaviour
{
    [SerializeField] private GameObject active,inActive;
    [SerializeField] private ElementID ID;
    static ParkingUI selectedParking;
    private void OnEnable()
    {
        ParkingAreaSelector.OnAreaSelected += AddGlow;
        active.SetActive(false);
        inActive.SetActive(true);
    }

    private void AddGlow(ElementID ID)
    {
        if (ID != this.ID) return;
        if(selectedParking != null) selectedParking.RemoveGlow();
        active.SetActive(true);
        inActive.SetActive(false);
        selectedParking = this;
    }

    private void RemoveGlow()
    {
        active.SetActive(false);
        inActive.SetActive(true);
    }

    private void OnDisable()
    {
        ParkingAreaSelector.OnAreaSelected -= AddGlow;
        
    }
}
