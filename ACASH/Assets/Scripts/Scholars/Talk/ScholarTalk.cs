using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScholarTalk
{
    private Scholar Scholar;
    public bool talking { get; private set; }
    private string type_of_talk;

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
    }

    public void SayWithoutStop(KeyWord key)
    {
        MakeKey(key);
        type_of_talk = "talk";
        Scholar.Select.Selectable(false);
        Scholar.TextBox.Say(key_word);
        talking = true;
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
                        Scholar.Move.SetRotateGoal(Player.get.transform.position);
                    }
                    else
                    {
                        Scholar.Continue();
                        Scholar.Select.Selectable(true);
                        talking = false;
                    }
                    break;
                }
            case "question":
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


    public void Stop()
    {
        Scholar.TextBox.Clear();
    }
}
