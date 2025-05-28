using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ItemButton : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private Button button;
    [SerializeField] private Component itemObject;
    [SerializeField] private Component previewObject;
    private bool selected = false;
    private const float SPEED_MULTIPLIER = 15f;

    void Start()
    {
        button.onClick.AddListener(() => ButtonClicked());
        GameInputActions.Instance.ClickInput += GameInputActions_ClickInput;
    }

    private void GameInputActions_ClickInput(object sender, System.EventArgs e)
    {
        if (!selected) return;
        Vector3 placementPosition = ComponentMoveUI.GetUpdatedPosition();
        if (placementPosition != Vector3.zero && !Collision.CollideWithComponent(placementPosition))
        {
            Instantiate(itemObject, placementPosition, Quaternion.identity);
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
        previewObject.gameObject.SetActive(false);
    }

    private void ShowPreview()
    {
        previewObject.gameObject.SetActive(true);
    }

    void Update()
    {
        if (!selected) return;

        Vector3 updatedPosition = ComponentMoveUI.GetUpdatedPosition();
        if (updatedPosition != Vector3.zero)
        {
            previewObject.transform.position = Vector3.Lerp(previewObject.transform.position,
            updatedPosition, Time.deltaTime * SPEED_MULTIPLIER);
           
        }
    }
}
