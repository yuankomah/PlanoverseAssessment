using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Component : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private GameObject componentObject;
    [SerializeField] private Button componentButton;
    [SerializeField] private Button removeButton;
    [SerializeField] private Button rotateButton;
    [SerializeField] private Button moveButton;

    private Vector3 position;
    private bool onMove;
    private float countdown;
    private const float maxCountdown = 5f;

    private void Awake()
    {
        onMove = false;
        image.gameObject.SetActive(false);
    }

    public void SetPosition(Vector3 position)
    {
        this.position = position;
    }

    void Start()
    {
        componentButton.onClick.AddListener(() => ShowButtons());
        removeButton.onClick.AddListener(() => Destroy(gameObject));
        rotateButton.onClick.AddListener(() => RotateObject());
        moveButton.onClick.AddListener(() => MoveObject());
        GameInputActions.Instance.ClickInput += GameInputAction_ClickInput;
    }

    private void OnDestroy()
    {
        GameInputActions.Instance.ClickInput -= GameInputAction_ClickInput;
    }

    private void GameInputAction_ClickInput(object sender, System.EventArgs e)
    {
        if (!onMove) return;
        Vector3 mousePos = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        if (Physics.Raycast(ray, out RaycastHit hit, Collision.RAY_MAXIMUM_RANGE, Collision.DEFAULT_LAYER))
        {
            Vector3 placementPosition = new Vector3(hit.point.x, Collision.BASE_POSITION_Y, hit.point.z);
            if (!Collision.CollideWithComponent(placementPosition))
            {
                transform.position = placementPosition;
                SetPosition(placementPosition);
            }
        } else
        {
            SetPosition(position);
        }

        onMove = false;
        GameInputActions.Instance.SetCanvasScreenCamera();
    }

    private void MoveObject()
    {
        SetPosition(transform.position);
        onMove = true;
        HideButtons();
        GameInputActions.Instance.SetCanvasScreenOverlay();
    }

    private void RotateObject()
    {
        countdown = maxCountdown;
        componentObject.transform.Rotate(Vector3.up, 45f);
    }

    private void ShowButtons()
    {
        countdown = maxCountdown;
        image.gameObject.SetActive(true);
    }

    private void HideButtons()
    {
        countdown = 0f;
        image.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (countdown > 0f)
        {
            countdown -= Time.deltaTime;
            if (countdown < 0f) HideButtons();
        }

        if (!onMove) return;

        Vector3 mousePos = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        if (Physics.Raycast(ray, out RaycastHit hit, Collision.RAY_MAXIMUM_RANGE, Collision.DEFAULT_LAYER))
        {
            transform.position = new Vector3(hit.point.x, Collision.BASE_POSITION_Y, hit.point.z);
        }
    }
}
