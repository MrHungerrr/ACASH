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
        exam,
        afterhours,
    }

    KeyWord exam_key = new KeyWordWithoutMain("Exam_Part");

    public bool exam;


    public delegate void OnTimeDone();
    public event OnTimeDone ChillDone;
    public event OnTimeDone PrepareDone;
    public event OnTimeDone ExamDone;



    [HideInInspector]
    public part exam_part;



    public void Awake()
    {
        PrepareDone += StartExam;
        ExamDone += FinishExam;
    }


    public void ResetExam()
    {
        StartGame();
    }

    private void StartGame()
    {
        exam = false;
        exam_part = part.chill;
        TimeManager.get.SetTime(0);

        exam_key += 0;
        HUDManager.get.ExamHUD(exam_key);

        LevelSettings.get.ExamNext += ResetExam;
    }

    private void StartPrepare()
    {
        GameManager.get.NewScholars();

        exam_part = part.prepare;
        TimeManager.get.SetTime(1);
        exam_key += 1;
        HUDManager.get.ExamHUD(exam_key);
    }

    public void StartExam()
    {
        exam = true;
        exam_part = part.exam;
        TimeManager.get.SetTime(2);

        exam_key += 2;
        HUDManager.get.ExamHUD(exam_key);
    }

    public void FinishExam()
    {
        exam = false;
        exam_part = part.afterhours;

        exam_key += 3;
        HUDManager.get.ExamHUD(exam_key);
    }


    public void TimeDone()
    {
        switch(exam_part)
        {
            case part.chill:
                {
                    StartPrepare();
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
