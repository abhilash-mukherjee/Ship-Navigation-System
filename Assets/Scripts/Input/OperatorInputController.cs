using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class OperatorInputController : MonoBehaviour
{
    private OperatorInputActionMappings shipInputActions;
    private InputAction linearMovementAction, steerMovementAction;
    [SerializeField] private SteerInput steerInput;
    [SerializeField] private LinearInput linearInput;
    [SerializeField] GameEvent VRScreenToggled, viewToggled, parkingAreaSelected;
    private void Awake()
    {
        shipInputActions = new OperatorInputActionMappings();
    }
    private void OnEnable()
    {
        linearMovementAction = shipInputActions.Operator.LinearMovement;
        steerMovementAction = shipInputActions.Operator.Steer;
        linearMovementAction.Enable();
        steerMovementAction.Enable();
        shipInputActions.Operator.ToggleView.performed += OnViewToggled;
        shipInputActions.Operator.SelectParkingArea.performed += OnParkingAreaSelected;
        shipInputActions.Operator.ToogleVRScreen.performed += OnVRScreenToggled;
        shipInputActions.Operator.ToggleView.Enable();
        shipInputActions.Operator.ToogleVRScreen.Enable();
        shipInputActions.Operator.SelectParkingArea.Enable();
    }

    
    private void OnViewToggled(InputAction.CallbackContext obj)
    {
        Debug.Log("View toggled");
        viewToggled.Raise();
    }
    private void OnParkingAreaSelected(InputAction.CallbackContext obj)
    {
        Debug.Log("parkingAreaSelected");
        parkingAreaSelected.Raise();
    }

    private void OnDisable()
    {
        linearMovementAction.Disable();
        steerMovementAction.Disable();
        shipInputActions.Operator.ToggleView.performed -= OnViewToggled;
        shipInputActions.Operator.ToogleVRScreen.performed -= OnVRScreenToggled;
        shipInputActions.Operator.SelectParkingArea.performed -= OnParkingAreaSelected;
        shipInputActions.Operator.ToggleView.Disable();
        shipInputActions.Operator.ToogleVRScreen.Disable();
        shipInputActions.Operator.SelectParkingArea.Disable();
    }

    private void OnVRScreenToggled(InputAction.CallbackContext obj)
    {
        Debug.Log("vr screen toggled");
        VRScreenToggled.Raise();
    }

    private void FixedUpdate()
    {
        linearInput.Value = linearMovementAction.ReadValue<Vector2>().y;
        steerInput.Value = linearInput.Value >= 0 ? steerMovementAction.ReadValue<Vector2>().x : -steerMovementAction.ReadValue<Vector2>().x;
    }
}
