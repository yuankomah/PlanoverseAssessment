using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ComponentRemoveUI : MonoBehaviour
{
    [SerializeField] private Button removeButton;
    [SerializeField] private Component componentObject;

    void Start()
    {
        removeButton.onClick.AddListener(() => Destroy(componentObject.gameObject));
    }
}
