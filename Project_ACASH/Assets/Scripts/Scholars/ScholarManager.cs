using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Single;

public class ScholarManager : MonoSingleton<ScholarManager>
{

    public Scholar[] Scholars => _scholars;


    private Scholar[] _scholars;



    public void Setup()
    {
        ScholarFaces.Setup();
    }


    public void SetLevel()
    {
        ExamManager.Instance.PrepareDone.AddListener(StartExam);
        ExamManager.Instance.ExamDone.AddListener(EndExam);

        _scholars = GameObject.FindObjectsOfType<Scholar>();
    }


    public void NewScholars()
    {
        for (int i = 0; i < Scholars.Length; i++)
        {
            Scholars[i].Reset();
        }
    }


    public Scholar GetRandomScholar()
    {
        return Scholars[GetRandomScholarIndex()];
    }

    public int GetRandomScholarIndex()
    {
        return Random.Range(0, Scholars.Length);
    }


    public void StartPrepare()
    {
        GameManager.Instance.NewScholars();

        for (int i = 0; i < Scholars.Length; i++)
        {
            if (Scholars[i].Active)
                Scholars[i].Action.DoAction("Login");
        }
    }

    public void StartExam()
    {
        for (int i = 0; i < Scholars.Length; i++)
        {
            if(Scholars[i].Active)
                Scholars[i].Action.Enable();
        }
    }

    public void EndExam()
    {
        for (int i = 0; i < Scholars.Length; i++)
        {

        }
    }


    public List<Scholar> GetScholars(Vector3 point, float range)
    {
        List<Scholar> list = new List<Scholar>();

        for(int i = 0; i < Scholars.Length; i++)
        {
            float distance = Vector3.Distance(point, Scholars[i].Move.Position());

            if (distance <= range)
                list.Add(Scholars[i]);
        }

        return list;
    }
}