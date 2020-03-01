using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Single;

public class ExamManager : Singleton<ExamManager>
{

    public enum part
    {
        chill,
        prepare,
        exam
    }

    public bool exam;


    public delegate void OnTimeDone();
    public event OnTimeDone ChillDone;
    public event OnTimeDone PrepareDone;
    public event OnTimeDone ExamDone;



    [HideInInspector]
    public part exam_part;



    public void Awake()
    {
        ChillDone += StartPrepare;
        PrepareDone += StartExam;
    }


    public void ResetExam()
    {
        StartChill();
        GameManager.get.SetScholars();
    }

    private void StartChill()
    {
        exam = false;
        exam_part = part.chill;
        TimeManager.get.SetTime(0);
    }

    private void StartPrepare()
    {
        exam_part = part.prepare;
        TimeManager.get.SetTime(1);
    }

    public void StartExam()
    {
        exam = true;
        exam_part = part.exam;
        TimeManager.get.SetTime(2);
    }


    public void TimeDone()
    {
        switch(exam_part)
        {
            case part.chill:
                {
                    ChillDone();
                    break;
                }
            case part.prepare:
                {
                    PrepareDone();
                    break;
                }
            case part.exam:
                {
                    ExamDone();
                    break;
                }
        }
    }

}
