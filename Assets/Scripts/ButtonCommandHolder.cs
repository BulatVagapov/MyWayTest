using UnityEngine.UI;
using Zenject;

public class ButtonCommandHolder : IInitializable
{
    private Button button;
    private ICommand command;

    public ButtonCommandHolder(Button button, ICommand command)
    {
        this.button = button;
        this.command = command;
    }

    public void Initialize()
    {
        button.onClick.AddListener(command.Execute);
    }
}
