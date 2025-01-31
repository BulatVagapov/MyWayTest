using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private Button increaseButton;
    [SerializeField] private Button refreshButton;

    private readonly string DeviceDataPath = "D:/Work/UnityTests/MyWay/GameData/Save";
    private readonly string ServerDataPath = "D:/Work/UnityTests/MyWay/GameData/Server";

    public override void InstallBindings()
    {
        Container.Bind<PlayerPointsHolder>().AsSingle().NonLazy();


        Container.BindInterfacesAndSelfTo<ButtonCommandHolder>().FromMethod(_ => CreateButtonCommandHolder(increaseButton, Container.Resolve<IncreasePlayerPointsCommand>()));
        Container.BindInterfacesAndSelfTo<ButtonCommandHolder>().FromMethod(_ => CreateButtonCommandHolder(refreshButton, Container.Resolve<RefreshDataCommand>()));

        Container.Bind<AbstractDataLoader>().WithId(0).To<DataFromDeviceLoader>().AsTransient().WithArguments(DeviceDataPath);
        Container.Bind<AbstractDataLoader>().WithId(1).To<DataFromServerLoader>().AsTransient().WithArguments(ServerDataPath);
        Container.Bind<FileNameStrings>().AsSingle();

        Container.Bind<DataProvider>().FromMethod(_ => CreateDataProvider(Container.ResolveId< AbstractDataLoader>(0),
                                                        Container.ResolveId<AbstractDataLoader>(1),
                                                        Container.Resolve<FileNameStrings>(),
                                                        Container.Resolve<DefaultDataValues>()));

        Container.BindInterfacesAndSelfTo<DataHolder>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<PlayerPointsStartingNumberProvider>().AsSingle();

        Container.Bind<Saver>().AsSingle().WithArguments(DeviceDataPath);
        Container.BindInterfacesAndSelfTo<PointsNumberSaver>().AsSingle().NonLazy();
    }

    private ButtonCommandHolder CreateButtonCommandHolder(Button button, ICommand command)
    {
        return new ButtonCommandHolder(button, command);
    }

    private DataProvider CreateDataProvider(AbstractDataLoader dataFromDeviceLoader, AbstractDataLoader dataFromServerLoader, FileNameStrings fileNameStrings, DefaultDataValues defaultData)
    {
        return new DataProvider(dataFromDeviceLoader, dataFromServerLoader, fileNameStrings, defaultData);
    }
}