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
            case ScholarTypes.dumb:
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
            case ScholarTypes.dumb:
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
            case ScholarTypes.dumb:
                {
                    dumb.Bulling(key, strong);
                    break;
                }
        }
    }

    public void TeacherAnswer(string key, bool answer)
    {
        switch (type)
        {
            case ScholarTypes.dumb:
                {
                    dumb.TeacherAnswer(key, answer);
                    break;
                }
        }
    }

    public void StopQuestion()
    {
        switch (type)
        {
            case ScholarTypes.dumb:
                {
                    dumb.StopQuestion();
                    break;
                }
        }
    }

    public void Writing()
    {
        switch (type)
        {
            case ScholarTypes.dumb:
                {
                    dumb.Writing();
                    break;
                }
        }
    }

    public void CheckForTeacher()
    {
        switch (type)
        {
            case ScholarTypes.dumb:
                {
                    dumb.CheckForTeacher();
                    break;
                }
        }
    }
}

