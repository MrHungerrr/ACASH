using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatHelper : MonoBehaviour
{

    CheatingScript[] student = new CheatingScript[32];
    
    public static int cheatingCount = 0;
    [Range(0, 32)]
    public int cheatingCountonLevel; 
    public static bool allowcheating = true;


    void Start()
    {
        student = FindObjectsOfType<CheatingScript>();
        for (int i = 0; i < 3; i++)
        {
            Debug.Log(student[i].gameObject + " " + student[i].stress);
        }
    }


    void Update()
    {
        //Debug.Log(cheatingCount);
        if (cheatingCount < cheatingCountonLevel)
        {
            allowcheating = true;
        }
        else
        {
            allowcheating = false;
            //Debug.Log("Пока списывающих достаточно! ");
        }
    }

    void StressCollection()
    {
        
    }
}
