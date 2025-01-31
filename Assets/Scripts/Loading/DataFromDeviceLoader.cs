using Cysharp.Threading.Tasks;
using System;
using System.IO;
using UnityEngine;

public class DataFromDeviceLoader : AbstractDataLoader
{
    public DataFromDeviceLoader(string path) : base(path)
    {
    }

    public override async UniTask<(bool, AssetBundle)> TryLoadAssetBundleAsync(string fileName, IProgress<float> progress)
    {
        returnetAssetBundle = null;

        GetPathToFile(fileName);

        if (!File.Exists(pathToFile))
        {
            return (false, null);
        }

        returnetAssetBundle = await AssetBundle.LoadFromFileAsync(pathToFile);

        progress.Report(1);

        return (true, returnetAssetBundle);
    }

    public override async UniTask<(bool, string)> TryLoadTextDataAsync(string fileName, IProgress<float> progress)
    {
        returnedFileTextContext = string.Empty;
        
        GetPathToFile(fileName);

        if (!File.Exists(pathToFile))
        {
            return (false, null);
        }

        returnedFileTextContext = await File.ReadAllTextAsync(pathToFile);

        progress.Report(1);

        return (true, returnedFileTextContext);
    }
}
