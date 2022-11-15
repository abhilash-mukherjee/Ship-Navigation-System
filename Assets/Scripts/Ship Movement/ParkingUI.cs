using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkingUI : MonoBehaviour
{
    [SerializeField] private GameObject glow;
    [SerializeField] private ElementID ID;
    static ParkingUI selectedParking;
    private void OnEnable()
    {
        ParkingAreaSelector.OnAreaSelected += AddGlow;
    }

    private void AddGlow(ElementID ID)
    {
        if (ID != this.ID) return;
        if(selectedParking != null) selectedParking.RemoveGlow();
        glow.SetActive(true);
        selectedParking = this;
    }

    private void RemoveGlow()
    {
        glow.SetActive(false);
    }

    private void OnDisable()
    {
        ParkingAreaSelector.OnAreaSelected -= AddGlow;
        
    }
}
