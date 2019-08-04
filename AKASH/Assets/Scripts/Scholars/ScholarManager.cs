using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScholarManager : MonoBehaviour
{

    private Scholar[] scholar;



    private void Awake()
    {
        scholar = GameObject.FindObjectsOfType<Scholar>();
    }


    void Start()
    {
        
    }


    public void Stress(int value)
    {
        for( int i = 0; i< scholar.Length; i++)
        {
            scholar[i].Stress(value);
        }
    }
}
