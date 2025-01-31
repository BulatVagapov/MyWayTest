using TMPro;
using Zenject;
public class PointsNumberView : IInitializable, ILateDisposable
{
    private TMP_Text messageText;
    private PlayerPointsHolder playerPointsHolder;

    public PointsNumberView(TMP_Text messageText, PlayerPointsHolder playerPointsHolder)
    {
        this.messageText = messageText;
        this.playerPointsHolder = playerPointsHolder;
    }

    public void Initialize()
    {
        playerPointsHolder.PlayerPointsChadgedEvent.AddListener(OnPlayerPointsNumberChanged);
    }

    public void LateDispose()
    {
        playerPointsHolder.PlayerPointsChadgedEvent.RemoveListener(OnPlayerPointsNumberChanged);
    }

    public void OnPlayerPointsNumberChanged(int pointsNumber)
    {
        messageText.text = pointsNumber.ToString();
    }
}
