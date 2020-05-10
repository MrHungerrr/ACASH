using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ActionsList
{
    private static Action[] actions_1 = new Action[]
    {
        new Action("Think_Aloud", "All"),
        //new Action("Ask", "All"),
    };

    private static Action[] actions_2 = new Action[]
    {
        new Action("Outside", "All"),
        new Action("Sink", "All"),
    };

    private static Action[] actions_3 = new Action[]
    {
        new Action("Toilet", "All"),
    };

    private static Dictionary<int, Action[]> actions = new Dictionary<int, Action[]>()
    {
        { 1, actions_1 },
        { 2, actions_2 },
        { 3, actions_3 },
    };





    private static Action[] cheatings_1 = new Action[]
    {
        //new Action("Ask_Neighbour", "All"),
        //new Action("Asshole_Class_Computer", ScholarTypes.asshole),
        new Action("Note", "All"),
        new Action("Calculate", "All"),
    };

    private static Action[] cheatings_2 = new Action[]
    {
        new Action("Outside", "All"),
        new Action("Sink", "All"),
        //new Action("Program", "All"),
    };

    private static Action[] cheatings_3 = new Action[]
    {
        new Action("Toilet", "All"),
        //new Action("Asshole_Teacher_Room_Computer", ScholarTypes.asshole),
        //new Action("Nerd_Break_Cameras", ScholarTypes.nerd),
        //new Action("Nerd_Break_Desk", ScholarTypes.nerd),
    };

    private static Dictionary<int, Action[]> cheatings = new Dictionary<int, Action[]>()
    {
        { 1, cheatings_1 },
        { 2, cheatings_2 },
        { 3, cheatings_3 },
    };





    public static string ActionChoice(int cost, Scholar scholar)
    {
        Action action = FindAction(cost, scholar.type);
        Debug.Log("Scholar №" + scholar.Info.number + " выбрал действие - " + action.name);
        return action.name + "_" + cost;
    }

    public static string CheatingChoice(int cost, Scholar scholar)
    {
        Action cheating = FindCheating(cost, scholar.type);
        Debug.Log("Ученик №" + scholar.Info.number + "выбрал списывание - " + cheating.name);
        return "Cheating_" + cheating.name + "_" + cost;
    }



    private static Action FindAction(int cost, string scholar)
    {
        int choice = Random.Range(0, actions[cost].Length);

        for (int i = 0; i < actions[cost].Length; i++)
        {
            if (actions[cost][choice].scholar == "All" || actions[cost][choice].scholar == scholar)
            {
                return actions[cost][choice];
            }

            choice++;

            if (choice >= actions[cost].Length)
                choice = 0;
        }
        return null;
    }


    private static Action FindCheating(int cost, string scholar)
    {
        int choice = Random.Range(0, cheatings[cost].Length);

        for (int i = 0; i < cheatings[cost].Length; i++)
        {
            if (cheatings[cost][choice].scholar == "All" || cheatings[cost][choice].scholar == scholar)
            {
                return cheatings[cost][choice];
            }

            choice++;

            if (choice >= cheatings[cost].Length)
                choice = 0;
        }

        return null;
    }
}
