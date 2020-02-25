using UnityEngine;


public class ScholarComputer : A_Computer
{

    public override void SetComputer()
    {
        base.SetComputer();

        Exam.SetExamController(this, true);
    }


    public override void SetScholars()
    {
        base.SetScholars();
    }

    public void Select(string key)
    {
        select = key;
        base.Select();
    }

}
