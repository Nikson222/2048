using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(RectTransform))]
public class Field : MonoBehaviour
{
    private const int FIELD_SIZE = 3;
    private const int START_VALUE_CELL = 1;

    private RectTransform _cellRectTransform;
    [SerializeField] private float _cellSpacing;

    [SerializeField] private Cell _cellPrefab;
    [SerializeField] private int _minStarterCellsCount;
    [SerializeField] private int _maxStarterCellsCount;

    private RectTransform _fieldRectTransform;
    private Cell[,] _cells = new Cell[FIELD_SIZE, FIELD_SIZE];

    private CellContentManager _contentManager;

    public int FieldSize => FIELD_SIZE;
    public Cell[,] Cells => _cells;

    [Inject]
    public void Constructor(CellContentManager cellContentManager)
    {
        _contentManager = cellContentManager;
    }

    public void Awake()
    {
        _cellRectTransform = _cellPrefab.GetComponent<RectTransform>();
        _fieldRectTransform = GetComponent<RectTransform>();
    }

    public void Start()
    {
        CalculateFieldSize();
        CreateCells();
        FillStarterCells();
    }

    public void RestartField()
    {
        ClearField();
        FillStarterCells();
    }

    private void CalculateFieldSize()
    {
        float sizeOfField = FIELD_SIZE * (_cellRectTransform.rect.width + _cellSpacing) + _cellSpacing;

        _fieldRectTransform.sizeDelta = new Vector2(sizeOfField, sizeOfField);
    }

    private void CreateCells()
    {
        Vector2 startCellPosition = CalculateStartCellPosition();

        for (int x = 0; x < FIELD_SIZE; x++)
        {
            for (int y = 0; y < FIELD_SIZE; y++)
            {
                Cell cell = InstantiateCell();

                CalculateInitCellPosition(cell, startCellPosition, new Vector2Int(x, y));

                _cells[x, y] = cell;
            }
        }

        InitCells();
    }

    private void ClearField()
    {
        foreach (var cell in _cells)
        {
            if (!cell.IsEmpty)
                cell.DeleteContent();
        }
    }

    private void FillStarterCells()
    {
        int countCells = Random.Range(_minStarterCellsCount, _maxStarterCellsCount + 1);

        Vector2Int lastCoordinates = Vector2Int.zero;

        for (int i = 0; i < countCells; i++)
        {
            int xCoordinate = Random.Range(0, FIELD_SIZE);
            int yCoordinate = Random.Range(0, FIELD_SIZE);

            if (new Vector2Int(xCoordinate, yCoordinate) != lastCoordinates)
            {
                if (_cells[xCoordinate, yCoordinate].IsEmpty)
                {
                    CellContent cellContent = _contentManager.GetContent(START_VALUE_CELL);
                    _cells[xCoordinate, yCoordinate].SetContent(cellContent);
                }
            }
            else
                i--;

            lastCoordinates = new Vector2Int(xCoordinate, yCoordinate);
        }
    }

    private Vector2 CalculateStartCellPosition()
    {
        float startPositionX = -(_fieldRectTransform.rect.width / 2) + (_cellRectTransform.rect.width / 2) + _cellSpacing;
        float startPositionY = (_fieldRectTransform.rect.height / 2) - (_cellRectTransform.rect.height / 2) - _cellSpacing;

        return new Vector2(startPositionX, startPositionY);
    }

    private void CalculateInitCellPosition(Cell cell, Vector2 startPosition, Vector2Int coordinates)
    {
        Vector2 newPosition = new Vector2(startPosition.x + (coordinates.x * (_cellRectTransform.rect.width + _cellSpacing)),
            startPosition.y - (coordinates.y * (_cellRectTransform.rect.height + _cellSpacing)));

        cell.transform.localPosition = newPosition;
    }

    private Cell InstantiateCell()
    {
        Cell cell = Instantiate(_cellPrefab, transform, false);
        cell.transform.localScale = Vector3.one;

        return cell;
    }

    private void InitCells()
    {
        for (int x = 0; x < FIELD_SIZE; x++)
        {
            for (int y = 0; y < FIELD_SIZE; y++)
            {
                Cell currentCell = _cells[x, y];

                Cell rightCell = (x < FIELD_SIZE - 1) ? _cells[x + 1, y] : null;
                Cell downCell = (y < FIELD_SIZE - 1) ? _cells[x, y + 1] : null;
                Cell leftCell = (x > 0) ? _cells[x - 1, y] : null;
                Cell upperCell = (y > 0) ? _cells[x, y - 1] : null;

                currentCell.Init(rightCell, downCell, leftCell, upperCell);
                currentCell.OnMerge += _contentManager.UpdateContent;
            }
        }
    }

    public void CreateNewContentInRandomCell()
    {
        List<Cell> emptyCells = new List<Cell>();
        foreach (var cell in _cells)
        {
            if (cell.IsEmpty)
                emptyCells.Add(cell);
        }

        if (emptyCells.Count > 0)
        {
            int randomIndex = Random.Range(0, emptyCells.Count);

            emptyCells[randomIndex].SetContent(_contentManager.GetContent(START_VALUE_CELL));
        }
    }
}
