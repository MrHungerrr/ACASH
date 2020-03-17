using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Single;

public class ScholarManager : Singleton<ScholarManager>
{
    [HideInInspector]
    public Scholar[] scholars;
    private bool withoutScholars;


    [HideInInspector]
    public int cheating_count;
    private int cheating_limit = 2;


    [HideInInspector]
    public int left;
    [HideInInspector]
    public int cheated;
    [HideInInspector]
    public int finished;


    //Emotions
    public Texture ussual;
    public Texture happy;
    public Texture smile;
    public Texture sad;
    public Texture upset;
    public Texture suprised;
    public Texture ask;
    public Texture dead;


    public void Setup()
    {
        var scholars_buf = GameObject.FindObjectsOfType<Scholar>();
        List<Scholar> scholars_list = new List<Scholar>();

        foreach(Scholar s in scholars_buf)
        {
            s.Setup();

            if (!s.disabled)
                scholars_list.Add(s);
        }

        scholars = scholars_list.ToArray();

        withoutScholars = (scholars.Length == 0);

        for (int i = 0; i < scholars.Length; i++)
        {
            scholars[i].Info.SetNumber(i);
        }

        ExamManager.get.ChillDone += StartPrepare;
        ExamManager.get.PrepareDone += StartExam;
        ExamManager.get.ExamDone += EndExam;
    }


    public void NewScholars()
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






    public void Shout(int value)
    {
        Stress(value);
    }

    public void Stress(int value)
    {
        for (int i = 0; i < scholars.Length; i++)
        {
            if(scholars[i].active)
                scholars[i].Stress.Change(value);
        }
    }



    public int[] GetStress()
    {
        int[] buf = new int[scholars.Length];

        for (int i = 0; i < scholars.Length; i++)
        {
            buf[i] = scholars[i].Stress.value;
        }

        return buf;
    }




    public int GetStress(int scholarNum)
    {
        return scholars[scholarNum].Stress.value;
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

    public Scholar GetRandomScholar()
    {
        return scholars[GetRandomScholarIndex()];
    }

    public int GetRandomScholarIndex()
    {
        return Random.Range(0, scholars.Length);
    }


    public void StartPrepare()
    {
        for (int i = 0; i < scholars.Length; i++)
        {
            if (scholars[i].active)
                scholars[i].Action.DoAction("Login");
        }
    }

    public void StartExam()
    {
        for (int i = 0; i < scholars.Length; i++)
        {
            if(scholars[i].active)
                scholars[i].Action.Enable();
        }
    }

    public void EndExam()
    {
        for (int i = 0; i < scholars.Length; i++)
        {
            if (scholars[i].active)
                scholars[i].Execute.EndExamForScholar();
        }
    }



    public void Hear(float distance)
    {
        for (int i = 0; i < scholars.Length; i++)
        {
            scholars[i].Senses.Hear(distance);
        }
    }

    public void SpecialHear(Vector3 pos)
    {
        for (int i = 0; i < scholars.Length; i++)
        {
            scholars[i].Senses.SpecialHear(pos);
        }
    }





    public delegate bool Count(Scholar scholar);

    public static bool Cheated(Scholar scholar)
    {
        return !scholar.Execute.executed && scholar.Cheat.IsCheated();
    }

    public static bool Left(Scholar scholar)
    {
        return !scholar.Execute.executed;
    }

    public static bool NotFinished(Scholar scholar)
    {
        return !scholar.Execute.executed && !scholar.Test.finished;
    }

    public int GetCount(Count Type)
    {
        int result = 0;

        for (int i = 0; i < scholars.Length; i++)
        {
            if (Type(scholars[i]))
                result++;
        }

        return result;
    }
}