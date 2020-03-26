using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Single;

public class ScholarManager : Singleton<ScholarManager>
{
    [HideInInspector]
    public Scholar[] scholars;


    [HideInInspector]
    public int cheating_count;


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
        scholars = GameObject.FindObjectsOfType<Scholar>();
        int number = 0;

        for (int i = 0; i < scholars.Length; i++)
        {
            scholars[i].Setup();

            if (!scholars[i].disabled)
            {
                scholars[i].Info.SetNumber(number);
                scholars[i].Move.Position(PlaceManager.get.GetPlace(PlaceManager.place.Home, number));
                number++;
            }
        }

        ExamManager.get.ChillDone += StartPrepare;
        ExamManager.get.PrepareDone += StartExam;
        ExamManager.get.ExamDone += EndExam;
    }


    public void NewScholars()
    {
        for (int i = 0; i < scholars.Length; i++)
        {
            if (!scholars[i].disabled)
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
            if (scholars[i].active && !scholars[i].disabled)
                scholars[i].Action.DoAction("Login");
        }
    }

    public void StartExam()
    {
        for (int i = 0; i < scholars.Length; i++)
        {
            if(scholars[i].active && !scholars[i].disabled)
                scholars[i].Action.Enable();
        }
    }

    public void EndExam()
    {
        for (int i = 0; i < scholars.Length; i++)
        {
            if (scholars[i].active && !scholars[i].disabled)
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


    public Scholar[] GetScholars(Vector3 point, float range)
    {
        List<Scholar> result = new List<Scholar>();

        for(int i = 0; i < scholars.Length; i++)
        {
            float distance = Vector3.Distance(point, scholars[i].Move.Position());

            if (distance <= range)
                result.Add(scholars[i]);
        }

        return result.ToArray();
    }

    public Scholar[] GetVisibleScholars()
    {
        List<Scholar> result = new List<Scholar>();

        for (int i = 0; i < scholars.Length; i++)
        {
            if (!scholars[i].Senses.T_behind_wall && scholars[i].active)
                result.Add(scholars[i]);
        }

        return result.ToArray();
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