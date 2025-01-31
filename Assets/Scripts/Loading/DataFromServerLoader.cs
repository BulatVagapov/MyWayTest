using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DataFromServerLoader : AbstractDataLoader
{
    private FileNameStrings fileNameStrings;
    
    public DataFromServerLoader(string path, FileNameStrings fileNameStrings) : base(path)
    {
        this.fileNameStrings = fileNameStrings;
    }

    public override async UniTask<(bool, AssetBundle)> TryLoadAssetBundleAsync(string fileName, IProgress<float> progress)
    {
        GetPathToFile(fileName);

        UnityWebRequest wedRequest = UnityWebRequestAssetBundle.GetAssetBundle(pathToFile);

        await wedRequest.SendWebRequest().ToUniTask(progress);

        if (wedRequest.result == UnityWebRequest.Result.Success)
        {
            return (true, DownloadHandlerAssetBundle.GetContent(wedRequest));
        }

        return (false, null);
    }

    public override async UniTask<(bool, string)> TryLoadTextDataAsync(string fileName, IProgress<float> progress)
    {
        GetPathToFile(fileName);

        UnityWebRequest wedRequest = UnityWebRequest.Get(pathToFile);

        await wedRequest.SendWebRequest().ToUniTask(progress);
        
        if (wedRequest.result == UnityWebRequest.Result.Success)
        {
            return (true, wedRequest.downloadHandler.text);
        }

        return (false, null);
    }

    protected override void GetPathToFile(string fileName)
    {
        if (fileName.Equals(fileNameStrings.AssetBundle))
        {
            pathToFile = "https://drive.usercontent.google.com/u/0/uc?id=1xOBcQCfswWmtIQVpU_vd7qSKxQ7ndFos&export=download";
        }

        if (fileName.Equals(fileNameStrings.GreetingText))
        {
            pathToFile = "https://drive.usercontent.google.com/u/0/uc?id=1ifaSkIK6O9g2UYigHJeoz7nugXc0B4yt&export=download";
        }

        if (fileName.Equals(fileNameStrings.StartingNumber))
        {
            pathToFile = "https://drive.usercontent.google.com/u/0/uc?id=13UeXzzRnmvf6GzPyG2okWy9Im0olPlxQ&export=download";
        }
    }
}
