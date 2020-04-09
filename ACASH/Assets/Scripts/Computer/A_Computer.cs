using UnityEngine;
using ComputerActions;


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
    public TextController Text;
    [HideInInspector]
    public ComputerSounds Sound;
    [HideInInspector]
    public OverwatchCameraController Overwatch;

    [HideInInspector]
    public string command;


    public virtual void Setup()
    {
        Transform win = transform.Find("Screen").Find("UI").Find("Canvas").Find("Windows");
        Numpad = win.parent.Find("Screen").GetComponentInChildren<NumpadController>();

        Sound = GetComponent<ComputerSounds>();
        Sound.Setup(this);

        Login = win.GetComponent<LoginController>();
        Login.Setup(this);

        Desktop = win.GetComponent<DesktopController>();
        Desktop.Setup();

        SS = win.GetComponent<StudentStress>();
        SS.Setup();

        Overwatch = win.GetComponent<OverwatchCameraController>();
        Overwatch.Setup();

        Windows = win.GetComponent<ComputerWindows>();
        Windows.Setup(this);

        Calculator = win.GetComponent<CalculatorController>();
        Calculator.Setup();

        Question = win.GetComponent<QuestionController>();
        Question.Setup();

        Exam = win.GetComponent<ExamController>();

        Text = win.GetComponent<TextController>();
        Text.Setup();

        Commands = new ComputerCommands(this);
    }

    public virtual void SetScholars()
    {
        //SS.SetScholars();
    }

    protected void ExecuteCommand()
    {
        Commands.Do(command);
    }

    public void ExecuteCommand(string command)
    {
        this.command = command;
        ExecuteCommand();
    }

    public void ExecuteCommand(GetC.commands command)
    {
        this.command = GetC.GetString(command);
        ExecuteCommand();
    }



    public void ResetComputer()
    {
        Calculator.Reset();
        Exam.Reset();
        Text.Reset();

        Commands.Do(GetC.commands.Login);
    }
}
