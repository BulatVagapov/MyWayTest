public class IncreasePlayerPointsCommand : ICommand
{
    private PlayerPointsHolder playerPointsHolder;
    private int addededPoints;

    public IncreasePlayerPointsCommand(PlayerPointsHolder playerPointsHolder, int addededPoints)
    {
        this.playerPointsHolder = playerPointsHolder;
        this.addededPoints = addededPoints;
    }

    public void Execute()
    {
        playerPointsHolder.IncreasePoints(addededPoints);
    }
}
