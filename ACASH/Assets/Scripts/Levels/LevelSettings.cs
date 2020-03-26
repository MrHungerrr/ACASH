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


   
    public event ActionEvent.OnAction ExamNext;
    public event ActionEvent.OnAction ExamOver;


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
            if(ExamNext != null)
                ExamNext();
        }
        else
        {
            if (ExamOver != null)
                ExamOver();
        }
    }


}
