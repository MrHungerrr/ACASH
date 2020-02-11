public class ActionsQueueElement : I_ActionsQueueElement
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

}
