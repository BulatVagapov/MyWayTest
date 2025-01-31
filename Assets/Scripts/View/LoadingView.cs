using Cysharp.Threading.Tasks;
using DG.Tweening;
using Zenject;

public class LoadingView : IInitializable
{
    private LoadingScreen loadingScreen;
    private DataHolder dataHolder;

    private int loadingTime;
    private float loadingStep = 0.1f;

    private bool dataIsLoaded;
    private bool loadingTimeIsOver;

    public LoadingView(LoadingScreen loadingScreen, DataHolder dataHolder, int loadingTime)
    {
        this.loadingScreen = loadingScreen;
        this.dataHolder = dataHolder;
        this.loadingTime = loadingTime;
    }

    public void Initialize()
    {
        dataHolder.DataLoadedEvent.AddListener(OnDataGot);
        Loading().Forget();
    }

    private async UniTask Loading()
    {
        dataIsLoaded = false;
        loadingTimeIsOver = false;

        await loadingScreen.LoadingProgressFillImage.DOFillAmount(1, loadingTime);

        loadingTimeIsOver = true;

        if (dataIsLoaded) FinishLoading();
    }

    private void OnDataGot()
    {
        dataIsLoaded = true;

        if (loadingTimeIsOver) FinishLoading();
    }

    private void FinishLoading()
    {
        loadingScreen.gameObject.SetActive(false);
    }
}
