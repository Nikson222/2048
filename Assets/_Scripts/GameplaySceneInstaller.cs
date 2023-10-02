using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class GameplaySceneInstaller : MonoInstaller
{
    [SerializeField] private ColorsManager _colorsManager;
    [SerializeField] private ContentPooler _contentPooler;
    [SerializeField] private ContentMover _contentMover;
    [SerializeField] private Field _field;
    [SerializeField] private ScoreHandler _scorehandler;
    [SerializeField] private GameOverScreen _gameOverScreen;

    public override void InstallBindings()
    {
        Container.Bind<ColorsManager>().FromInstance(_colorsManager).AsTransient();
        Container.Bind<ScoreHandler>().FromInstance(_scorehandler).AsTransient();
        Container.Bind<GameOverScreen>().FromInstance(_gameOverScreen).AsTransient();
        Container.Bind<Field>().FromInstance(_field).AsTransient();
        Container.Bind<ContentPooler>().FromInstance(_contentPooler).AsTransient();
        Container.Bind<ContentMover>().FromInstance(_contentMover).AsTransient();
        Container.Bind<CellContentManager>().AsTransient();
        Container.Bind<GameController>().AsTransient();
        Container.Bind<IInputHandler>().To<MobileInput>().AsSingle();
    }
}
