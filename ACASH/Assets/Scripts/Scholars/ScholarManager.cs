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
    public Transform near_toilet;
    [HideInInspector]
    public Transform[,] desks;
    [HideInInspector]
    public Transform[,] toilets;
    [HideInInspector]
    public Transform[] outside;
    [HideInInspector]
    public Transform[,] sinks;


    [HideInInspector]
    public int toilets_count;
    [HideInInspector]
    public int outside_count;
    [HideInInspector]
    public int sinks_count;
    [HideInInspector]
    public int desks_count;


    private bool[] outside_busy;
    private bool[] toilets_busy;
    private bool[] sinks_busy;


    [HideInInspector]
    public Transform teacher_room;
    [HideInInspector]
    public bool teacher_room_busy;

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
        var buf = GameObject.FindGameObjectsWithTag("Sink");
        sinks = new Transform[2, buf.Length];
        sinks_busy = new bool[buf.Length];
        sinks_count = buf.Length;

        for (int i = 0; i < sinks_count; i++)
        {
            sinks[1, i] = buf[i].transform;
            sinks[0, i] = buf[i].transform.parent.transform.Find("Destonation");
        }


        buf = GameObject.FindGameObjectsWithTag("Toilet");
        toilets = new Transform[2, buf.Length];
        toilets_busy = new bool[buf.Length];
        toilets_count = buf.Length;

        for (int i = 0; i < toilets_count; i++)
        {
            toilets[1, i] = buf[i].transform;
            toilets[0, i] = buf[i].transform.parent.transform.Find("Destonation");
        }


        buf = GameObject.FindGameObjectsWithTag("Outside");
        outside = new Transform[buf.Length];
        outside_busy = new bool[buf.Length];
        outside_count = buf.Length;

        for (int i = 0; i < outside_count; i++)
        {
            outside[i] = buf[i].transform;
            outside[i].gameObject.GetComponent<Renderer>().enabled = false;
        }


        DeskManager.get.SetDeskManager();
        desks = new Transform[2, DeskManager.get.desks.Length];
        desks_count = DeskManager.get.desks.Length;

        for (int i = 0; i < desks_count; i++)
        {
            ScholarComputer desk = DeskManager.get.desks[i];
            desks[1, i] = desk.transform;
            desks[0, i] = desk.transform.Find("Destonation");
            //Debug.Log(desks[1, i].position);
            //Debug.Log(desks[0, i].position);
        }

        scholars = GameObject.FindObjectsOfType<Scholar>();
        withoutScholars = (scholars.Length == 0);

        for (int i = 0; i < scholars.Length; i++)
        {
            scholars[i].SetNumber(i);
        }
    }

    public void SetScholars()
    {

        //Рандомные ученики?
        //ScholarNumberRandomer();
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

    public Scholar GetRandomScholar()
    {
        int rand = Random.Range(0, scholars.Length);
        return scholars[rand];
    }

    public int GetRandomScholarIndex()
    {
        return Random.Range(0, scholars.Length);
    }

    public int IsFree(string place, int a)
    {
        switch (place)
        {
            case "toilet":
                {
                    for (int i = 0; i < toilets_count; i++)
                    {
                        if (!toilets_busy[a])
                            return a;
                        a = (a + 1) % toilets_count;
                    }
                    break;
                }
            case "sink":
                {
                    for (int i = 0; i < sinks_count; i++)
                    {
                        if (!sinks_busy[a])
                            return a;
                        a = (a + 1) % sinks_count;
                    }
                    break;
                }
            case "outside":
                for (int i = 0; i < outside_count; i++)
                {
                    if (!outside_busy[a])
                        return a;
                    a = (a + 1) % outside_count;
                }
                break;
        }

        return -1;
    }

    public Vector3 GetPlace(string place, int i)
    {
        switch (place)
        {
            case "toilet":
                {
                    return toilets[0, i].position;
                }
            case "sink":
                {
                    return sinks[0, i].position;
                }
            case "outside":
                {
                    return outside[i].position;
                }
            default:
                {
                    return Vector3.zero;
                }
        }
    }

    public Vector3 GetPlace(string place)
    {
        switch (place)
        {
            case "toilet":
                {
                    return toilets[0, i].position;
                }
            case "sink":
                {
                    return sinks[0, i].position;
                }
            case "outside":
                {
                    return outside[i].position;
                }
            default:
                {
                    return Vector3.zero;
                }
        }
    }

    public Vector3 GetSightGoal(string place, int i)
    {
        switch (place)
        {
            case "toilet":
                {
                    return toilets[1, i].position;
                }
            case "sink":
                {
                    return sinks[1, i].position;
                }
            case "outside":
                {
                    var buf = outside[i].position;
                    buf += new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1));
                    return buf;
                }
            default:
                {
                    return Vector3.zero;
                }
        }
    }




    public bool IsCheatFree()
    {
        if (cheating_count < cheating_limit)
            return true;
        else
            return false;
    }




    private IEnumerator ScholarsAction(float time)
    {
        yield return new WaitForSeconds(time);

        Debug.Log("Пошел Экшон!");
        ActionsGiver.GiveActions(Difficulty.get.actions[2]);

        StartCoroutine(ScholarsAction(Random.Range(5, 15)));
    }


    private IEnumerator PrepareForTest()
    {
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < scholars.Length; i++)
        {
            scholars[i].Action.Doing("Go_Home");
        }

        while(true)
        {
            yield return new WaitForSeconds(1f);
            float buf = 0;

            for (int i = 0; i < scholars.Length; i++)
            {
                if (!scholars[i].Action.doing)
                    buf++;
            }

            if (buf == scholars.Length)
                break;
        }
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
            Debug.Log(i);
            scholars[i].Action.StartWriting();
            scholars[i].chill = false;
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