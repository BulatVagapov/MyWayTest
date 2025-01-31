using UnityEngine;
using Zenject;

public class PointsNumberSaver : IInitializable
{
    private PlayerPointsHolder playerPointsHolder;
    private Saver saver;
    private StartNumberJsonModel jsonModel;
    private FileNameStrings fileNameStrings;

    public PointsNumberSaver(PlayerPointsHolder playerPointsHolder, Saver saver, FileNameStrings fileNameStrings)
    {
        this.playerPointsHolder = playerPointsHolder;
        this.saver = saver;
        this.fileNameStrings = fileNameStrings;
    }

    public void Initialize()
    {
        Application.quitting += OnApplicationQuit;
    }

    private void OnApplicationQuit()
    {
        jsonModel = new();
        jsonModel.StartNumber = playerPointsHolder.PlayerPoints;
        saver.SaveData<StartNumberJsonModel>(fileNameStrings.StartingNumber, jsonModel);
    }
}
