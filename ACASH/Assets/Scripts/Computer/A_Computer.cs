using UnityEngine;
using Computer;


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
    public TextController Text;

    [HideInInspector]
    public string command;


    public virtual void Setup()
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

        Text = win.GetComponent<TextController>();
        Text.SetTextController();
    }

    public virtual void SetScholars()
    {
        SS.SetScholars();
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

}
