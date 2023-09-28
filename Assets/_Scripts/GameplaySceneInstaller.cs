using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class GameplaySceneInstaller : MonoInstaller
{
    [SerializeField] private ColorsManager _colorsManager;
    [SerializeField] private ContentPooler _contentPooler;
    public override void InstallBindings()
    {
        Container.Bind<ColorsManager>().FromInstance(_colorsManager).AsTransient();
        Container.Bind<ContentPooler>().FromInstance(_contentPooler).AsTransient();
        Container.Bind<CellContentManager>().AsTransient();
        Container.Bind<InputHandler>().To<DesktopInput>().AsSingle();
    }
}
