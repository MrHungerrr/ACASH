using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using Cinemachine;


public abstract class A_Computer : MonoBehaviour
{
    [HideInInspector]
    public ComputerWindows Windows;
    [HideInInspector]
    public ComputerCommands Commands;
    [HideInInspector]
    public LoginController Login;
    [HideInInspector]
    public DesktopController Desktop;
    [HideInInspector]
    public StudentStress SS;
    [HideInInspector]
    public NumpadController Numpad;
    [HideInInspector]
    public CalculatorController Calculator;
    [HideInInspector]
    public QuestionController Question;
    [HideInInspector]
    public ExamController Exam;

    [HideInInspector]
    public string select;


    public virtual void SetComputer()
    {
        Transform win = transform.Find("Screen").Find("UI").Find("Canvas").Find("Windows");
        Numpad = win.parent.Find("Screen").GetComponentInChildren<NumpadController>();

        Login = win.GetComponent<LoginController>();
        Login.SetLoginController(this);

        Desktop = win.GetComponent<DesktopController>();
        Desktop.SetDesktopController();

        SS = win.GetComponent<StudentStress>();
        SS.SetSS();

        Windows= win.GetComponent<ComputerWindows>();
        Windows.SetComputerWindows(this);

        Commands = new ComputerCommands(this);

        Calculator = win.GetComponent<CalculatorController>();
        Calculator.SetCalculatorController();

        Question = win.GetComponent<QuestionController>();
        Question.SetQuestionController();

        Exam = win.GetComponent<ExamController>();
    }

    public virtual void SetScholars()
    {
        SS.SetScholars();
    }

    public virtual void Select()
    {
        Commands.Do(select);
    }


}
