using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameplaySceneInstaller : MonoInstaller
{
    [SerializeField] private ColorsManager _colorsManager;

    public override void InstallBindings()
    {
        Container.Bind<ColorsManager>().FromInstance(_colorsManager).AsSingle();
        Container.Bind<IInput>().To<DesktopInput>().AsSingle();
    }
}
