public class ActionsQueueElementChoice : I_ActionsQueueElement
{
    private string action_true;
    private string action_false;
    private bool choice;

    public ActionsQueueElementChoice(string action_true, string action_false, ref bool choice)
    {
        this.action_true = action_true;
        this.action_false = action_false;
        this.choice = choice;
    }

    public string GetAction()
    {
        if (choice)
            return action_true;
        else
            action_false;
    }

}
