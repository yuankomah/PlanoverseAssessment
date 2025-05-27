using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ItemButton : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private Button button;
    [SerializeField] private GameObject itemObject;
    [SerializeField] private GameObject previewObject;
    private bool selected = false;

    void Start()
    {
        button.onClick.AddListener(() => ButtonClicked());
        GameInputActions.Instance.ClickInput += GameInputActions_ClickInput;
    }

    private void GameInputActions_ClickInput(object sender, System.EventArgs e)
    {
        if (!selected) return;

        Vector3 mousePos = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        if (Physics.Raycast(ray, out RaycastHit hit, Collision.RAY_MAXIMUM_RANGE, Collision.DEFAULT_LAYER))
        {
            Vector3 placementPosition = new Vector3(hit.point.x, Collision.BASE_POSITION_Y, hit.point.z);
            if (!Collision.CollideWithComponent(placementPosition))
            {
                Instantiate(itemObject, placementPosition, Quaternion.identity);
            }
        }
        ButtonClicked();
    }

    private void ButtonClicked()
    {
        if (selected)
        {
            HidePreview();
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
        } else
        {
            ShowPreview();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        }

        selected = !selected;
    }

    private void HidePreview()
    {
        previewObject.SetActive(false);
    }

    private void ShowPreview()
    {
        previewObject.SetActive(true);
    }

    void Update()
    {
        if (!selected) return;

        Vector3 mousePos = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        if (Physics.Raycast(ray, out RaycastHit hit, Collision.RAY_MAXIMUM_RANGE, Collision.DEFAULT_LAYER))
        {
            if (!previewObject.activeSelf) ShowPreview();
            previewObject.transform.position = new Vector3(hit.point.x, Collision.BASE_POSITION_Y, hit.point.z);
        }
        else if (previewObject.activeSelf)
        {
            HidePreview();
        }

    }
}
