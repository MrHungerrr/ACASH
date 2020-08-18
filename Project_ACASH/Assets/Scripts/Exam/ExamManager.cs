using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using GameTime;
using UnityEngine;
using Single;
using Overwatch.Watchable;
using Overwatch;
using Overwatch.Read;

public class ExamManager : Singleton<ExamManager>
{

    public enum part
    {
        Prepare,
        Exam,
        Afterhours,
    }

    public bool Exam => _exam;

    public UnityEvent PrepareDone { get; } = new UnityEvent();
    public UnityEvent ExamDone { get; }  = new UnityEvent();


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
        TimeManager.Instance.SetTimer(10);
        TimeManager.Instance.Timer.OnTimeDone += ExamDone.Invoke;
    }

    private void FinishExam()
    {
        _exam = false;
        _examPart = part.Afterhours;
    }
}
