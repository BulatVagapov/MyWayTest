using Zenject;

public class CommandsInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<IncreasePlayerPointsCommand>().AsSingle().WithArguments(1);
        Container.BindInterfacesAndSelfTo<RefreshDataCommand>().AsSingle();
    }
}