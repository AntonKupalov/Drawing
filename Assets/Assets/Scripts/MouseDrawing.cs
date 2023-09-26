using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MouseDrawing : MonoBehaviour
{
    public UnityEvent<Color> ColorChanged;
    [SerializeField] 
    private float _deep = 0.5f;
    [SerializeField] 
    private Material _material;

    private Camera _camera;
    private List<LineRenderer> _lineRenderers = new();
    private LineRenderer _lineRenderer;
    private float _currentWidth = 0.1f;
    private Color _currentColor = Color.clear;

    private void Awake()
    {
        _camera = Camera.main;
    }
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartDrawingNewLine();
        }
        
        if (Input.GetMouseButton(0))
        {
            KeepDrawingNewLine();
        }
    }

    public void SetColor(Image image)
    {
        _currentColor = image.color;
        _lineRenderer.startColor = _currentColor;
        _lineRenderer.endColor = _currentColor;
        ColorChanged.Invoke(_currentColor);
    }

    public void ClearAll()
    {
        for (var index = 0; index < _lineRenderers.Count - 1; index++)
        {
            var lineRenderer = _lineRenderers[index];
            if (lineRenderer != null)
            {
                Destroy(lineRenderer.gameObject);
            }
        }
    }

    private void StartDrawingNewLine()
    {
        var newRenderer = new GameObject("Line");
        newRenderer.transform.SetParent(transform);
        newRenderer.transform.SetAsFirstSibling();
        _lineRenderer = newRenderer.AddComponent<LineRenderer>();
        SetLineRendererSettings();
        _lineRenderers.Add(_lineRenderer);
    }

    private void SetLineRendererSettings()
    {
        _lineRenderer.positionCount = 0;
        _lineRenderer.material = _material;
        _lineRenderer.startWidth = _currentWidth;
        _lineRenderer.endWidth = _currentWidth;
        _lineRenderer.startColor = _currentColor;
        _lineRenderer.endColor = _currentColor;
    }

    private void KeepDrawingNewLine()
    {
        var ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(ray))
        {
            return;
        }
        
        var mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _deep);
        var drawingPoint = _camera.ScreenToWorldPoint(mousePosition);
        _lineRenderer.positionCount++;
        _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, drawingPoint);
    }
}