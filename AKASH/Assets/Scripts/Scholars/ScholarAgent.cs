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

}

