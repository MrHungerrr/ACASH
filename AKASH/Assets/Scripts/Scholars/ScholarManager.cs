using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScholarManager : MonoBehaviour
{
    [HideInInspector]
    public Scholar[] scholars;

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
    public Dictionary<string, int> scholar_type_count = new Dictionary<string, int>()
    {
        { "Asshole", 0 },
        { "Dumb", 0 },
        { "Underdog", 0 },
    };




    private void Awake()
    {
        scholars = GameObject.FindObjectsOfType<Scholar>();


        var buf = GameObject.FindGameObjectsWithTag("Sink");
        sinks = new Transform[2,buf.Length];
        sinks_busy = new bool[buf.Length];
        sinks_count = buf.Length;

        for (int i = 0; i < buf.Length; i++)
        {
            sinks[1, i] = buf[i].transform;
            sinks[0, i] = buf[i].transform.parent.transform.Find("Destonation");
        }


        buf = GameObject.FindGameObjectsWithTag("Toilet");
        toilets = new Transform[2,buf.Length];
        toilets_busy = new bool[buf.Length];
        toilets_count = buf.Length;

        for (int i = 0; i < buf.Length; i++)
        {
            toilets[1, i] = buf[i].transform;
            toilets[0, i] = buf[i].transform.parent.transform.Find("Destonation");
        }


        buf = GameObject.FindGameObjectsWithTag("Outside");
        outside = new Transform[buf.Length];
        outside_busy = new bool[buf.Length];
        outside_count = buf.Length;

        for (int i = 0; i < buf.Length; i++)
        {
            outside[i] = buf[i].transform;
            outside[i].gameObject.GetComponent<Renderer>().enabled = false;
        }


        buf = GameObject.FindGameObjectsWithTag("ScholarDesk");
        desks = new Transform[2, buf.Length];
        desks_count = buf.Length;

        for (int i = 0; i < buf.Length; i++)
        {
            desks[1, i] = buf[i].transform;
            desks[0, i] = buf[i].transform.parent.transform.Find("Destonation");
        }

        ScholarDeskSort();
    }



    void Start()
    {
        ScholarNomberRandomer();
    }



    public void Stress(int value)
    {
        for( int i = 0; i< scholars.Length; i++)
        {
            scholars[i].Stress(value);
        }
    }



    public int[] GetStress()
    {
        int[] buf = new int[scholars.Length];

        for (int i = 0; i < scholars.Length; i++)
        {
            buf[i] = scholars[i].stress;
        }

        return buf;
    }




    public int GetStress(int scholarNom)
    {
        return scholars[scholarNom].stress;
    }



    private void ScholarDeskSort()
    {
        for (int i = 0; i < (desks_count - 1); i++)
            for (int i2 = 0; i2 < (desks_count - 1 - i); i2++)
            {
                if ((desks[1, i2].position.x > desks[1, i2 + 1].position.x) || (desks[1, i2].position.x == desks[1, i2 + 1].position.x && desks[1, i2].position.z < desks[1, i2 + 1].position.z))
                {
                    var buf0 = desks[0, i2 + 1];
                    var buf1 = desks[1, i2 + 1];
                    desks[0, i2 + 1] = desks[0, i2];
                    desks[1, i2 + 1] = desks[1, i2];
                    desks[0, i2] = buf0;
                    desks[1, i2] = buf1;
                }
            }


        for (int i = 0; i < desks_count; i++)
        {
            desks[1,i].name = "Desk_" + i;
            Debug.Log(desks[1, i].position);
        }
    }



    private void ScholarNomberRandomer()
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

        for (int i = 0; i < scholars.Length; i++)
        {
            scholars[i].SetNomber(i);
        }
    }

    public int IsFree(string place, int a)
    {
        switch(place)
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


}