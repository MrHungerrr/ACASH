using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScholarTalk
{
    private Scholar Scholar;
    public bool talking { get; private set; }
    private string type_of_talk;


    public ScholarTalk(Scholar scholar)
    {
        this.Scholar = scholar;
    }


    public void Update()
    {
        if (talking)
            Talk();
    }


    public void Say(string key)
    {
        type_of_talk = "talk";
        Scholar.Stop();
        Scholar.Select.Selectable(false);
        Scholar.TextBox.Say(Scholar.keyWord + key);
        talking = true;
    }

    public void SayWithoutStop(string key)
    {
        type_of_talk = "talk";
        Scholar.Select.Selectable(false);
        Scholar.TextBox.Say(Scholar.keyWord + key);
        talking = true;
    }

    public void SayThoughts(string key)
    {
        type_of_talk = "thoughts";
        Scholar.TextBox.Say(Scholar.keyWord + key);
        talking = true;
    }

    public void Question(string key)
    {
        type_of_talk = "question";
        Scholar.Select.Selectable(false);
        Scholar.TextBox.Question(Scholar.keyWord + key);
        talking = true;
    }


    private void Talk()
    {
        switch (type_of_talk)
        {
            case "question":
            case "talk":
                {
                    if (Scholar.TextBox.IsTalking())
                    {
                        Scholar.Move.SetRotateGoal(Player.get.transform.position);
                    }
                    else
                    {
                        Scholar.Select.Selectable(true);
                        talking = false;
                    }
                    break;
                }
            case "thoughts":
                {
                    if (!Scholar.TextBox.IsTalking())
                    {
                        talking = false;
                    }
                    break;
                }
        }
    }
}
