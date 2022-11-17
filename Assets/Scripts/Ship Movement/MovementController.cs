using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    //visible Properties
    [SerializeField] private SteerInput steerInput;
    [SerializeField] private LinearInput linearInput;
    [SerializeField] private ShipModeName navigatingMode, parkingMode, dockedMode;
    [SerializeField] private ShipModeNameContainer currentMode;

    [SerializeField] private TMPro.TextMeshProUGUI directionText;
    [SerializeField] private float nonSteerAngularDrag = 2f;
    [SerializeField] private float steerAngularDrag = 0.05f;
    public Transform Motor;
    public float SteerPower = 500f;
    public float Power = 5f;
    public float MaxSpeed = 10f;
    public float Drag = 0.1f;

    //used Components
    protected Rigidbody Rigidbody;
    protected Quaternion StartRotation;
    protected ParticleSystem ParticleSystem;

    //internal Properties
    protected Vector3 CamVel;


    public void Awake()
    {
        ParticleSystem = GetComponentInChildren<ParticleSystem>();
        Rigidbody = GetComponent<Rigidbody>();
        StartRotation = Motor.localRotation;
    }

    public void FixedUpdate()
    {
        if (currentMode.ModeName == dockedMode)
        {
            directionText.text = "Ship is docked. Press B to undock";
            return;
        }
        else directionText.text = "";
       //default direction
       var forceDirection = transform.forward;

        Rigidbody.angularDrag = steerInput.Value == 0 ? nonSteerAngularDrag : steerAngularDrag;
        //Rotational Force
        
        var rotForce = steerInput.Value * transform.right * SteerPower / 100f ;
        rotForce.y = 0f;
        Rigidbody.AddForceAtPosition(rotForce, Motor.position);

        //compute vectors
        var forward = Vector3.Scale(new Vector3(1, 0, 1), transform.forward);
        var targetVel = Vector3.zero;

        PhysicsHelper.ApplyForceToReachVelocity(Rigidbody, forward * MaxSpeed *linearInput.Value, Power);

        //moving forward
        var movingForward = Vector3.Cross(transform.forward, Rigidbody.velocity).y < 0;
        //move in direction
        Rigidbody.velocity = Quaternion.AngleAxis(Vector3.SignedAngle(Rigidbody.velocity, (movingForward ? 1f : 0f) * transform.forward, Vector3.up) * Drag, Vector3.up) * Rigidbody.velocity;

    }


}




