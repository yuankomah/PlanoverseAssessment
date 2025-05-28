using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections;

public class ComponentMoveUI : MonoBehaviour
{
    [SerializeField] private Button moveButton;
    [SerializeField] private Component componentObject;

    private void Start()
    {
        moveButton.onClick.AddListener(MoveObject);
        GameInputActions.Instance.ClickInput += GameInputAction_ClickInput;
    }

    private void OnDestroy()
    {
        GameInputActions.Instance.ClickInput -= GameInputAction_ClickInput;
    }

    private void GameInputAction_ClickInput(object sender, System.EventArgs e) 
    {
        if (!componentObject.IsMoving()) return;
        Vector3 placementPosition = GetUpdatedPosition();
        if (placementPosition != Vector3.zero && !Collision.CollideWithComponent(placementPosition))
        {
            componentObject.transform.position = placementPosition;
        }

        componentObject.SetMove(false);
        GameInputActions.Instance.SetCanvasScreenCamera();
    }

    private void MoveObject()
    {

        componentObject.SetMove(true);
        componentObject.HideButtons();
        GameInputActions.Instance.SetCanvasScreenOverlay();
    }


    public static Vector3 GetUpdatedPosition()
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();

        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        if (Physics.Raycast(ray, out RaycastHit hit, Collision.RAY_MAXIMUM_RANGE, Collision.DEFAULT_LAYER))
        {
            return new Vector3(hit.point.x, Collision.BASE_POSITION_Y, hit.point.z);
        }

        return Vector3.zero;

    }
}
