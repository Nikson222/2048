using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellsMover
{
    public CellsMover(IInput inputController)
    {
        _inputController = inputController;
    }

    private IInput _inputController;
    private Cell[,] _cells;

    public void GetCellList(Cell[,] cells)
    {
        _cells = cells;
    }
}
