using UnityEngine;
using UnityEngine.Events;
using UnityTools.Single;
using Exam;


public class LevelSettings : MonoSingleton<LevelSettings>
{
    public enum difficultyes
    {
        Easy,
        Normal,
        Hard
    }

    private enum nextExamMode
    {
        Auto,
        Manual,
    }


    public difficultyes difficultyType;
    [SerializeField]
    private nextExamMode nextExam;


    public UnityEvent ExamNext { get; } = new UnityEvent();
    public UnityEvent ExamOver { get; } = new UnityEvent();


    [Range(0,20)]
    public int actionsCount;

    [SerializeField]
    [Range(0, 20)]
    private int examsCount;

    private int exam_index;


    public void SetLevel()
    {
        switch(nextExam)
        {
            case nextExamMode.Auto:
                {
                    ExamNext.AddListener(ExamManager.Instance.ResetExam);
                    break;
                }
            case nextExamMode.Manual:
                {
                    break;
                }
        }

        exam_index = 1;
    }

    public void RestartExam()
    {
        ExamNext.Invoke();
    }

    public void NextExam()
    {
        if (exam_index < examsCount)
        {
            exam_index++;
            ExamNext.Invoke();
        }
        else
        {
            ExamOver.Invoke();
        }
    }


}
