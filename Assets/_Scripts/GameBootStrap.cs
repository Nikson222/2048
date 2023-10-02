using UnityEngine;
using Zenject;

public class GameBootStrap : MonoBehaviour
{
    private Field _field;
    private ContentMover _contentMover;
    private CellContentManager _cellContentManager;
    private ScoreHandler _scoreHandler;
    private GameController _gameController;

    [Inject]
    public void Constructor(Field field, ContentMover contentMover, CellContentManager cellContentManager, ScoreHandler scoreHandler, GameController gameController)
    {
        _field = field;
        _contentMover = contentMover;
        _cellContentManager = cellContentManager;
        _scoreHandler = scoreHandler;
        _gameController = gameController;
    }

    public void Start()
    {
        _contentMover.OnMove += _field.CreateNewContentInRandomCell;

        _field.OnRestartField += _scoreHandler.ResetScore;
        _contentMover.OnNoOpportunityMove += _gameController.StopGame;

        foreach (var cell in _field.Cells)
            cell.OnMerge += _scoreHandler.IncrementScore;
    }
}
