using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (FloatObjectScript))]
public class BoatControllerScript : MonoBehaviour
{
    public Vector3 COM;
    [Space (15)]
    public float speed = 1.0f;
    public float steerSpeed = 1.0f;
    public float movementThresold = 10.0f;
    Transform m_COM;
    float verticalInput;
    float movementFactor;
    float horizontalInput;
    float steerFactor;    
// Start is called before the first frame update
 
    // Update is called once per frame
    void Update()
    {
        Balance ();
	Movement ();
	Steer ();
    }
	
    void Balance () {
		if (!m_COM) {
			m_COM = new GameObject ("COM").transform;
			m_COM.SetParent (transform);
		}

		m_COM.position = COM;
		GetComponent<Rigidbody> ().centerOfMass = m_COM.position;
	}

    void Movement () {
		verticalInput = Input.GetAxis ("Vertical");
		movementFactor = Mathf.Lerp (movementFactor, verticalInput, Time.deltaTime / movementThresold);
		transform.Translate (movementFactor * speed, 0.0f, 0.0f);
	}

     void Steer () {
		horizontalInput = Input.GetAxis ("Horizontal");
		steerFactor = Mathf.Lerp (steerFactor, horizontalInput * verticalInput, Time.deltaTime / movementThresold);
		transform.Rotate (0.0f,0.0f, steerFactor * steerSpeed);
	}
}
