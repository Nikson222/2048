using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class Cell : MonoBehaviour
{
    private const float MIN_DISTANCE_TO_TARGET_CELL = 0.01f;
    [SerializeField] private float _speed;

    public Cell _rightCell;
    public Cell _downCell;
    public Cell _leftCell;
    public Cell _upperCell;
    public CellContent _content;

    private bool _isMoving = false;
    public bool IsMoving => _isMoving;
    public bool IsEmpty => _content == null;

    public event Action<CellContent, int> OnMerge;
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
        _content.transform.localScale = Vector3.one;
        _content.transform.localPosition = Vector3.zero;
    }

    public void DeleteContent()
    {
        _content.transform.SetParent(null);
        _content.gameObject.SetActive(false);
        _content = null;
    }


    public void StartMoveContentByDirection(SwipeDirection direction)
    {
        StartCoroutine(MoveContentIntoNeighbour(direction));
    }

    private IEnumerator MoveContentIntoNeighbour(SwipeDirection direction)
    {
        Cell neighbour = GetNeighbourByMoveDirection(direction);
        CellContent cellContent = _content;

        if (neighbour)
        {
            if (IsPossibilityToMerge(direction))
            {
                yield return StartCoroutine(neighbour.Merge(neighbour));
            }
            else
            {
                yield return StartCoroutine(MoveContentRoutine(neighbour));
                ClearContent();
                neighbour.SetContent(cellContent);
            }
        }
    }

    private IEnumerator MoveContentRoutine(Cell targetCell)
    {
        _isMoving = true;

        while (Vector3.Distance(_content.transform.position, targetCell.transform.position) > MIN_DISTANCE_TO_TARGET_CELL)
        {
            _content.transform.position = Vector3.MoveTowards(_content.transform.position, targetCell.transform.position, _speed * Time.fixedDeltaTime);
            yield return null;
        }

        _content.transform.position = targetCell.transform.position;

        _isMoving = false;
    }

    private Cell GetNeighbourByMoveDirection(SwipeDirection direction)
    {
        switch (direction)
        {
            case SwipeDirection.Up:
                return _upperCell;
            case SwipeDirection.Down:
                return _downCell;
            case SwipeDirection.Left:
                return _leftCell;
            case SwipeDirection.Right:
                return _rightCell;
            default:
                return null;
        }
    }

    public void ClearContent()
    {
        _content.transform.SetParent(null);
        _content = null;
    }

    public bool IsPossibilityToMerge(SwipeDirection swipeDirection)
    {
        Cell neightbor = GetNeighbourByMoveDirection(swipeDirection);

        if (neightbor != null && _content != null && neightbor._content != null && neightbor._content.Points == _content.Points)
            return true;
        else
            return false;
    }

    private IEnumerator Merge(Cell cellToMerge)
    {
        yield return StartCoroutine(MoveContentRoutine(cellToMerge));
        cellToMerge.DoublePoints();
        DeleteContent();
    }

    public void DoublePoints()
    {
        OnMerge?.Invoke(_content, _content.Value + 1);
    }

    public bool IsCanMoveToDirection(SwipeDirection swipeDirection)
    {
        Cell neightbor = GetNeighbourByMoveDirection(swipeDirection);
        if (neightbor != null && neightbor.IsEmpty || IsPossibilityToMerge(swipeDirection))
            return true;
        else
            return false;
    }
}
