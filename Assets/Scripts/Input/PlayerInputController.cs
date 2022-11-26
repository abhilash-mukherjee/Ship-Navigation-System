using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerInputController : MonoBehaviour
{
    private ShipInputActionMappings shipInputActions;
    private InputAction linearMovementAction, steerMovementAction;
    [SerializeField] private SteerInput steerInput;
    [SerializeField] private LinearInput linearInput;
    [SerializeField] GameEvent hornPressed, dockPressed, viewToggled, sailorSidePanelToggled, requestParkingArea;
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
        shipInputActions.Ship.RequestParkingArea.performed += ParkingAreaRequested;
        //shipInputActions.Ship.TooglePanel.performed += OnPanelToggled;
        shipInputActions.Ship.Horn.Enable();
        shipInputActions.Ship.Dock.Enable();
        shipInputActions.Ship.ToggleView.Enable();
        shipInputActions.Ship.RequestParkingArea.Enable();
        //shipInputActions.Ship.TooglePanel.Enable();
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
        SceneManager.LoadScene(0);
        viewToggled.Raise();
    }
    private void OnPanelToggled(InputAction.CallbackContext obj)
    {
        Debug.Log("parkingAreaSelected");
        sailorSidePanelToggled.Raise();
    }

    private void OnDisable()
    {
        linearMovementAction.Disable();
        steerMovementAction.Disable();
        shipInputActions.Ship.Horn.performed -= OnHornPressed;
        shipInputActions.Ship.Dock.performed -= OnDockPressed;
        shipInputActions.Ship.ToggleView.performed -= OnViewToggled;
        shipInputActions.Ship.RequestParkingArea.performed -= ParkingAreaRequested;
        //shipInputActions.Ship.TooglePanel.performed -= OnPanelToggled;
        shipInputActions.Ship.Horn.Disable();
        shipInputActions.Ship.Dock.Disable();
        shipInputActions.Ship.ToggleView.Disable();
        shipInputActions.Ship.RequestParkingArea.Disable();
        //shipInputActions.Ship.TooglePanel.Disable();
    }

    private void ParkingAreaRequested(InputAction.CallbackContext obj)
    {
        requestParkingArea.Raise();
    }

    private void FixedUpdate()
    {
        linearInput.Value = linearMovementAction.ReadValue<Vector2>().y;
        steerInput.Value = linearInput.Value >= 0 ? steerMovementAction.ReadValue<Vector2>().x : -steerMovementAction.ReadValue<Vector2>().x;
    }
}
