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


    public string GetAction()
    {
        if (queue.Peek() != null)
        {
            return queue.Dequeue().GetAction();
        }
        else
        {
            return null;
        }
    }


    public string Show()
    {
        string show = "";
        int i = 1;

        foreach (I_ActionsQueueElement element in queue)
        {
            show += i + ". " + element.Show() + "\n";
            i++;
        }

        return show;
    }
}
