using UnityEngine;
using System;


public class ScholarAgent
{
    private string type;
    private Dumb dumb;

    public ScholarAgent(string t, Scholar s)
    {
        type = t;
        switch (type)
        {
            case "Dumb":
                {
                    dumb = new Dumb(s);
                    break;
                }
        }
    }



    public void HearBulling(bool strong)
    {
        switch (type)
        {
            case "Dumb":
                {
                    dumb.HearBulling(strong);
                    break;
                }
        }
    }

    public void Bulling(string key, bool strong)
    {
        switch (type)
        {
            case "Dumb":
                {
                    dumb.Bulling(key, strong);
                    break;
                }
        }
    }

    public void BullingForSubjects(string key, string obj)
    {
        switch (type)
        {
            case "Dumb":
                {
                    dumb.BullingForSubjects(key, obj);
                    break;
                }
        }
    }

    public void TeacherAnswer(string key, bool answer)
    {
        switch (type)
        {
            case "Dumb":
                {
                    dumb.TeacherAnswer(key, answer);
                    break;
                }
        }
    }

    public void Writing()
    {
        switch (type)
        {
            case "Dumb":
                {
                    dumb.Writing();
                    break;
                }
        }
    }

    public void CheatNeed()
    {
        switch (type)
        {
            case "Dumb":
                {
                    dumb.CheatNeed();
                    break;
                }
        }
    }

    public void CanCheat()
    {
        switch (type)
        {
            case "Dumb":
                {
                    dumb.CanCheat();
                    break;
                }
        }
    }

    public void CheatingSelection()
    {
        switch (type)
        {
            case "Dumb":
                {
                    dumb.CheatingSelection();
                    break;
                }
        }
    }

    public void RandomSimpleAction()
    {
        switch (type)
        {
            case "Dumb":
                {
                    dumb.RandomSimpleAction();
                    break;
                }
        }
    }

    public void RandomSpecialAction()
    {
        switch (type)
        {
            case "Dumb":
                {
                    dumb.RandomSpecialAction();
                    break;
                }
        }
    }
}

