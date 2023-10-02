using Zenject;

public class GameController
{
    private GameOverScreen _gameOverScreen;
    private Field _field;

    [Inject]
    public void Constructor(ContentMover contentMover, GameOverScreen gameOverScreen, Field field)
    {
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