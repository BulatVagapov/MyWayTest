using UnityEngine.Events;

public class PlayerPointsHolder
{
    public int PlayerPoints { get; private set; }

    public UnityEvent<int> PlayerPointsChadgedEvent = new();

    public void IncreasePoints(int points)
    {
        PlayerPoints += points;
        PlayerPointsChadgedEvent?.Invoke(PlayerPoints);
    }

    public void SetPlayerPointsNumber(int pointsNumber)
    {
        PlayerPoints = pointsNumber;
        PlayerPointsChadgedEvent?.Invoke(PlayerPoints);
    }
}
