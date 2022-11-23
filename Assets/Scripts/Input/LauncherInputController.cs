using UnityEngine;
using UnityEngine.InputSystem;

public class LauncherInputController : MonoBehaviour
{
    private LauncherInputActionMappings launcherInputActions;
    [SerializeField] GameEvent viewToggled, connectionStarted;
    private void Awake()
    {
        launcherInputActions = new LauncherInputActionMappings();
    }
    private void OnEnable()
    {

        launcherInputActions.Launcher.ToogleView.performed += OnViewToggeled;
        launcherInputActions.Launcher.EnterUAEWaters.performed += OnConnectionStarted;
        launcherInputActions.Launcher.ToogleView.Enable();
        launcherInputActions.Launcher.EnterUAEWaters.Enable();

    }

   

    private void OnDisable()
    {

        launcherInputActions.Launcher.ToogleView.performed -= OnViewToggeled;
        launcherInputActions.Launcher.EnterUAEWaters.performed -= OnConnectionStarted;
        launcherInputActions.Launcher.ToogleView.Disable();
        launcherInputActions.Launcher.EnterUAEWaters.Disable();

    }
    private void OnViewToggeled(InputAction.CallbackContext obj)
    {
        viewToggled.Raise();
    }
     private void OnConnectionStarted(InputAction.CallbackContext obj)
    {
        connectionStarted.Raise();
    }
    
}
