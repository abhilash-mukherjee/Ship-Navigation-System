using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform target;

    public bool isCustomOffset;
    public Vector3 offset;

    [SerializeField]private float forceFactor, torqueFactor;
    [SerializeField] private float xzForceMultiplier = 1, yForceMultiplier = 0.1f, forcePower = 3, logMultiplier = 0.1f;
    [SerializeField] private Rigidbody rbd, targetRBD;
    private Queue<Vector3> storedPositions;
    private Queue<Vector3> storedForwardDirections;
    private void Start()
    {
        //storedPositions = new Queue<Vector3>();
        //storedForwardDirections = new Queue<Vector3>();
        // You can also specify your own offset from inspector
        // by making isCustomOffset bool to true
        if (!isCustomOffset)
        {
            offset = transform.position - target.position;
        }
    }

    private void FixedUpdate()
    {
        //storedPositions.Enqueue(target.transform.position);
        //storedForwardDirections.Enqueue(target.transform.forward);
        //if(storedForwardDirections.Count != storedPositions.Count)
        //{
        //    if (storedForwardDirections.Count > storedPositions.Count) storedForwardDirections.Dequeue();
        //    else storedPositions.Dequeue();
        //    return;
        //}
        //if(storedPositions.Count < followFrameGap)
        //{
        //    return;
        //}
        //if (storedPositions.Count > followFrameGap)
        //{
        //    storedPositions.Dequeue();
        //    storedForwardDirections.Dequeue();
        //    return;
        //}
        SmoothFollow(target.position,target.forward);
    }
     
    public void SmoothFollow(Vector3 _targetPos, Vector3 _targetForward)
    {
        Vector3 targetPos = _targetPos + offset;
        Vector3 torqueVector = _targetForward - transform.forward;
        Vector3 forceVector = (targetPos - transform.position);
        forceVector.x = forceVector.x * xzForceMultiplier;
        forceVector.z = forceVector.z * xzForceMultiplier;
        forceVector.y = forceVector.y * yForceMultiplier;
        torqueVector.y = 0;
        var vRel = rbd.velocity - targetRBD.velocity;
        vRel.Normalize();
        rbd.AddForce(forceFactor * forceVector.magnitude * -vRel);
        rbd.AddTorque(torqueVector * torqueFactor);
    }
}
 