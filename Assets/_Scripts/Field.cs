using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(RectTransform))]
public class Field : MonoBehaviour
{
    private const int FIELD_SIZE = 3;
    private const int INIT_VALUE_CELL = 0;
    private const int START_VALUE_CELL = 1;

    [SerializeField] private Cell _cellPrefab;
    private RectTransform _cellRectTransform;
    [SerializeField] private float _cellSpacing;

    [SerializeField] private int _minStarterCellsCount;
    [SerializeField] private int _maxStarterCellsCount;

    private RectTransform _fieldRectTransform;
    private Cell[,] _cells = new Cell[FIELD_SIZE, FIELD_SIZE];

    private ColorsManager _colorsManager;

    [Inject]
    public void Constructor(ColorsManager colorsManager)
    {
        _colorsManager = colorsManager;
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
        CreateCellsWithStartValue();
    }

    public void RestartField()
    {
        ClearField();
        CreateCellsWithStartValue();
    }

    private void ClearField()
    {
        foreach (var cell in _cells)
        {
            InitCell(cell, cell.Coordinates);
        }
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

                InitCell(cell, new Vector2Int(x, y));
            }
        }
    }

    private void CreateCellsWithStartValue()
    {
        int countCells = Random.Range(_minStarterCellsCount, _maxStarterCellsCount+1);

        Color colorByValue = _colorsManager.GetColorByValue(START_VALUE_CELL);

        for (int i = 0; i < countCells; i++)
        {
            int xCoordinate = Random.Range(0, FIELD_SIZE);
            int yCoordinate = Random.Range(0, FIELD_SIZE);

            if (_cells[xCoordinate, yCoordinate].IsEmpty)
                _cells[xCoordinate, yCoordinate].SetValue(START_VALUE_CELL, colorByValue);
            else
                i--;
        }
    }

    private void CalculateFieldSize()
    {
        float sizeOfField = FIELD_SIZE * (_cellRectTransform.rect.width + _cellSpacing) + _cellSpacing;

        _fieldRectTransform.sizeDelta = new Vector2(sizeOfField, sizeOfField);
    }

    private Vector2 CalculateStartCellPosition()
    {
        float startPositionX = -(_fieldRectTransform.rect.width / 2) + (_cellRectTransform.rect.width / 2) + _cellSpacing;
        float startPositionY = (_fieldRectTransform.rect.height / 2) - (_cellRectTransform.rect.height / 2) - _cellSpacing;

        return new Vector2(startPositionX, startPositionY);
    }

    private Cell InstantiateCell()
    {
        Cell cell = Instantiate(_cellPrefab, transform, false);
        cell.transform.localScale = Vector3.one;

        return cell;
    }

    private void InitCell(Cell cell, Vector2Int coordinates)
    {
        Color initcolor = _colorsManager.GetColorByValue(INIT_VALUE_CELL);

        cell.SetValue(coordinates, INIT_VALUE_CELL, initcolor);
    }
    
    private void CalculateInitCellPosition(Cell cell, Vector2 startPosition, Vector2Int coordinates)
    {
        Vector2 newPosition = new Vector2(startPosition.x + (coordinates.x * (_cellRectTransform.rect.width + _cellSpacing)), 
            startPosition.y - (coordinates.y * (_cellRectTransform.rect.height + _cellSpacing)));

        cell.transform.localPosition = newPosition;
    }
}
