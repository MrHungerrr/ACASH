using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Single;

public class ComputerCommands
{
    private A_Computer Comp;

    public ComputerCommands(A_Computer c)
    {
        Comp = c;
        Do("Login");
    }

    public void Do(string type)
    {
        switch (type)
        {
            //========================================================
            // Окна

            case "Login":
                {
                    Disable();
                    CloseProgram();
                    EnableTaskBar(false);
                    Comp.Numpad.Enable(true);
                    Comp.Login.Reset();
                    Comp.Numpad.Set(Comp.Login.login);
                    Set(type);
                    break;
                }
            case "Desktop":
                {
                    Disable();
                    CloseProgram();
                    EnableTaskBar(true);
                    Comp.Numpad.Enable(false);
                    Set(type);
                    break;
                }
            case "Student Stress":
                {
                    Disable("Desktop");
                    SetProgram(type);
                    Comp.SS.Refresh();
                    Set(type);
                    break;
                }
            case "Overwatch":
                {
                    Disable("Desktop");
                    SetProgram(type);
                    Set(type);
                    break;
                }
            case "Info":
                {
                    Disable("Desktop");
                    SetProgram(type);
                    Set(type);
                    break;
                }
            case "Calculator":
                {
                    Disable("Desktop");
                    SetProgram(type);
                    Set(type);
                    Comp.Numpad.Enable(true);
                    Comp.Calculator.Reset();
                    Comp.Numpad.Set(Comp.Calculator.input);
                    break;
                }
            case "Exam":
                {
                    Disable(Comp.Windows.current_window);
                    SetProgram(type);
                    Set(type);
                    break;
                }
            case "Question 1":
                {
                    Disable("Exam");
                    SetProgram("Question");
                    Set("Question");
                    Comp.Question.SetQuestion(0);
                    break;
                }
            case "Question 2":
                {
                    Disable("Exam");
                    SetProgram("Question");
                    Set("Question");
                    Comp.Question.SetQuestion(1);
                    break;
                }
            case "Question 3":
                {
                    Disable("Exam");
                    SetProgram("Question");
                    Set("Question");
                    Comp.Question.SetQuestion(2);
                    break;
                }
            case "Rules":
                {
                    Disable(Comp.Windows.current_window);
                    SetProgram(type);
                    Set(type);
                    break;
                }



            //========================================================
            // Кнопки

            case "Refresh":
                {
                    Comp.SS.Refresh(); 
                    break;
                }
            case "Input Field Login":
                {
                    Comp.Numpad.Set(Comp.Login.login);
                    break;
                }
            case "Input Field Password":
                {
                    Comp.Numpad.Set(Comp.Login.password);
                    break;
                }
            case "Log In Computer":
                {
                    Comp.Login.TryLogin();
                    break;
                }
            case "0":
                {
                    Comp.Numpad.Plus(0);
                    break;
                }
            case "1":
                {
                    Comp.Numpad.Plus(1);
                    break;
                }
            case "2":
                {
                    Comp.Numpad.Plus(2);
                    break;
                }
            case "3":
                {
                    Comp.Numpad.Plus(3);
                    break;
                }
            case "4":
                {
                    Comp.Numpad.Plus(4);
                    break;
                }
            case "Plus":
                {
                    Comp.Calculator.input.Plus(Calculator.operations.Plus);
                    break;
                }
            case "Minus":
                {
                    Comp.Calculator.input.Plus(Calculator.operations.Minus);
                    break;
                }
            case "Multiply":
                {
                    Comp.Calculator.input.Plus(Calculator.operations.Multiply);
                    break;
                }
            case "Divide":
                {
                    Comp.Calculator.input.Plus(Calculator.operations.Divide);
                    break;
                }
            case "Mod":
                {
                    Comp.Calculator.input.Plus(Calculator.operations.Mod);
                    break;
                }
            case "Calculate":
                {
                    Comp.Calculator.Calculate();
                    break;
                }
            case "Backspace":
                {
                    Comp.Numpad.Backspace();
                    break;
                }
            case "Reset":
                {
                    Comp.Numpad.Reset();
                    break;
                }
            case "Finish Exam":
                {
                    Comp.Exam.Finish();
                    break;
                }
            case "Answer 1":
                {
                    Comp.Question.SetAnswer(0);
                    break;
                }
            case "Answer 2":
                {
                    Comp.Question.SetAnswer(1);
                    break;
                }
            case "Answer 3":
                {
                    Comp.Question.SetAnswer(2);
                    break;
                }
            case "Answer 4":
                {
                    Comp.Question.SetAnswer(3);
                    break;
                }
            case "Close":
                {
                    Escape(Comp.Windows.current_window);
                    break;
                }
            case "Exit":
                {
                    Do("Login");
                    break;
                }
            default:
                {
                    Debug.Log("Несуществуюющее окно - " + type);
                    break;
                }
        }
    }


    public void Escape(string type)
    {
        switch (type)
        {
            case "Student Stress":
                {
                    Disable(type);
                    CloseProgram();
                    Set("Desktop");
                    break;
                }
            case "Overwatch":
                {
                    Disable(type);
                    CloseProgram();
                    Set("Desktop");
                    break;
                }
            case "Info":
                {
                    Disable(type);
                    CloseProgram();
                    Set("Desktop");
                    break;
                }
            case "Calculator":
                {
                    Disable(type);
                    Comp.Numpad.Enable(false);
                    CloseProgram();
                    Set("Desktop");
                    break;
                }
            case "Exam":
                {
                    Disable(type);
                    CloseProgram();
                    Set("Desktop");
                    break;
                }
            case "Question":
                {
                    Do("Exam");
                    break;
                }
            case "Rules":
                {
                    Disable(type);
                    CloseProgram();
                    Set("Desktop");
                    break;
                }
        }
    }

    public void Set(string window)
    {
        Comp.Windows.Set(window);
    }


    public void Disable(string window)
    {
        Comp.Windows.Disable(window);
    }

    public void DisableAll()
    {
        Comp.Windows.DisableAll();
    }

    public void Disable()
    {
        Comp.Windows.Disable();
    }


    public void SetProgram(string n)
    {
        Comp.Windows.SetProgram(n);
    }

    public void CloseProgram()
    {
        Comp.Windows.CloseProgram();
    }

    public void EnableTaskBar(bool option)
    {
        Comp.Windows.EnableTaskBar(option);
    }
}
