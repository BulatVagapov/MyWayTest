using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class DataHolder : IInitializable
{
    private DataProvider dataProvider;

    public int StartingNumber { get; private set; }
    public string GreetengText { get; private set; }
    public Sprite ButtonSprite { get; private set; }

    public UnityEvent DataLoadedEvent = new();

    public DataHolder(DataProvider dataProvider)
    {
        this.dataProvider = dataProvider;
    }

    public void Initialize()
    {
        GetData().Forget();
    }

    private async UniTask GetData()
    {
        (StartingNumber, GreetengText, ButtonSprite) = await dataProvider.GetAllData();
        DataLoadedEvent?.Invoke();
    }

    public void RefreshData()
    {
        GetRefreshableDataAsync().Forget();
    }

    private async UniTask GetRefreshableDataAsync()
    {
        (GreetengText, ButtonSprite) = await dataProvider.LoadRefrashableData();

        DataLoadedEvent?.Invoke();
    }
}
