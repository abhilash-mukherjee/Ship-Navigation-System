using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class OperatorInputController : MonoBehaviour
{
    private OperatorInputActionMappings operatorInputActions;
    //private InputAction linearMovementAction, steerMovementAction;
    //[SerializeField] private SteerInput steerInput;
    //[SerializeField] private LinearInput linearInput;
    [SerializeField] GameEvent VRScreenToggled, viewToggled, parkingAreaSelected;
    private void Awake()
    {
        operatorInputActions = new OperatorInputActionMappings();
    }
    private void OnEnable()
    {
        //linearMovementAction = operatorInputActions.Operator.LinearMovement;
        //steerMovementAction = operatorInputActions.Operator.Steer;
        //linearMovementAction.Enable();
        //steerMovementAction.Enable();
        operatorInputActions.Operator.ToggleView.performed += OnViewToggled;
        operatorInputActions.Operator.SelectParkingArea.performed += OnParkingAreaSelected;
        operatorInputActions.Operator.ToogleVRScreen.performed += OnVRScreenToggled;
        operatorInputActions.Operator.ToggleView.Enable();
        operatorInputActions.Operator.ToogleVRScreen.Enable();
        operatorInputActions.Operator.SelectParkingArea.Enable();
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
        //linearMovementAction.Disable();
        //steerMovementAction.Disable();
        operatorInputActions.Operator.ToggleView.performed -= OnViewToggled;
        operatorInputActions.Operator.ToogleVRScreen.performed -= OnVRScreenToggled;
        operatorInputActions.Operator.SelectParkingArea.performed -= OnParkingAreaSelected;
        operatorInputActions.Operator.ToggleView.Disable();
        operatorInputActions.Operator.ToogleVRScreen.Disable();
        operatorInputActions.Operator.SelectParkingArea.Disable();
    }

    private void OnVRScreenToggled(InputAction.CallbackContext obj)
    {
        Debug.Log("vr screen toggled");
        VRScreenToggled.Raise();
    }

    //private void FixedUpdate()
    //{
    //    linearInput.Value = linearMovementAction.ReadValue<Vector2>().y;
    //    steerInput.Value = linearInput.Value >= 0 ? steerMovementAction.ReadValue<Vector2>().x : -steerMovementAction.ReadValue<Vector2>().x;
    //}
}
