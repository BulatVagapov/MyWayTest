using TMPro;
using UnityEngine.UI;
using Zenject;

public class RefreshableDataView : IInitializable, ILateDisposable
{
    private DataHolder dataHolder;
    private TMP_Text greetengMessageText;
    private Image buttonImage;

    public RefreshableDataView(DataHolder dataHolder, TMP_Text greetengMessageText, Image buttonImage)
    {
        this.dataHolder = dataHolder;
        this.greetengMessageText = greetengMessageText;
        this.buttonImage = buttonImage;
    }

    public void Initialize()
    {
        dataHolder.DataLoadedEvent.AddListener(OnDataLoaded);
    }

    public void LateDispose()
    {
        dataHolder.DataLoadedEvent.RemoveListener(OnDataLoaded);
    }

    private void OnDataLoaded()
    {
        greetengMessageText.text = dataHolder.GreetengText;
        buttonImage.sprite = dataHolder.ButtonSprite;
    }
}
