using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CellContentManager
{
    private const int MAX_VALUE_CONTENT = 12;

    private ContentPooler _contentPooler;
    private ColorsManager _colorsManager;

    public event Action OnMaxValueCell;

    public CellContentManager(ContentPooler contentPooler, ColorsManager colorsManager)
    {
        _contentPooler = contentPooler;
        _colorsManager = colorsManager;
    }

    public CellContent GetContent(int value)
    {
        CellContent cellContent = _contentPooler.GiveCellContent();

        UpdateContent(cellContent, value);

        return cellContent;
    }

    public void UpdateContent(CellContent cellContent, int value)
    {
        cellContent.SetValue(value, _colorsManager.GetColorByValue(value));

        if (value == MAX_VALUE_CONTENT)
            OnMaxValueCell?.Invoke();
    }
}
