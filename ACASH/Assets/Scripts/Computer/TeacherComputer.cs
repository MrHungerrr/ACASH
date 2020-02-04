using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using Cinemachine;
using UnityEngine;


public class TeacherComputer : Computer
{
    [HideInInspector]
    public ComputerUIColliderManager Col;
    [HideInInspector]
    public TeacherComputerController Controller;

    public override void SetComputer()
    {
        Col = transform.Find("Screen").Find("UI").GetComponent<ComputerUIColliderManager>();
        Col.SetColliders();

        base.SetComputer();
        Controller = GetComponent<TeacherComputerController>();
        Controller.SetTeacherComputerController();
    }

    public override void SetScholars()
    {
        base.SetScholars();
    }
}
