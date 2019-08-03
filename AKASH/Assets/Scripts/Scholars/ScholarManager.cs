using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScholarManager : MonoBehaviour
{

    private GameObject[] scholar;
    private Dictionary<int, Dumb> dumb = new Dictionary<int, Dumb>();
    private Dictionary<string, int> scholar_quantity = new Dictionary<string, int>()
    {
        { "All", 0},
        { "Dumb", 0},
    };


    private void Awake()
    {
        scholar = GameObject.FindGameObjectsWithTag("Scholar");

        scholar_quantity["All"] = scholar.Length;

        for (int i = 0; i < scholar_quantity["All"]; i++)
        {
            scholar[i] = scholar[i].transform.Find("Scholar").transform.Find("Model").gameObject;
            scholar_quantity[scholar[i].tag]++;
        }

        for (int i = 0; i < scholar_quantity["All"]; i++)
        {
            switch(scholar[i].tag)
            {
                case "Dumb":
                    {
                        dumb.Add(i, scholar[i].GetComponent<Dumb>());
                        break;
                    }
            }
        }

    }


    void Start()
    {
        
    }


    public void Stress(int value)
    {
        foreach (var student in dumb.Values)
        {
            student.Stress(value);
        }
    }
}
