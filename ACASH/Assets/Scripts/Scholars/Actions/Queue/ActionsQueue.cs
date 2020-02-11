using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsQueue
{
    public Queue<I_ActionsQueueElement> queue = new Queue<I_ActionsQueueElement>();

    
    public void Add(string action)
    {
        queue.Enqueue(new ActionsQueueElement(action));
    }

    public void Add(string action_true, string action_false, ref bool option)
    {
        queue.Enqueue(new ActionsQueueElementChoice(action_true, action_false, ref option));
    }


    public string GetAction()
    {
        return queue.Dequeue().GetAction();
    }


    public string Show()
    {
        foreach()
    }
}
