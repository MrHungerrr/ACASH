public class ActionsQueueElement : IActionsQueueElement
{
    private string action;

    public ActionsQueueElement(string action)
    {
        this.action = action;
    }

    public string GetAction()
    {
        return action;
    }

    public string Show()
    {
        return action;
    }

}
