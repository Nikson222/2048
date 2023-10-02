using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameController
{
    private ContentMover _contentMover;
    private GameOverScreen _gameOverScreen;
    private Field _field;

    [Inject]
    public void Constructor(ContentMover contentMover, GameOverScreen gameOverScreen, Field field)
    {
        _contentMover = contentMover;
        _gameOverScreen = gameOverScreen;
        _field = field;
    }

    public void StopGame()
    {
        _gameOverScreen.ShowScreen();
    }

    public void StartGame()
    {
        _field.RestartField();

        _gameOverScreen.HideScreen();
    }
}