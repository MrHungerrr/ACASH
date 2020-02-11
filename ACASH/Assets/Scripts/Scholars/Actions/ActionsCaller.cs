using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsCaller : MonoBehaviour
{

    private static void SelectAction(string key, Scholar scholar)
    {
        Debug.Log("Action - " + key);

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
                    scholar.Action.Doing("Think_Aloud_1");
                    break;
                }
            case "Program":
                {
                    /*
                    int buf = Random.Range(1, 3);
                    scholar.Action.Doing("Program_" + buf, "Info");
                    */
                    break;
                }
            case "Ask":
                {
                    /*
                    scholar.Action.JustQuestion("Question_1");
                    */
                    break;
                }
        }
    }


    private static void SelectCheating(string key, Scholar scholar)
    {
        Debug.Log("Cheat - " + key);

        switch (key)
        {
            case "Air":
                {
                    if (BaseMath.Probability(0.9))
                    {
                        scholar.Action.QuestionBeforeAct("Air_1", "Cheat_Air_1");
                    }
                    else
                    {
                        scholar.Action.Doing("Cheat_Air_1");
                    }
                    break;
                }
            case "Sink":
                {
                    if (BaseMath.Probability(0.9))
                    {
                        scholar.Action.QuestionBeforeAct("Sink_1", "Cheat_Sink_1");
                    }
                    else
                    {
                        scholar.Action.Doing("Cheat_Sink_1");
                    }
                    break;
                }
            case "Toilet":
                {
                    if (BaseMath.Probability(0.9))
                    {
                        scholar.Action.QuestionBeforeAct("Toilet_1", "Cheat_Toilet_1");
                    }
                    else
                    {
                        scholar.Action.Doing("Cheat_Toilet_1");
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
