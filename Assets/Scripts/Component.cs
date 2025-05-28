using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Component : MonoBehaviour
{
    [SerializeField] private GameObject componentObject;
    [SerializeField] private ComponentUI componentButton;
    [SerializeField] private ComponentRemoveUI removeButton;
    [SerializeField] private ComponentRotateUI rotateButton;
    [SerializeField] private ComponentMoveUI moveButton;

    private float countdown;
    private const float SPEED_MULTIPLIER = 15f;
    private const float MAX_COUNTDOWN = 5f;

    private bool onMove;
    private void Awake()
    {
        onMove = false;
    }

    public void ResetCountdown()
    {
        countdown = MAX_COUNTDOWN;
    }

    private void UpdateButtons()
    {
        if (countdown > 0f)
        {
            countdown -= Time.deltaTime;
            if (countdown < 0f) HideButtons();
        }
    }

    public void HideButtons()
    {
        countdown = 0f;
        componentButton.Hide();
    }

    public GameObject GetGameObject()
    {
        return componentObject;
    }

    public bool IsMoving()
    {
        return onMove;
    }

    public void SetMove(bool onMove)
    {
        this.onMove = onMove;
    }


    private void Update()
    {
        UpdateButtons();

        if (!onMove) return;
        Vector3 updatedPosition = ComponentMoveUI.GetUpdatedPosition();
        if (updatedPosition != Vector3.zero)
        {
            transform.position = Vector3.Lerp(transform.position, updatedPosition, Time.deltaTime * SPEED_MULTIPLIER);
        }
    }

}
