using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using Single;

public class ExamManager : Singleton<ExamManager>
{

    public enum part
    {
        Prepare,
        Exam,
        Afterhours,
    }

    public bool Exam => _exam;

    public UnityEvent PrepareDone = new UnityEvent();
    public UnityEvent ExamDone = new UnityEvent();


    private bool _exam;
    private part _examPart;


    public void SetLevel()
    {
        PrepareDone.AddListener(StartExam);
        ExamDone.AddListener(FinishExam);
    }


    public void UnsetLevel()
    {
        PrepareDone.RemoveAllListeners();
        ExamDone.RemoveAllListeners();
    }


    public void ResetExam()
    {
        StartPrepare();
    }

    private void StartPrepare()
    {
        _exam = false;
        _examPart = part.Prepare;
    }

    private void StartExam()
    {
        _exam = true;
        _examPart = part.Exam;
        TimeManager.Instance.SetTime(300);
        TimeManager.Instance.OnTimeDone += ExamDone.Invoke;
    }

    private void FinishExam()
    {
        _exam = false;
        _examPart = part.Afterhours;
        TimeManager.Instance.Disable();
    }
}
