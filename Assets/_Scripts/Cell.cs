using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class Cell : MonoBehaviour
{
    private const float MIN_DISTANCE_TO_TARGET_CELL = 0.01f;
    [SerializeField] private float _speedMoveContent;

    private Cell _rightCell;
    private Cell _downCell;
    private Cell _leftCell;
    private Cell _upperCell;
    private CellContent _content;

    public CellContent Content => _content;

    private bool _isMoving = false;
    public bool IsMoving => _isMoving;
    public bool IsEmpty => Content == null;

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
        Content.transform.SetParent(transform, false);
        Content.transform.localScale = Vector3.one;
        Content.transform.localPosition = Vector3.zero;
    }

    private void DisableContent(CellContent content)
    {
        content.transform.SetParent(null);
        content.gameObject.SetActive(false);
    }

    public void DeleteContent()
    {
        Content.transform.SetParent(null);
        Content.gameObject.SetActive(false);
        _content = null;
    }

    public void StartMoveContentByDirection(SwipeDirection direction)
    {
        StartCoroutine(MoveContentIntoNeighbour(direction));
    }

    private IEnumerator MoveContentIntoNeighbour(SwipeDirection direction)
    {
        Cell neighbour = GetNeighbourByMoveDirection(direction);
        CellContent cellContent = Content;

        Vector2 saveStartPosition = Content.transform.position;

        if (neighbour)
        {
            if (IsPossibilityToMerge(direction))
            {
                ClearContent();
                yield return StartCoroutine(Merge(cellContent, neighbour, saveStartPosition));
            }
            else
            {
                ClearContent();
                yield return StartCoroutine(MoveContentRoutine(cellContent, neighbour, saveStartPosition));
                neighbour.SetContent(cellContent);
            }
        }
    }

    private IEnumerator MoveContentRoutine(CellContent content, Cell targetCell, Vector2 startPosition)
    {
        SetContentFieldParent(content);

        content.transform.position = startPosition;

        _isMoving = true;

        while (Vector3.Distance(content.transform.position, targetCell.transform.position) > MIN_DISTANCE_TO_TARGET_CELL)
        {
            content.transform.position = Vector3.MoveTowards(content.transform.position, targetCell.transform.position, _speedMoveContent * Time.fixedDeltaTime);
            yield return null;
        }

        content.transform.position = targetCell.transform.position;

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
        Content.transform.SetParent(null);
        _content = null;
    }

    public bool IsPossibilityToMerge(SwipeDirection swipeDirection)
    {
        Cell neightbor = GetNeighbourByMoveDirection(swipeDirection);

        if (neightbor != null && Content != null && neightbor.Content != null && neightbor.Content.Points == Content.Points)
            return true;
        else
            return false;
    }

    private IEnumerator Merge(CellContent content, Cell cellToMerge, Vector2 startPosition)
    {
        yield return StartCoroutine(MoveContentRoutine(content, cellToMerge, startPosition));
        cellToMerge.DoublePoints();
        DisableContent(content);
    }

    public void DoublePoints()
    {
        OnMerge?.Invoke(Content, Content.Value + 1);
    }

    public bool IsCanMoveToDirection(SwipeDirection swipeDirection)
    {
        Cell neightbor = GetNeighbourByMoveDirection(swipeDirection);
        if (neightbor != null && neightbor.IsEmpty || IsPossibilityToMerge(swipeDirection))
            return true;
        else
            return false;
    }

    private void SetContentFieldParent(CellContent content)
    {
        content.transform.SetParent(transform.parent, false);
        content.transform.localScale = Vector3.one;
    }
}
