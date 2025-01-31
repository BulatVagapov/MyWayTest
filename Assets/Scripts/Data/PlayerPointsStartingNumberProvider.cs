using UnityEngine;
using Zenject;

public class PlayerPointsStartingNumberProvider : IInitializable
{
    private PlayerPointsHolder playerPointsHolder;
    private DataHolder dataHolder;

    public PlayerPointsStartingNumberProvider(PlayerPointsHolder playerPointsHolder, DataHolder dataHolder)
    {
        this.playerPointsHolder = playerPointsHolder;
        this.dataHolder = dataHolder;
    }

    public void Initialize()
    {
        dataHolder.DataLoadedEvent.AddListener(OnDataLoaded);
    }

    private void OnDataLoaded()
    {
        playerPointsHolder.SetPlayerPointsNumber(dataHolder.StartingNumber);
        dataHolder.DataLoadedEvent.RemoveListener(OnDataLoaded);
    }
}
