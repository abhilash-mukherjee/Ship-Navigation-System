using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAudio : MonoBehaviour
{
    [SerializeField] private AudioEvent horn;
    [SerializeField] private AudioSource engineSource, hornSource;
    [SerializeField] [Range(0, 1f)] private float minVol;
    [SerializeField] private float volumeDivider;
    [SerializeField] private Rigidbody shipRBD; 
    private void Start()
    {
        engineSource.Play();

    }
    public void RingHorn()
    {
        horn.Play(hornSource);
    }

    private void Update()
    {
        engineSource.volume = minVol + shipRBD.velocity.magnitude / volumeDivider;
    }
}
