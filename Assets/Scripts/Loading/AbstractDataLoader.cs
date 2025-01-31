using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public abstract class AbstractDataLoader
{
    protected string path;
    protected string pathToFile;
    protected string returnedFileTextContext;
    protected AssetBundle returnetAssetBundle;

    protected AbstractDataLoader(string path)
    {
        this.path = path;
    }

    public abstract UniTask<(bool, string)> TryLoadTextDataAsync(string fileName, IProgress<float> progress);

    public abstract UniTask<(bool, AssetBundle)> TryLoadAssetBundleAsync(string fileName, IProgress<float> progress);

    protected virtual void GetPathToFile(string fileName)
    {
        pathToFile = path + "/" + fileName;
    }
}
