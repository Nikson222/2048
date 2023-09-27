using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class Cell : MonoBehaviour
{
    public static int MAX_VALUE = 12;

    private Vector2Int _coordinates;
    private int _value = 0;
    private Color _currentColor;

    [SerializeField] private Image _image;
    [SerializeField] private Text _text;

    public Vector2Int Coordinates => _coordinates;
    public int Points => IsEmpty ? 0 : (int)Mathf.Pow(2, _value);
    public bool IsEmpty => _value == 0;

    public void SetValue(Vector2Int coordinates, int value, Color color)
    {
        _coordinates = coordinates;

        if(value < MAX_VALUE)
            _value = value;
        else
            _value = 0;

        _currentColor = color;

        UpdateValue();
    }

    public void SetValue(int value, Color color)
    {
        if (value < MAX_VALUE)
            _value = value;
        else
            _value = 0;

        _currentColor = color;

        UpdateValue();
    }

    private void UpdateValue()
    {
        _text.text = IsEmpty ? "" : Points.ToString();
        _image.color = _currentColor;
    }
}
