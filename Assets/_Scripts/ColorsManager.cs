using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorsManager : MonoBehaviour
{
    [SerializeField] private Color[] avaliableColors;

    public void OnValidate()
    {
        if(avaliableColors.Length > Cell.MAX_VALUE)
            avaliableColors = new Color[Cell.MAX_VALUE];
    }

    public Color GetColorByValue(int value)
    {
        if (value < avaliableColors.Length && value < Cell.MAX_VALUE)
            return avaliableColors[value];
        else
            return avaliableColors[0];
    }
}
