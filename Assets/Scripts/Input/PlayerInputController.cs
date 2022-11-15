using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    private ShipInputActionMappings shipInputActions;
    private InputAction linearMovementAction, steerMovementAction;
    [SerializeField] private SteerInput steerInput;
    [SerializeField] private LinearInput linearInput;
    [SerializeField] GameEvent hornPressed, dockPressed, viewToggled, parkingAreaSelected;
    private InputAction shipInput;
    private void Awake()
    {
        shipInputActions = new ShipInputActionMappings();
    }
    private void OnEnable()
    {
        linearMovementAction = shipInputActions.Ship.LinearMovement;
        steerMovementAction = shipInputActions.Ship.Steer;
        linearMovementAction.Enable();
        steerMovementAction.Enable();
        shipInputActions.Ship.Horn.performed += OnHornPressed;
        shipInputActions.Ship.Dock.performed += OnDockPressed;
        shipInputActions.Ship.ToggleView.performed += OnViewToggled;
        shipInputActions.Ship.SelectParkingArea.performed += OnParkingAreaSelected;
        shipInputActions.Ship.Horn.Enable();
        shipInputActions.Ship.Dock.Enable();
        shipInputActions.Ship.ToggleView.Enable();
        shipInputActions.Ship.SelectParkingArea.Enable();
    }

    private void OnHornPressed(InputAction.CallbackContext obj)
    {
        hornPressed.Raise();
    }
     private void OnDockPressed(InputAction.CallbackContext obj)
    {
        Debug.Log("Dock pressed");
        dockPressed.Raise();
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
        shipInputActions.Ship.Horn.performed -= OnHornPressed;
        shipInputActions.Ship.Dock.performed -= OnDockPressed;
        shipInputActions.Ship.ToggleView.performed -= OnViewToggled;
        shipInputActions.Ship.SelectParkingArea.performed -= OnParkingAreaSelected;
        shipInputActions.Ship.Horn.Disable();
        shipInputActions.Ship.Dock.Disable();
        shipInputActions.Ship.ToggleView.Disable();
        shipInputActions.Ship.SelectParkingArea.Disable();
    }
    private void FixedUpdate()
    {
        linearInput.Value = linearMovementAction.ReadValue<Vector2>().y;
        steerInput.Value = linearInput.Value >= 0 ? steerMovementAction.ReadValue<Vector2>().x : -steerMovementAction.ReadValue<Vector2>().x;
    }
}
