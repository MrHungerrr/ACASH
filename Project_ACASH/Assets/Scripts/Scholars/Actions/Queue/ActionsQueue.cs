using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsQueue
{
    private readonly Queue<IActionsQueueElement> _queue = new Queue<IActionsQueueElement>();

    
    public void Add(string action)
    {
        _queue.Enqueue(new ActionsQueueElement(action));
    }


    public string GetAction()
    {
        if (_queue.Count > 0)
        {
            return _queue.Dequeue().GetAction();
        }
        else
        {
            return null;
        }
    }


    public void Reset()
    {
        _queue.Clear();
    }

    public string Show()
    {
        string show = "";
        int i = 1;

        foreach (IActionsQueueElement element in _queue)
        {
            show += i + ". " + element.Show() + "\n";
            i++;
        }

        return show;
    }
}
