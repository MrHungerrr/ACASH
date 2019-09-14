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

    public void TeacherPermission(string key, bool answer)
    {
        switch (type)
        {
            case "Dumb":
                {
                    dumb.TeacherPermission(key, answer);
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
                    dumb.CanCheat(Convert.ToInt32(UnityEngine.Random.Range(0, 5)));
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
                    dumb.CheatingSelection(Convert.ToInt32(UnityEngine.Random.Range(0, 5)));
                    break;
                }
        }
    }

    public void RandomAction()
    {
        switch (type)
        {
            case "Dumb":
                {
                    dumb.RandomAction(Convert.ToInt32(UnityEngine.Random.Range(0, 5)));
                    break;
                }
        }
    }
}

