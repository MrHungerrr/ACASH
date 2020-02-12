using System.Collections.Generic;
public static class ActionsOperations
{

    private static Operation[] opeartion_1 = new Operation[]
    {
        new Operation("operation"),
        new OperationSpec("operation","option"),
    };


    public static Dictionary<string, Operation[]> operations = new Dictionary<string, Operation[]>()
    {
        {"action_1", opeartion_1},
    };

}
