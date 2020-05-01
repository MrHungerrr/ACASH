using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
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

    KeyWord exam_key = new KeyWord("Exam", "Part");

    public bool exam;


    [HideInInspector]
    public UnityEvent ChillDone;
    [HideInInspector]
    public UnityEvent PrepareDone;
    [HideInInspector]
    public UnityEvent ExamDone;


    [HideInInspector]
    public part exam_part;


    public void Setup()
    {
        Player.get.Talk.ExecuteDone.AddListener(EarlyFinish);
    }

    public void SetLevel()
    {
        ChillDone.AddListener(StartPrepare);
        PrepareDone.AddListener(StartExam);
        ExamDone.AddListener(FinishExam);
    }


    public void UnsetLevel()
    {
        ChillDone.RemoveAllListeners();
        PrepareDone.RemoveAllListeners();
        ExamDone.RemoveAllListeners();
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
        switch (exam_part)
        {
            case part.Chill:
                {
                    ChillDone.Invoke();

                    break;
                }
            case part.Prepare:
                {
                    PrepareDone.Invoke();

                    break;
                }
            case part.Exam:
                {
                    ExamDone.Invoke();

                    break;
                }
        }
    }



    public void EarlyFinish()
    {
        if(ScholarManager.get.GetCount(ScholarManager.Left) == 0)
        {
            ExamDone.Invoke();
        }
    }
}
