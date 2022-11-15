using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class PathFollower : MonoBehaviour
{
    [SerializeField]
    private PathCreator pathCreator;
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float distanceScaler = 2;
    private float m_distanceTravelled;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_distanceTravelled += speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(m_distanceTravelled);
          Quaternion rotation = Quaternion.LookRotation ( 
            pathCreator.path.GetPointAtDistance(m_distanceTravelled) - transform.position, transform.TransformDirection(Vector3.up)
            );
          transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);

    }
}
