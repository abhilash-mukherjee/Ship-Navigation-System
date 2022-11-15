using System;
using System.Collections.Generic;
using UnityEngine;

public class ParkingAreaMapElement : MonoBehaviour
{

    [SerializeField]private ElementID m_parkingID;

    public ElementID ParkingID { get => m_parkingID; set => m_parkingID = value; }

   

}
