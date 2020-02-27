using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using Cinemachine;
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
        Col.SetColliders();

        base.Setup();

        Exam.SetExamController(this);

        Controller = GetComponent<TeacherComputerController>();
        Controller.SetTeacherComputerController();
    }

    public override void SetScholars()
    {
        base.SetScholars();
    }
}
