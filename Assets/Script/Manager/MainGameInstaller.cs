using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class MainGameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<DataManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<CameraManager>().FromComponentInHierarchy().AsSingle();
    }
}
