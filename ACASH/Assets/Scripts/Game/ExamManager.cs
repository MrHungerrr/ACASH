using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Single;

public class ExamManager : Singleton<ExamManager>
{

    public enum part
    {
        Chill,
        Prepare,
        Exam,
        Afterhours,
    }

    KeyWord exam_key = new KeyWordWithoutMain("Exam_Part");

    public bool exam;


    public event ActionEvent.OnAction ChillDone;
    public event ActionEvent.OnAction PrepareDone;
    public event ActionEvent.OnAction ExamDone;


    [HideInInspector]
    public part exam_part;



    public void Awake()
    {
        ChillDone += StartPrepare;
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
        exam_part = part.Chill;
        TimeManager.get.SetTime(exam_part);

        exam_key += 0;
        HUDManager.get.ExamHUD(exam_key);
    }

    private void StartPrepare()
    {
        exam_part = part.Prepare;
        TimeManager.get.SetTime(exam_part);
        exam_key += 1;
        HUDManager.get.ExamHUD(exam_key);
    }

    public void StartExam()
    {
        exam = true;
        exam_part = part.Exam;
        TimeManager.get.SetTime(exam_part);
        ScholarActionTime.get.Setup();

        exam_key += 2;
        HUDManager.get.ExamHUD(exam_key);
    }

    public void FinishExam()
    {
        exam = false;
        exam_part = part.Afterhours;
        TimeManager.get.SetTime(exam_part);
        TimeManager.get.Enable(false);

        exam_key += 3;
        HUDManager.get.ExamHUD(exam_key);
    }


    public void TimeDone()
    {
        switch(exam_part)
        {
            case part.Chill:
                {
                    if (ChillDone != null)
                        ChillDone();

                    break;
                }
            case part.Prepare:
                {
                    if(PrepareDone != null)
                        PrepareDone();

                    break;
                }
            case part.Exam:
                {
                    if(ExamDone != null)
                        ExamDone();

                    break;
                }
        }
    }
}
