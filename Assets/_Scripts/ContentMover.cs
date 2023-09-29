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

        List<Cell> cellsWhichMoving = new List<Cell>();

        foreach (var cell in _field.Cells)
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

        // Дождитесь, пока все ячейки завершат движение
        yield return new WaitWhile(() => cellsWhichMoving.Any(cell => cell.IsMoving));

        OnMove?.Invoke();
        IsMovingProcess = false;
        //IsMovingProcess = true;

        //bool anyoneCellCanMove = true;

        //while (anyoneCellCanMove)
        //{
        //    List<Cell> cellsWhichMoving = new List<Cell>();

        //    foreach (var cell in _field.Cells)
        //    {
        //        if (!cell.IsEmpty)
        //        {
        //            if (cell.IsCanMoveToDirection(swipeDirection) && !cell.IsMoving)
        //            {
        //                cell.StartMoveContentByDirection(swipeDirection);
        //                cellsWhichMoving.Add(cell);
        //                print(cell);
        //            }

        //            yield return null;
        //        }
        //    }

        //    if (cellsWhichMoving.Count < 1)
        //    {
        //        anyoneCellCanMove = false;
        //        break;
        //    }

        //    while (cellsWhichMoving.Count > 0)
        //    {
        //        List<Cell> cellsWhichEndMoving = new List<Cell>();

        //        foreach (var cell in cellsWhichMoving)
        //        {
        //            if (!cell.IsMoving)
        //            {
        //                cellsWhichEndMoving.Add(cell);
        //            }
        //        }

        //        foreach (var cell in cellsWhichEndMoving)
        //        {
        //            cellsWhichMoving.Remove(cell);
        //        }

        //        yield return new WaitForSeconds(0.1f);
        //    }
        //}

        //OnMove?.Invoke();
        //IsMovingProcess = false;
    }
}