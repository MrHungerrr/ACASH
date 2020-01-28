using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ActionsList
{

    private static Dictionary<int, Action[]> actions = new Dictionary<int, Action[]>()
    {
        { 1, actions_1 },
        { 2, actions_2 },
        { 3, actions_3 },
    };

    private static Action[] actions_1 = new Action[]
    {
        new Action("Think_Aloud", "All"),
        new Action("Web", "All"),
        new Action("Ask", "All"),
    };

    private static Action[] actions_2 = new Action[]
    {
        new Action("Air", "All"),
        new Action("Sink", "All"),
    };

    private static Action[] actions_3 = new Action[]
    {
        new Action("Toilet", "All"),
    };




    private static Dictionary<int, Action[]> cheatings = new Dictionary<int, Action[]>()
    {
        { 1, cheatings_1 },
        { 2, cheatings_2 },
        { 3, cheatings_3 },
    };

    private static Action[] cheatings_1 = new Action[]
    {
        new Action("Ask_Neighbour", "All"),
        new Action("Asshole_Class_Computer", ScholarTypes.asshole),
        new Action("Note", "All"),
        new Action("Phone", "All"),
    };

    private static Action[] cheatings_2 = new Action[]
    {
        new Action("Air", "All"),
        new Action("Sink", "All"),
    };

    private static Action[] cheatings_3 = new Action[]
    {
        new Action("Toilet", "All"),
        new Action("Asshole_Teacher_Room_Computer", ScholarTypes.asshole),
        new Action("Nerd_Break_Cameras", ScholarTypes.nerd),
        new Action("Nerd_Break_Desk", ScholarTypes.nerd),
    };








    public static void ActionChoice(int cost, Scholar scholar)
    {
        Action action = FindAction(cost, scholar.type);

        SelectAction(action.name, scholar);
    }

    public static string CheatingChoice(int cost, Scholar scholar)
    {
        Action cheating = FindCheating(cost, scholar.type);

        return cheating.name;
    }





    private static Action FindAction(int cost, string scholar)
    {
        int choice = Random.Range(0, actions[cost].Length - 1);

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
        int choice = Random.Range(0, cheatings[cost].Length - 1);

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


    private static void SelectAction(string key, Scholar scholar)
    {
        switch (key)
        {
            case "Air":
                {
                    if (BaseMath.Probability(0.9))
                    {
                        scholar.Action.QuestionBeforeAct("Air_1", "Air_1");
                    }
                    else
                    {
                        scholar.Action.Doing("Air_1");
                    }
                    break;
                }
            case "Sink":
                {
                    if (BaseMath.Probability(0.9))
                    {
                        scholar.Action.QuestionBeforeAct("Sink_1", "Sink_1");
                    }
                    else
                    {
                        scholar.Action.Doing("Sink_1");
                    }
                    break;
                }
            case "Toilet":
                {
                    if (BaseMath.Probability(0.9))
                    {
                        scholar.Action.QuestionBeforeAct("Toilet_1", "Toilet_1");
                    }
                    else
                    {
                        scholar.Action.Doing("Toilet_1");
                    }
                    break;
                }
            case "Think_Aloud":
                {
                    int buf = Random.Range(1, 3);
                    scholar.Action.Doing("Think_Aloud_" + buf);
                    break;
                }
            case "Web":
                {
                    int buf = Random.Range(1, 3);
                    scholar.Action.Doing("Web_" + buf);
                    break;
                }
            case "Ask":
                {
                    int buf = Random.Range(1, 3);
                    scholar.Action.Doing("Ask_" + buf);
                    break;
                }
        }
    }


    private static void SelectCheating(string key, Scholar scholar)
    {
        switch (key)
        {
            case "Air":
                {
                    if (BaseMath.Probability(0.9))
                    {
                        scholar.Action.QuestionBeforeAct("Air_1", "Air_1");
                    }
                    else
                    {
                        scholar.Action.Doing("Air_1");
                    }
                    break;
                }
            case "Sink":
                {
                    if (BaseMath.Probability(0.9))
                    {
                        scholar.Action.QuestionBeforeAct("Sink_1", "Sink_1");
                    }
                    else
                    {
                        scholar.Action.Doing("Sink_1");
                    }
                    break;
                }
            case "Toilet":
                {
                    if (BaseMath.Probability(0.9))
                    {
                        scholar.Action.QuestionBeforeAct("Toilet_1", "Toilet_1");
                    }
                    else
                    {
                        scholar.Action.Doing("Toilet_1");
                    }
                    break;
                }
            case "Ask_Neighbour":
                {

                    break;
                }
            case "Asshole_Class_Computer":
                {

                    break;
                }
            case "Note":
                {

                    break;
                }
            case "Phone":
                {

                    break;
                }
            case "Asshole_Teacher_Room_Computer":
                {

                    break;
                }
            case "Nerd_Break_Cameras":
                {

                    break;
                }
            case "Nerd_Break_Desk":
                {

                    break;
                }
        }
    }
}
