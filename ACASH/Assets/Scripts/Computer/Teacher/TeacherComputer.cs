using ComputerActions;
using UnityEngine;


public class TeacherComputer : A_Computer
{
    [HideInInspector]
    public ComputerUIColliderManager Col;
    [HideInInspector]
    public TeacherComputerController Controller;

    public override void Setup()
    {
        Col = transform.Find("Screen").Find("UI").GetComponent<ComputerUIColliderManager>();
        Col.Setup();

        base.Setup();

        Exam.SetExamController(this);

        Controller = GetComponent<TeacherComputerController>();
        Controller.Setup();

        ExamManager.get.ExamDone += ExamDone;
    }

    public override void SetScholars()
    {
        base.SetScholars();
    }



    private void ExamDone()
    {
        Commands.Do(GetC.commands.Score);
    }
}
