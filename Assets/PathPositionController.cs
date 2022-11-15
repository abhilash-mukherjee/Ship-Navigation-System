using UnityEngine;

public class PathPositionController: MonoBehaviour
{
    private Transform m_shipTransform;
    private float m_YOffset;
    Vector3 m_newPos;

    public Transform ShipTransform {  set => m_shipTransform = value; }
    public float YOffset { set => m_YOffset = value; }

    private void Awake()
    {
        m_newPos = Vector3.zero;
    }
    private void Update()
    {
        m_newPos = transform.position;
        m_newPos.y = m_shipTransform.position.y + m_YOffset;
        
        transform.position = m_newPos  ;
    }
}