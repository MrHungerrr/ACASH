using UnityEngine;
using ComputerActions;


public class ScholarComputer : A_Computer
{
    [HideInInspector]
    public ScholarComputerAIController Controller { get; private set; }

    public override void Setup()
    {
        base.Setup();

        Exam.Setup(this);

        Controller = GetComponent<ScholarComputerAIController>();
        Controller.Setup();
    }






    public override void SetScholars()
    {
        base.SetScholars();
    }
}
