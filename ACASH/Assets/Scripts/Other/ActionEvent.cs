using System;


public static class ActionEvent
{

    public delegate void OnAction();

    public static void Unsubscribe(OnAction ConcreteDelegate)
    {
        if (ConcreteDelegate != null)
        {
            Delegate[] clientList = ConcreteDelegate.GetInvocationList();

            foreach (var d in clientList)
                ConcreteDelegate -= (d as OnAction);
        }
    }
}

