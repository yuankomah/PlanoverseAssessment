using System;
using UnityEngine;

public class GameInputActions : MonoBehaviour
{
    public static GameInputActions Instance { get; private set; }
    private InputActions inputActions;
    [SerializeField] private Canvas canvas;
    public event EventHandler ClickInput;

    private void Awake()
    {
        Instance = this;
        inputActions = new InputActions();
        inputActions.Enable();
        inputActions.Component.Click.performed += Click_Performed;
    }

    private void OnDestroy()
    {
        inputActions.Component.Click.performed -= Click_Performed;
    }

    private void Click_Performed(UnityEngine.InputSystem.InputAction.CallbackContext e)
    {
        ClickInput?.Invoke(this, EventArgs.Empty);
    }

    public void SetCanvasScreenCamera()
    {
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
    }

    public void SetCanvasScreenOverlay()
    {
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
    }
}
