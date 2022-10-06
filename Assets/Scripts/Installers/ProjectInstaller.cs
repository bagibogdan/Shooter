using Zenject;
using SaveSystem;

public class ProjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);
        Container.Bind<SaveController>().AsSingle().NonLazy();
    }
}