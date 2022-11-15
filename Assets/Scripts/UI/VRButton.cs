using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRButton : MonoBehaviour
{
    [SerializeField] GameEvent OnButtonPressed;
    private GameObject presser;
    private bool isPressed;
    private void Start()
    {
        isPressed = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (isPressed) return;
        presser = other.gameObject;
        isPressed = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject != presser) return;
        isPressed = false;
        OnButtonPressed.Raise();
    }
}
