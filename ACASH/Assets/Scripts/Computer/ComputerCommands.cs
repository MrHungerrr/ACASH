using UnityEngine;
using ComputerActions;

public class ComputerCommands
{
    private A_Computer Comp;

    public ComputerCommands(A_Computer c)
    {
        Comp = c;
        Do("Login");
    }


    public void Do(GetC.commands command)
    {
        Do(GetC.GetString(command));
    }


    public void Do(string type)
    {
        switch (type)
        {
            //========================================================
            // Окна

            case "Login":
                {
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
                    CloseProgram();
                    EnableTaskBar(true);
                    Comp.Numpad.Enable(false);
                    Set(type);
                    break;
                }
            case "Student Stress":
                {
                    SetProgram(type);
                    Comp.SS.Refresh();
                    Set(type);
                    break;
                }
            case "Overwatch":
                {
                    SetProgram(type);
                    Set(type);
                    break;
                }
            case "Info":
                {
                    SetProgram(type);
                    Set(type);
                    break;
                }
            case "Calculator":
                {
                    SetProgram(type);
                    Set(type);
                    Comp.Numpad.Enable(true);
                    Comp.Numpad.Set(Comp.Calculator.input);
                    break;
                }
            case "Exam":
                {
                    SetProgram(type);
                    Set(type);
                    break;
                }
            case "Question 1":
                {
                    SetProgram("Question");
                    Set("Question");
                    Comp.Question.SetQuestion(0);
                    break;
                }
            case "Question 2":
                {
                    SetProgram("Question");
                    Set("Question");
                    Comp.Question.SetQuestion(1);
                    break;
                }
            case "Question 3":
                {
                    SetProgram("Question");
                    Set("Question");
                    Comp.Question.SetQuestion(2);
                    break;
                }
            case "Rules":
                {
                    SetProgram(type);
                    Set(type);
                    break;
                }
            case "Text":
                {
                    SetProgram(type);
                    Set(type);
                    Comp.Numpad.Enable(true);
                    Comp.Numpad.Set(Comp.Text.input);
                    break;
                }
            case "Score":
                {
                    Set(type);
                    Comp.Numpad.Enable(false);
                    ComputerManager.get.end = true;
                    CloseProgram();
                    EnableTaskBar(false);
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
            case "Camera Right":
                {
                    Comp.Overwatch.ChangeCameraRight();
                    break;
                }
            case "Camera Left":
                {
                    Comp.Overwatch.ChangeCameraLeft();
                    break;
                }
            case "Second Page Score":
                {
                    ScoreManager.get.Accept();
                    LevelSettings.get.NextExam();
                    ComputerManager.get.end = false;

                    if (Comp.Login.loged)
                        Do("Desktop");
                    else
                        Do("Login");
                    //Новый экзамен
                    break;
                }
            case "Continue Score":
                {
                    ScoreManager.get.Accept();
                    LevelSettings.get.NextExam();
                    ComputerManager.get.end = false;

                    if(Comp.Login.loged)
                        Do("Desktop");
                    else
                        Do("Login");
                    //Новый экзамен
                    break;
                }
            case "Restart Score":
                {
                    ScoreManager.get.Zeroing();
                    LevelSettings.get.RestartExam();
                    ComputerManager.get.end = false;

                    if (Comp.Login.loged)
                        Do("Desktop");
                    else
                        Do("Login");
                    //Новый экзамен
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
                    Debug.LogError("Несуществуюющее окно - " + type);
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
                    CloseProgram();
                    Set("Desktop");
                    break;
                }
            case "Overwatch":
                {
                    CloseProgram();
                    Set("Desktop");
                    break;
                }
            case "Info":
                {
                    CloseProgram();
                    Set("Desktop");
                    break;
                }
            case "Calculator":
                {
                    Comp.Numpad.Enable(false);
                    CloseProgram();
                    Set("Desktop");
                    break;
                }
            case "Exam":
                {
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
                    CloseProgram();
                    Set("Desktop");
                    break;
                }
            case "Text":
                {
                    CloseProgram();
                    Set("Desktop");
                    Comp.Numpad.Enable(false);
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
