using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;


public class TeacherComputer : Computer
{
    [HideInInspector]
    public TeacherComputerController Controller;

    public override void SetComputer()
    {
        base.SetComputer();
        Controller = GetComponent<TeacherComputerController>();
        Controller.SetTeacherComputerController();
    }

    public override void SetScholars()
    {
        base.SetScholars();
    }
}
