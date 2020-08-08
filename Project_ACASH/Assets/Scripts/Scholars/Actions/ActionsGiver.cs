using UnityEngine;
using System.Collections;

public static class ActionsGiver
{

    public static void GiveActions(ActionsCount actions_count)
    {
        ActionsCount actions = new ActionsCount(actions_count);
        Debug.Log(actions);
        int length;

        if (ScholarManager.Instance.scholars.Length < actions.count)
            length = ScholarManager.Instance.scholars.Length;
        else
            length = actions.count;

        
        int[] scholars_numbs = new int[length];
        Zeroing(scholars_numbs);

        Scholar s;
        int cost;

        for (int i = 0; i < length; i++)
        {
            scholars_numbs[i] = ScholarChoice(scholars_numbs);
            s = ScholarManager.Instance.scholars[scholars_numbs[i]];

            cost = actions.GetRandomCost();

            if (cost == 0)
            {
                //Закончились действия
                break;
            }

            string act;

            if (s.Cheat.Probability())
            {
                //Debug.Log("Списывание");
                act = ActionsList.CheatingChoice(cost, s);
            }
            else
            {
                //Debug.Log("Действие");
                act = ActionsList.ActionChoice(cost, s);
            }

            s.Action.AddAction(act);
        }
    }

    private static int ScholarChoice(int[] busy)
    {
        bool unic;
        int buf = ScholarManager.Instance.GetRandomScholarIndex();

        do
        {
            unic = true;
            for (int i = 0; i< busy.Length; i++)
            {
                if (buf == busy[i])
                {
                    unic = false;
                    buf++;
                    buf %= ScholarManager.Instance.scholars.Length;
                }
            }
        }
        while (!unic);

        return buf;
    }



    private static void Zeroing(int[] array)
    {
        for(int i = 0; i < array.Length; i++)
        {
            array[i] = -1;
        }
    }
}
