using UnityEngine;
using Single;


public class LevelSettings: Singleton<LevelSettings>
{
    public enum difficultyes
    {
        Easy,
        Normal,
        Hard
    }

    public difficultyes difficultyType;


   
    public delegate void OnExamDone();
    public event OnExamDone ExamNext;
    public event OnExamDone ExamOver;


    [Range(0,20)]
    public int actionsCount;

    [SerializeField]
    [Range(0, 20)]
    private int examsCount;

    private int exam_index;


    public void Setup()
    {
        exam_index = 1;
    }

    public void NextExam()
    {
        if( exam_index < examsCount)
        {
            exam_index++;
            ExamNext();
        }
        else
        {
            ExamOver();
        }
    }


}
