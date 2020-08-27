using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsSimple
{

    public string GetActions()
    {
        int option = Random.Range(0, 3);

        switch(option)
        {
            case 0:
                {
                    return "Write";
                }
            case 1:
                {
                    return "Answer";
                }
            case 2:
                {
                    return "Watch_Rules";
                }
        }

        return "Write";
    }
}
