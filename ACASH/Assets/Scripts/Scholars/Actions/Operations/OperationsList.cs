using System.Collections.Generic;
using UnityEngine;
using Questions;
using Operations;

public static class OperationsList
{

    private static Operation[] Toilet_3 = new Operation[]
    {
        new Operation(GetO.operation.Check),
        new Verify(GetO.verify.Teacher_Is_Here, true),

        new Question(GetQ.questions.Toilet),
        new Verify(GetO.verify.Answer, true),

        new Special(GetO.special.Go_To_Toilet, 0),
        new Special(GetO.special.Pee),
        new Operation(GetO.operation.Go_Home),
    };


    private static Operation[] Air_2 = new Operation[]
    {
        new Operation(GetO.operation.Check),
        new Verify(GetO.verify.Teacher_Is_Here, true),

        new Question(GetQ.questions.Air),
        new Verify(GetO.verify.Answer, true),

        new Special(GetO.special.Go_Outside, 0),
        new Special(GetO.special.Think_Outside),
        new Operation(GetO.operation.Go_Home),
    };

    private static Operation[] Sink_2 = new Operation[]
    {
        new Operation(GetO.operation.Check),
        new Verify(GetO.verify.Teacher_Is_Here, true),

        new Question(GetQ.questions.Sink),
        new Verify(GetO.verify.Answer, true),

        new Special(GetO.special.Go_To_Sink, 0),
        new Special(GetO.special.Wash_Hands),
        new Operation(GetO.operation.Go_Home),
    };

    private static Operation[] Ask_1 = new Operation[]
    {
        new Operation(GetO.operation.Check),
        new Verify(GetO.verify.Teacher_Is_Here, true),

        new Question(GetQ.questions.Simple),
    };

    private static Operation[] Think_Aloud_1 = new Operation[]
    {
        new Operation(GetO.operation.Think_Aloud),
    };

    private static Operation[] Write = new Operation[]
    {
        new Operation(GetO.operation.Go_Home),
        new Special(GetO.special.Think, 5),
        new Computer(GetO.computer.Text),
    };


    private static Operation[] Calculate = new Operation[]
    {
        new Operation(GetO.operation.Go_Home),
        new Special(GetO.special.Think, 5),
        new Computer(GetO.computer.Calculator),
    };

    private static Operation[] Watch_Rules = new Operation[]
    {
        new Operation(GetO.operation.Go_Home),
        new Special(GetO.special.Think, 5),
        new Computer(GetO.computer.Rules),
    };

    private static Operation[] Answer = new Operation[]
    {
        new Operation(GetO.operation.Go_Home),
        new Special(GetO.special.Think, 5),
        new Computer(GetO.computer_spec.Question, Random.Range(1,4)),
    };


    private static Operation[] Login = new Operation[]
    {
        new Operation(GetO.operation.Go_Home),
        new Computer(GetO.computer.Login),
    };

    private static Operation[] Note = new Operation[]
    {
        new Operation(GetO.operation.Go_Home),
        new Operation(GetO.operation.Note),
    };

    private static Operation[] Execute = new Operation[]
    {
        new Operation(GetO.operation.Execute),
    };



    public static Dictionary<string, Operation[]> operations = new Dictionary<string, Operation[]>()
    {
        {"Toilet_3", Toilet_3},
        {"Air_2", Air_2},
        {"Sink_2", Sink_2},
        {"Ask_1", Ask_1},
        {"Think_Aloud_1", Think_Aloud_1},
        {"Write", Write},
        {"Execute", Execute},
        {"Watch_Rules", Watch_Rules},
        {"Answer", Answer},
        {"Cheating_Calculate_1", Calculate},
        {"Cheating_Note_1", Note},
        {"Login", Login },
    };


}
