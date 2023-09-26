using UnityEngine.UI;
using UnityEngine;

public class Point : MonoBehaviour
{
    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    private void Update()
    {
        transform.position = Input.mousePosition;
    }

    public void SetColor(Color color)
    {
        _image.color = color;
    }
}