using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellContent : MonoBehaviour
{
    public static int MAX_VALUE = 12;

    private int _value = 0;
    private Color _currentColor;

    [SerializeField] private Image _image;
    [SerializeField] private Text _text;

    public int Points => (int)Mathf.Pow(2, _value);

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
        _text.text = Points.ToString();
        _image.color = _currentColor;
    }
}
