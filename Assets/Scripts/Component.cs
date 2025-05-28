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
    private const float ROTATION = 45f;
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
        Vector3 placementPosition = Collision.GetUpdatedPosition();
        if (placementPosition != Vector3.zero && !Collision.CollideWithComponent(placementPosition))
        {
            transform.position = placementPosition;
            SetPosition(placementPosition);
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
        componentObject.transform.Rotate(Vector3.up, ROTATION);
    }

    private void ShowButtons()
    {
        countdown = maxCountdown;
        image.gameObject.SetActive(true);
    }

    private void UpdateButtons()
    {
        if (countdown > 0f)
        {
            countdown -= Time.deltaTime;
            if (countdown < 0f) HideButtons();
        }
    }

    private void HideButtons()
    {
        countdown = 0f;
        image.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateButtons();
        if (!onMove) return;

        Vector3 updatedPosition = Collision.GetUpdatedPosition();
        if (updatedPosition != Vector3.zero)
        {
            transform.position = Vector3.Lerp(transform.position, updatedPosition, Time.deltaTime * 15f);
        }
    }
}
