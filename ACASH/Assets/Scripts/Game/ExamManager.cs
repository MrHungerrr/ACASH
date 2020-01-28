using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using N_BH;

public class ExamManager : Singleton<ExamManager>
{

    [HideInInspector]
    public string exam_part;

    public Dictionary<string, bool> banned = new Dictionary<string, bool>()
    {
        { "Pen_", false },
        { "Calculator_", false },
        { "Talking_", false },
        { "Cheating_", false },
        { "Walking_", false },
    };


    private void Awake()
    {
        
    }


    public void ResetExam()
    {
        StartChill();
    }

    private void StartChill()
    {
        exam_part = "chill";
        TimeManager.get.SetTime(0);
        Debug.Log("StartChill");
    }

    private void StartPrepare()
    {
        exam_part = "prepare";
        ScholarManager.get.StartPrepare();
        TimeManager.get.SetTime(1);
        Debug.Log("StartPrepare");
    }

    public void StartExam()
    {
        exam_part = "exam";
        ScholarManager.get.StartExam();
        TimeManager.get.SetTime(2);
        Debug.Log("StartExam");
    }


    public void TimeDone()
    {
        switch(exam_part)
        {
            case "chill":
                {
                    StartPrepare();
                    break;
                }
            case "prepare":
                {

                    break;
                }
            case "exam":
                {
                    ExamDone();
                    break;
                }
        }
    }

    public void ExamDone()
    {
        Debug.Log("ExamDone");
    }

}
