using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class DataProvider
{
    private AbstractDataLoader dataFromDeviceLoader;
    private AbstractDataLoader dataFromServerLoader;
    private FileNameStrings fileNameStrings;
    private DefaultDataValues defaultData;

    private (bool, object) loadingStringResult;
    private (bool, AssetBundle) loadingAssetBundleResult;

    public UnityEvent<float> LoadingProgressChangedEvent = new();
    private List<ProgressWithAction> loadingProgresses = new();
    public float AllDataLoadingProgress { get; private set; }

    public DataProvider(AbstractDataLoader dataFromDeviceLoader, AbstractDataLoader dataFromServerLoader, FileNameStrings fileNameStrings, DefaultDataValues defaultData)
    {
        this.dataFromDeviceLoader = dataFromDeviceLoader;
        this.dataFromServerLoader = dataFromServerLoader;
        this.fileNameStrings = fileNameStrings;
        this.defaultData = defaultData;
    }

    private void ResetProgress(int listCount)
    {
        AllDataLoadingProgress = 0;

        for (int i = 0; i < loadingProgresses.Count; i++)
        {
            loadingProgresses[i].FloatValueChanged -= OnLoadingProgressChanged;
        }

        loadingProgresses = new();

        for(int i = 0; i < listCount; i++)
        {
            loadingProgresses.Add(new ProgressWithAction());
            loadingProgresses[i].FloatValueChanged += OnLoadingProgressChanged;
        }
    }

    private void OnLoadingProgressChanged()
    {
        AllDataLoadingProgress = loadingProgresses.Sum(x => x.CurrentProgressFloatValue);
        AllDataLoadingProgress /= loadingProgresses.Count;
        LoadingProgressChangedEvent?.Invoke(AllDataLoadingProgress);
    }

    public async UniTask<(int, string, Sprite)> GetAllData()
    {
        ResetProgress(3);

        (int, string, Sprite) data;

        data = await UniTask.WhenAll(GetStartingNumberAsync(loadingProgresses[0]), GetGreetingTextAsync(loadingProgresses[1]), GetButtonSpriteAsync(loadingProgresses[2]));

        return data;
    }

    public async UniTask<(string, Sprite)> LoadRefrashableData()
    {
        ResetProgress(2);

        (string, Sprite) data;

        data = await UniTask.WhenAll(GetGreetingTextAsync(loadingProgresses[0]), GetButtonSpriteAsync(loadingProgresses[1]));

        return data;
    }

    public async UniTask<int> GetStartingNumberAsync(IProgress<float> progress)
    {
        loadingStringResult = await dataFromDeviceLoader.TryLoadTextDataAsync(fileNameStrings.StartingNumber, progress);

        if (loadingStringResult.Item1)
        {
            return JsonUtility.FromJson<StartNumberJsonModel>(loadingStringResult.Item2.ToString()).StartNumber;
        }

        loadingStringResult = await dataFromServerLoader.TryLoadTextDataAsync(fileNameStrings.StartingNumber, progress);

        if (loadingStringResult.Item1)
        {
            return JsonUtility.FromJson<StartNumberJsonModel>(loadingStringResult.Item2.ToString()).StartNumber;
        }

        return defaultData.StartingNumber;
    }

    public async UniTask<string> GetGreetingTextAsync(IProgress<float> progress)
    {
        loadingStringResult = await dataFromServerLoader.TryLoadTextDataAsync(fileNameStrings.GreetingText, progress);

        if (loadingStringResult.Item1)
        {
            return JsonUtility.FromJson<GreetingMessageJsonModel>(loadingStringResult.Item2.ToString()).GreetingText;
        }

        return defaultData.GreetingText;
    }

    public async UniTask<Sprite> GetButtonSpriteAsync(IProgress<float> progress)
    {
        if(loadingAssetBundleResult.Item2 != null)
        {
            loadingAssetBundleResult.Item2.Unload(false);
        }

        loadingAssetBundleResult = await dataFromServerLoader.TryLoadAssetBundleAsync(fileNameStrings.AssetBundle, progress);

        if (loadingStringResult.Item1)
        {
            AssetBundleRequest loadAsset = loadingAssetBundleResult.Item2.LoadAssetAsync<Sprite>("Assets/Sprites/RedButton.png");

            return loadAsset.asset as Sprite;
        }

        return defaultData.ButtonSprite;
    }
}
