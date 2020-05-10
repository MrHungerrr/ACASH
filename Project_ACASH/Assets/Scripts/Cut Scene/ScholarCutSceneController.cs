using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScholarOptions;
using Single;

public class ScholarCutSceneController
{
    [HideInInspector]
    public Scholar[] scholars { get; private set; }


    private void Teleport()
    {
        for (int i = 0; i < scholars.Length; i++)
        {
            scholars[i].Location.Teleport(PlaceManager.place.Desk, i);
        }
    }



    public void Setup()
    {
        scholars = ScholarManager.get.scholars;
        Teleport();
    }



    public void ResetScholars()
    {
        for (int i = 0; i < scholars.Length; i++)
        {
            if (scholars[i].scholarType == ScholarTypes.list.Random)
            {
                int rand = Random.Range(0, ScholarTypes.length);
                scholars[i].SetNewType((ScholarTypes.list)rand);

            }
            else
            {
                scholars[i].ResetType();
            }
        }
    }



    /*
    private void ScholarNumberRandomer()
    {
        for (int i = 0; i < scholars.Length; i++)
        {
            int buf = Random.Range(0, scholars.Length);

            if (i != buf)
            {
                var buf2 = scholars[i];
                scholars[i] = scholars[buf];
                scholars[buf] = buf2;
            }
        }
    }
    */



    public void StartWorking()
    {
        for (int i = 0; i < scholars.Length; i++)
        {
            if (scholars[i].active)
            {
                scholars[i].Action.Reset("Write");
                AddWork(scholars[i], 10);
            }
        }
    }



    public void AddWork(Scholar Scholar, int length)
    {
        for(int i = 0; i < length; i ++)
        {
            Scholar.Action.AddAction("Write");
        }
    }



    public void End()
    {
        for (int i = 0; i < scholars.Length; i++)
        {
            if (i != 119)
            {
                scholars[i].Disable();
                scholars[i].Emotions.Change(GetS.faces.Dead);
            }
        }
    }

    public void Off()
    {
        for (int i = 0; i < scholars.Length; i++)
        {
            if (i != 119)
            {
                scholars[i].Body.Disable();
            }
        }
    }

    public void LastStandingManDisableVeryBad()
    {
        scholars[119].Disable();
    }

    public void LastStandingManDeadVeryBad()
    {
        scholars[119].Emotions.Change(GetS.faces.Dead);
    }


}