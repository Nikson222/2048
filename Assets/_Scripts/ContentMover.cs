using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ContentMover : MonoBehaviour
{
    private IInputHandler _inputHandler;
    private Field _field;
    public event Action OnMove;
    private bool IsMovingProcess = false;

    [Inject]
    public void Constructor(IInputHandler inputHandler, Field field)
    {
        _inputHandler = inputHandler;
        _field = field;
    }

    public void Update()
    {
        SwipeDirection direction = _inputHandler.GetSwipeDirection();
        if (!direction.Equals(SwipeDirection.None) && !IsMovingProcess)
            StartCoroutine(MoveAllContent(direction));
    }

    private IEnumerator MoveAllContent(SwipeDirection swipeDirection)
    {
        IsMovingProcess = true;

        List<Cell> cells = new List<Cell>();

        if (swipeDirection == SwipeDirection.Down || swipeDirection == SwipeDirection.Right)
            cells = GetInvertedListCells();
        else
            cells = GetNormalListCells();

        while (IsMovingProcess)
        {
            List<Cell> cellsWhichMoving = new List<Cell>();

            foreach (var cell in cells)
            {
                if (!cell.IsEmpty)
                {
                    if (cell.IsCanMoveToDirection(swipeDirection) && !cell.IsMoving)
                    {
                        cell.StartMoveContentByDirection(swipeDirection);
                        cellsWhichMoving.Add(cell);
                    }
                }
            }

            if (cellsWhichMoving.Count > 0)
            {
                yield return new WaitWhile(() => cellsWhichMoving.Any(cell => cell.IsMoving));

            }
            else
            {
                IsMovingProcess = false;
                OnMove?.Invoke();
            }
        }
    }

    private List<Cell> GetNormalListCells()
    {
        List<Cell> normalList = new List<Cell>();

        foreach (var cell in _field.Cells)
        {
            normalList.Add(cell);
        }

        return normalList;
    }

    private List<Cell> GetInvertedListCells()
    {
        List<Cell> invertedList = new List<Cell>();

        for (int x = _field.FieldSize-1; x > -1; x--)
        {
            for (int y = _field.FieldSize-1; y > -1; y--)
            {
                invertedList.Add(_field.Cells[x, y]);
            }
        }

        return invertedList;
    }
}