using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScholarTalk
{
    private Scholar Scholar;
    public bool talking { get; private set; }
    private string type_of_talk;
    private Quaternion last_rotate_goal;

    public KeyWord key_word;


    public ScholarTalk(Scholar scholar)
    {
        this.Scholar = scholar;
        key_word = new KeyWord(scholar.type);
    }


    public void Update()
    {
        if (talking)
            Talk();
    }


    public void Say(KeyWord key)
    {
        MakeKey(key);
        type_of_talk = "hard_talk";
        Scholar.Pause();
        Scholar.Select.Selectable(false);
        Scholar.TextBox.Say(key_word);
        talking = true;
        last_rotate_goal = Scholar.Move.Rotation();
    }

    public void SayWithoutStop(KeyWord key)
    {
        MakeKey(key);
        type_of_talk = "talk";
        Scholar.Select.Selectable(false);
        Scholar.TextBox.Say(key_word);
        talking = true;
        last_rotate_goal = Scholar.Move.Rotation();
    }

    public void SayThoughts(KeyWord key)
    {
        MakeKey(key);
        type_of_talk = "thoughts";
        Scholar.TextBox.Say(key_word);
        talking = true;
    }

    public void SayThoughts(string key)
    {
        MakeKey(key);
        type_of_talk = "thoughts";
        Scholar.TextBox.Say(key_word);
        talking = true;
    }



    public void Question(KeyWord key)
    {
        MakeKey(key);
        type_of_talk = "question";
        Scholar.Select.Selectable(false);
        Scholar.TextBox.Question(key_word);
        talking = true;
        last_rotate_goal = Scholar.Move.Rotation();
    }


    public void MakeKey(KeyWord key)
    {
        key_word *= key;
    }

    public void MakeKey(string key)
    {
        Debug.Log(key);
        Debug.Log(key_word.GetFullWord());
        key_word *= key;
    }



    private void Talk()
    {
        switch (type_of_talk)
        {
            case "hard_talk":
                {

                    if (Scholar.TextBox.IsTalking())
                    {

                        Scholar.Move.SetRotateGoal(Player.Instance.transform.position);

                        if (!Scholar.TextBox.act && !Scholar.Select.selectable)
                            Scholar.Select.Selectable(true);
                    }
                    else
                    {
                        Scholar.Move.SetRotateGoal(last_rotate_goal);
                        Scholar.Continue();
                        talking = false;

                        if (!Scholar.Select.selectable)
                            Scholar.Select.Selectable(true);
                    }
                    break;
                }
            case "question":
                {
                    if (Scholar.TextBox.IsTalking())
                    {
                        Scholar.Move.SetRotateGoal(Player.Instance.transform.position);
                    }
                    else
                    {
                        Scholar.Move.SetRotateGoal(last_rotate_goal);
                        Scholar.Select.Selectable(true);
                        talking = false;
                    }
                    break;
                }
            case "talk":
                {
                    if (Scholar.TextBox.IsTalking())
                    {
                        Scholar.Move.SetRotateGoal(Player.Instance.transform.position);

                        if (!Scholar.TextBox.act && !Scholar.Select.selectable)
                            Scholar.Select.Selectable(true);
                    }
                    else
                    {
                        Scholar.Move.SetRotateGoal(last_rotate_goal);

                        if (!Scholar.Select.selectable)
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


    public void Stop()
    {
        Scholar.TextBox.Clear();
    }
}
