using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buyouncy : MonoBehaviour
{
    [SerializeField] private Transform centreOfBuyoncy, watterCentre;
    [SerializeField] private Vector3 waterCentreOffset;
    [SerializeField] private float buyouncyFactor, impulseMultiplier = 1, timeMin = 10f, timeMax = 15f;
    [SerializeField] Rigidbody RBD;
    private void Start()
    {
        StartCoroutine(ApplyImpulse(Random.Range(timeMin, timeMax)));
    }
    IEnumerator ApplyImpulse(float time)
    {
        yield return new WaitForSeconds(time);
        RBD.AddForce(impulseMultiplier * Vector3.up * Random.Range(-1,1f), ForceMode.Impulse);
        StartCoroutine(ApplyImpulse(Random.Range(timeMin,timeMax)));
    }
    private void FixedUpdate()
    {
        var delY = (watterCentre.position + waterCentreOffset) - centreOfBuyoncy.position;
        delY.x = 0f;
        delY.z = 0f;
        RBD.AddForce(delY * buyouncyFactor);
    }
}
