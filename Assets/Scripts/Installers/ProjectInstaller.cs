using UnityEngine;
using Zenject;
using Managers;
using SaveSystem;

public class ProjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);
        Container.Bind<SaveController>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<GameManager>().AsSingle().NonLazy();
        BindSignals();
    }
    
    private void BindSignals()
    {
        Container.DeclareSignal<OnMovementStart>();
        Container.DeclareSignal<OnMovementStop>();
    }
}