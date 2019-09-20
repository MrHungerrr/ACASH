using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatHelper : MonoBehaviour
{
    [HideInInspector]
    public CheatingScript[] student = new CheatingScript[32];
    public int studentcount = 0;
    
    public static int cheatingCount = 0;
    [Range(0, 32)]
    public int cheatingCountonLevel; 
    public static bool allowcheating = true;


    void Start()
    {
        student = FindObjectsOfType<CheatingScript>();
        for (int i = 0; i < student.Length; i++)
        {
            if (student[i] != null)
            {
                studentcount++;
            }
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

     public void StressCollection()
     {
        /*for (int i = 0; i < cheatingCountonLevel; i++)
        {
            Debug.Log(student[i].gameObject + " " + student[i].stress);
        }
        */
    }
}
