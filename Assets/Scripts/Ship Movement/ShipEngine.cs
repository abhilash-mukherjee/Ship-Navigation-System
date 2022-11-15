using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipEngine : MonoBehaviour
{
    ////Drags
    //public Transform waterJetTransform;

    ////How fast should the engine accelerate?
    //public float powerFactor;

    ////What's the boat's maximum engine power?
    //public float maxPower;

    ////The boat's current engine power is public for debuggingrt
    
    //[SerializeField]private float currentJetPower;

    //private float m_thrustFromWaterJet = 0f;

    //private Rigidbody m_boatRB;

    //private float m_WaterJetRotation_Y = 0f;

    //BoatController m_boatController;

    //void Start()
    //{
    //    m_boatRB = GetComponent<Rigidbody>();

    //    m_boatController = GetComponent<BoatController>();
    //}


    //void Update()
    //{
    //    UserInput();
    //}

    //void FixedUpdate()
    //{
    //    UpdateWaterJet();
    //}

    //void UserInput()
    //{
    //    //Forward / reverse
    //    if (Input.GetKey(KeyCode.W))
    //    {
    //        if (m_boatController.CurrentSpeed < 50f && currentJetPower < maxPower)
    //        {
    //            currentJetPower += 1f * powerFactor;
    //        }
    //    }
    //    else
    //    {
    //        currentJetPower = 0f;
    //    }

    //    //Steer left
    //    if (Input.GetKey(KeyCode.A))
    //    {
    //        m_WaterJetRotation_Y = waterJetTransform.localEulerAngles.y + 2f;

    //        if (m_WaterJetRotation_Y > 30f && m_WaterJetRotation_Y < 270f)
    //        {
    //            m_WaterJetRotation_Y = 30f;
    //        }

    //        Vector3 newRotation = new Vector3(0f, m_WaterJetRotation_Y, 0f);

    //        waterJetTransform.localEulerAngles = newRotation;
    //    }
    //    //Steer right
    //    else if (Input.GetKey(KeyCode.D))
    //    {
    //        m_WaterJetRotation_Y = waterJetTransform.localEulerAngles.y - 2f;

    //        if (m_WaterJetRotation_Y < 330f && m_WaterJetRotation_Y > 90f)
    //        {
    //            m_WaterJetRotation_Y = 330f;
    //        }

    //        Vector3 newRotation = new Vector3(0f, m_WaterJetRotation_Y, 0f);

    //        waterJetTransform.localEulerAngles = newRotation;
    //    }
    //}

    //void UpdateWaterJet()
    //{
    //    //Debug.Log(boatController.CurrentSpeed);

    //    Vector3 forceToAdd = -waterJetTransform.forward * currentJetPower;

    //    //Only add the force if the engine is below sea level
    //    float waveYPos = WaterController.current.GetWaveYPos(waterJetTransform.position, Time.time);

    //    if (waterJetTransform.position.y < waveYPos)
    //    {
    //        m_boatRB.AddForceAtPosition(forceToAdd, waterJetTransform.position);
    //    }
    //    else
    //    {
    //        m_boatRB.AddForceAtPosition(Vector3.zero, waterJetTransform.position);
    //    }
    //}
}
