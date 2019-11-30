using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using N_BH;


public class StudentStress : Singleton<StudentStress>
{
    private StudentStressAgent[] SSAgents;

    public void Set()
    {
        SSAgents = FindObjectsOfType<StudentStressAgent>();
        for (int i = 0; i < SSAgents.Length; i++)
            SSAgents[i].Set();
    }


    public void Refresh()
    {
        for (int i = 0; i < SSAgents.Length; i++)
            SSAgents[i].Refresh();
    }
}
