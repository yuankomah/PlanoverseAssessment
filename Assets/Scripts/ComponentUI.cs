using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ComponentUI : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Button componentButton;
    [SerializeField] private Component componentObject;

    private void Awake()
    {
        image.gameObject.SetActive(false);
    }

    void Start()
    {
        componentButton.onClick.AddListener(() => ShowButtons());
    }

    private void ShowButtons()
    {
        Show();
        image.gameObject.SetActive(true);
    }

    public void Hide()
    {
        image.gameObject.SetActive(false);
    }

    public void Show()
    {
        image.gameObject.SetActive(true);
    }
}
