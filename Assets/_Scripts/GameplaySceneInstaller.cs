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

    public override void InstallBindings()
    {
        Container.Bind<ColorsManager>().FromInstance(_colorsManager).AsTransient();
        Container.Bind<Field>().FromInstance(_field).AsTransient();
        Container.Bind<ContentPooler>().FromInstance(_contentPooler).AsTransient();
        Container.Bind<ContentMover>().FromInstance(_contentMover).AsTransient();
        Container.Bind<CellContentManager>().AsTransient();
        Container.Bind<IInputHandler>().To<DesktopInput>().AsSingle();
    }
}
