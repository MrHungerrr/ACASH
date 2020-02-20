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




    [HideInInspector]
    public Dictionary<string, int> scholar_type_count = new Dictionary<string, int>()
    {
        { ScholarTypes.asshole, 0 },
        { ScholarTypes.dumb, 0 },
        { ScholarTypes.nerd, 0 },
    };


    public void SetLevel()
    {
        scholars = GameObject.FindObjectsOfType<Scholar>();
        withoutScholars = (scholars.Length == 0);

        for (int i = 0; i < scholars.Length; i++)
        {
            scholars[i].Info.SetNumber(i);
        }
    }

    public void SetScholars()
    {

        //Рандомные ученики?
        //ScholarNumberRandomer();
    }


    public void Shout(int value)
    {
        Stress(value);
    }

    public void Stress(int value)
    {
        for (int i = 0; i < scholars.Length; i++)
        {
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



    private IEnumerator ScholarsAction(float time)
    {
        yield return new WaitForSeconds(time);

        Debug.Log("Пошел Экшон!");
        ActionsGiver.GiveActions(Difficulty.get.actions[2]);
    }


    private IEnumerator PrepareForTest()
    {
        yield return new WaitForSeconds(1f);
    }


    public void StartPrepare()
    {
        StartCoroutine(PrepareForTest());
    }
    
    public void StartExam()
    {
        StartCoroutine(ScholarsAction(10f));
        for (int i = 0; i < scholars.Length; i++)
        {
            scholars[i].Action.Reset();
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
}