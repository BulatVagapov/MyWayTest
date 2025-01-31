public class RefreshDataCommand : ICommand
{
    private DataHolder dataHolder;

    public RefreshDataCommand(DataHolder dataHolder)
    {
        this.dataHolder = dataHolder;
    }

    public void Execute()
    {
        dataHolder.RefreshData();
    }
}
