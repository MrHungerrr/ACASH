using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScholarTalk
{
    public bool Talking { get; private set; }

    private Scholar _scholar;
    private Quaternion _lastRotateGoal;
    private KeyWord _keyWord;


    public ScholarTalk(Scholar scholar)
    {
        this._scholar = scholar;
        _keyWord = new KeyWord("Scholar");
    }


    public void Update()
    {
        if (Talking)
            Talk();
    }


    //public void Say(KeyWord key)
    //{
    //    MakeKey(key);
    //    _scholar.Pause();
    //    _scholar.Select.Selectable(false);
    //    _scholar.TextBox.Say(_keyWord);
    //    Talking = true;
    //    _lastRotateGoal = _scholar.Move.Rotation();
    //}

    //public void SayWithoutStop(KeyWord key)
    //{
    //    MakeKey(key);
    //    _scholar.Select.Selectable(false);
    //    _scholar.TextBox.Say(_keyWord);
    //    Talking = true;
    //    _lastRotateGoal = _scholar.Move.Rotation();
    //}

    public void SayThoughts(KeyWord key)
    {
        MakeKey(key);
        _scholar.TextBox.Say(_keyWord);
        Talking = true;
    }

    public void SayThoughts(string key)
    {
        MakeKey(key);
        _scholar.TextBox.Say(_keyWord);
        Talking = true;
    }


    public void MakeKey(KeyWord key)
    {
        _keyWord *= key;
    }

    public void MakeKey(string key)
    {
        Debug.Log(key);
        Debug.Log(_keyWord.GetFullWord());
        _keyWord *= key;
    }



    private void Talk()
    {
        //case "hard_talk":
        //    {
        //        if (_scholar.TextBox.IsTalking())
        //        {

        //            _scholar.Move.SetRotateGoal(Player.Instance.transform.position);

        //            if (!_scholar.TextBox.act && !_scholar.Select.selectable)
        //                _scholar.Select.Selectable(true);
        //        }
        //        else
        //        {
        //            _scholar.Move.SetRotateGoal(_lastRotateGoal);
        //            _scholar.Continue();
        //            Talking = false;

        //            if (!_scholar.Select.selectable)
        //                _scholar.Select.Selectable(true);
        //        }
        //        break;
        //    }

        if (!_scholar.TextBox.IsTalking())
        {
            Talking = false;
        }

    }
    


    public void Stop()
    {
        _scholar.TextBox.Clear();
    }
}
