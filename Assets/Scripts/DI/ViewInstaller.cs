using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ViewInstaller : MonoInstaller
{
    [SerializeField] private TMP_Text pointsNumberText;
    [SerializeField] private TMP_Text greetengMessageText;
    [SerializeField] private LoadingScreen loadingScreen;
    [SerializeField] private Image buttonImage;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<PointsNumberView>().AsSingle().WithArguments(pointsNumberText).NonLazy();
        Container.BindInterfacesAndSelfTo<RefreshableDataView>().AsSingle().WithArguments(greetengMessageText, buttonImage).NonLazy();
        Container.BindInstance<LoadingScreen>(loadingScreen).AsSingle();
        Container.BindInterfacesAndSelfTo<LoadingView>().AsSingle().WithArguments(1).NonLazy();
    }
}