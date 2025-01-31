using System.IO;
using UnityEngine;

public class Saver
{
    private string path;
    private string savingDataString;
    private string pathToFile;

    public Saver(string path)
    {
        this.path = path;

        if (!Directory.Exists(this.path))
            Directory.CreateDirectory(this.path);
    }

    public void SaveData<T>(string fileName, T data)
    {
        pathToFile = path + "/" + fileName;

        savingDataString = JsonUtility.ToJson(data);

        File.WriteAllText(pathToFile, savingDataString);
    }
}
