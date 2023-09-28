using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class Cell : MonoBehaviour
{
    public Cell _rightCell;
    public Cell _downCell;
    public Cell _leftCell;
    public Cell _upperCell;
    public CellContent _content;

    private Vector2Int _coordinates;

    public bool IsEmpty => _content == null;
    public Vector2Int Coordinates => _coordinates;
    
    public void Init(Cell rightCell, Cell downCell, Cell leftCell, Cell upperCell)
    {
        _rightCell = rightCell;
        _downCell = downCell;
        _leftCell = leftCell;
        _upperCell = upperCell;
    }

    public void SetContent(CellContent cellContent)
    {
        _content = cellContent;
        _content.transform.SetParent(transform, false);
        _content.transform.localPosition = Vector3.zero;
    }

    public void ClearContent()
    {
        _content.transform.SetParent(null);
        _content.gameObject.SetActive(false);
        _content = null;
    }
}
