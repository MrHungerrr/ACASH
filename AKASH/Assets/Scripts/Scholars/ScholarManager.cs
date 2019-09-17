using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScholarManager : MonoBehaviour
{

    private Scholar[] scholars;
    [HideInInspector]
    public Transform[] toilets;
    public Transform[] desks;



    private void Awake()
    {
        scholars = GameObject.FindObjectsOfType<Scholar>();

        var buf = GameObject.FindGameObjectsWithTag("ScholarDesk");

        desks = new Transform[buf.Length];

        for (int i = 0; i < buf.Length; i++)
        {
            desks[i] = buf[i].transform;
        }

        ScholarDeskSort();
        ScholarNomberRandomer();
    }


    void Start()
    {
        
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
        for (int i = 0; i < (desks.Length - 1); i++)
            for (int i2 = 0; i2 < (desks.Length - 1 - i); i2++)
            {
                if ((desks[i2].position.x > desks[i2 + 1].position.x) || (desks[i2].position.x == desks[i2 + 1].position.x && desks[i2].position.z < desks[i2 + 1].position.z))
                {
                    var buf = desks[i2 + 1];
                    desks[i2 + 1] = desks[i2];
                    desks[i2] = buf;
                }
            }


        for (int i = 0; i < desks.Length; i++)
        {
            desks[i].name = "Desk_" + i;
        }
    }

    private void ScholarNomberRandomer()
    {
        for (int i = 0; i < scholars.Length; i++)
        {
            int buf = Random.Range(0, scholars.Length - 1);

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
}