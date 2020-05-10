using UnityEngine;
using UnityEngine.Events;
using Single;


public class LevelSettings: Singleton<LevelSettings>
{
    public enum difficultyes
    {
        Easy,
        Normal,
        Hard
    }

    private enum next_exam_mode
    {
        Auto,
        Manual,
    }





    public difficultyes difficultyType;
    [SerializeField]
    private next_exam_mode nextExam;

    [HideInInspector]
    public UnityEvent ExamNext;
    [HideInInspector]
    public UnityEvent ExamOver;


    [Range(0,20)]
    public int actionsCount;

    [SerializeField]
    [Range(0, 20)]
    private int examsCount;

    private int exam_index;


    public void Setup()
    {
        switch(nextExam)
        {
            case next_exam_mode.Auto:
                {
                    ExamNext.AddListener(ExamManager.get.ResetExam);
                    break;
                }
            case next_exam_mode.Manual:
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
