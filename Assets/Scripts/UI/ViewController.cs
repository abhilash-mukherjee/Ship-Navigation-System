using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewController : MonoBehaviour
{
    [SerializeField]
    private Transform OperatorXROrigin, SailorXROrigin;
    [SerializeField] private GameObject shipInut, operatorInput;
    [SerializeField]
    private OVRManager oVRManager;
    private bool m_toggler = true;
    private void Start()
    {
        oVRManager.gameObject.transform.SetParent(SailorXROrigin);
        oVRManager.gameObject.transform.localPosition = Vector3.zero;
        oVRManager.gameObject.transform.localRotation = Quaternion.identity;
        shipInut.SetActive(true);
        operatorInput.SetActive(false);
        m_toggler = true; 
    }
    public void OnViewToggled() 
    {
        oVRManager.gameObject.transform.SetParent(m_toggler == true  ? OperatorXROrigin : SailorXROrigin);
        shipInut.SetActive(!m_toggler);
        operatorInput.SetActive(m_toggler);
        m_toggler = !m_toggler;
        oVRManager.gameObject.transform.localPosition = Vector3.zero;
        oVRManager.gameObject.transform.localRotation = Quaternion.identity;
    }
}
