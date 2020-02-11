using UnityEngine;
using System.Collections;

public static class ActionsGiver
{

    public static void GiveActions(ActionsCount actions)
    {
        int length;

        if (ScholarManager.get.scholars.Length < actions.count)
            length = ScholarManager.get.scholars.Length;
        else
            length = actions.count;

        
        int[] scholars_numbs = new int[length];
        Zeroing(scholars_numbs);

        Scholar s;
        int cost;

        for (int i = 0; i < length; i++)
        {

            scholars_numbs[i] = ScholarChoice(scholars_numbs);
            s = ScholarManager.get.scholars[scholars_numbs[i]];

            cost = actions.GetRandomCost();

            if (cost > 0)
            {
                string act;

                if (s.Agent.CheatProbability())
                {
                    Debug.Log("Списывание");
                    act = ActionsList.CheatingChoice(cost , s);
                }
                else
                {
                    Debug.Log("Действие");
                    act = ActionsList.ActionChoice(cost, s);
                }
            }
            else
            {
                //Закончились действия
                break;
            }
        }
    }

    private static int ScholarChoice(int[] busy)
    {
        bool unic;
        int buf = ScholarManager.get.GetRandomScholarIndex();

        do
        {
            unic = true;
            for (int i = 0; i< busy.Length; i++)
            {
                if (buf == busy[i])
                {
                    unic = false;
                    buf++;
                    buf %= ScholarManager.get.scholars.Length;
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
