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
            default:
                {
                    Debug.Log("Не настроено действие для типа - " + type);
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
                    break;
                }
            default:
                {
                    Debug.Log("Не настроено действие для типа - " + type);
                    break;
                }
        }
    }
}

