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
        //Какой то общий сбор учеников;
    }



    void Update()
    {
        Debug.Log(cheatingCount);
        if (cheatingCount < cheatingCountonLevel)
        {
            allowcheating = true;
        }
        else
        {
            allowcheating = false;
            Debug.Log("Вас и так слишком много! ");
        }
    }
}
