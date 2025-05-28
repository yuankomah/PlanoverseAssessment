using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ComponentRotateUI : MonoBehaviour
{
    [SerializeField] private Button rotateButton;
    [SerializeField] private Component componentObject;
    [SerializeField] private float ROTATION = 45f;

    void Start()
    {
        rotateButton.onClick.AddListener(RotateObject);
    }

    private void RotateObject()
    {
        
        componentObject.GetGameObject().transform.Rotate(Vector3.up, ROTATION);
    }
}
