using System.Collections.Generic;
using UnityEngine;
using Operations;

public static class OperationsList
{

    private static Operation[] Toilet_3 = new Operation[]
    {
        new Operation(GetO.operation.Check),
        new Verify(GetO.verify.Teacher_Is_Here, true),

        new Verify(GetO.verify.Toilet_Is_Free, true),
        new Verify(GetO.verify.Answer, true),
        new Verify(GetO.verify.Toilet_Is_Free, true),

        new Special(GetO.special.Pee, 5),
        //new Operation(GetO.operation.Go_To_Desk),
    };


    private static Operation[] Outside_2 = new Operation[]
    {
        new Operation(GetO.operation.Check),
        new Verify(GetO.verify.Teacher_Is_Here, true),

        new Verify(GetO.verify.Outside_Is_Free, true),
        new Verify(GetO.verify.Answer, true),
        new Verify(GetO.verify.Outside_Is_Free, true),

        //new Operation(GetO.operation.Go_Outside),

        new Special(GetO.special.Think_Outside, 5),
        //new Operation(GetO.operation.Go_To_Desk),
    };

    private static Operation[] Sink_2 = new Operation[]
    {
        new Operation(GetO.operation.Check),
        new Verify(GetO.verify.Teacher_Is_Here, true),

        new Verify(GetO.verify.Sink_Is_Free, true),
        new Verify(GetO.verify.Answer, true),
        new Verify(GetO.verify.Sink_Is_Free, true),

        //new Operation(GetO.operation.Go_To_Sink),

        new Special(GetO.special.Wash_Hands, 5),
        //new Operation(GetO.operation.Go_To_Desk),
    };


    private static Operation[] Cheating_Toilet_3 = new Operation[]
    {
        //new Operation(GetO.operation.Go_To_Desk),
        new Operation(GetO.operation.Check),
        new Verify(GetO.verify.Teacher_Is_Here, true),

        new Verify(GetO.verify.Toilet_Is_Free, true),
        new Verify(GetO.verify.Answer, true),
        new Verify(GetO.verify.Toilet_Is_Free, true),

        //new Operation(GetO.operation.Go_To_Toilet),

        new Operation(GetO.operation.StartCheat),
        new Special(GetO.special.Wait, 5),
        new Operation(GetO.operation.Note),
        new Operation(GetO.operation.EndCheat),

        //new Operation(GetO.operation.Go_To_Desk),
    };


    private static Operation[] Cheating_Outside_2 = new Operation[]
    {
        new Operation(GetO.operation.Check),
        new Verify(GetO.verify.Teacher_Is_Here, true),

        new Verify(GetO.verify.Outside_Is_Free, true),
        new Verify(GetO.verify.Answer, true),
        new Verify(GetO.verify.Outside_Is_Free, true),

        //new Operation(GetO.operation.Go_Outside),

        new Operation(GetO.operation.StartCheat),
        new Special(GetO.special.Wait, 5),
        new Operation(GetO.operation.Note),
        new Operation(GetO.operation.EndCheat),

        //new Operation(GetO.operation.Go_To_Desk),
    };

    private static Operation[] Cheating_Sink_2 = new Operation[]
    {
        new Operation(GetO.operation.Check),
        new Verify(GetO.verify.Teacher_Is_Here, true),

        new Verify(GetO.verify.Sink_Is_Free, true),
        new Verify(GetO.verify.Answer, true),
        new Verify(GetO.verify.Sink_Is_Free, true),

        //new Operation(GetO.operation.Go_To_Sink),

        new Operation(GetO.operation.StartCheat),
        new Special(GetO.special.Wait, 5),
        new Operation(GetO.operation.Note),
        new Operation(GetO.operation.EndCheat),

        //new Operation(GetO.operation.Go_To_Desk),
    };

    private static Operation[] Ask_1 = new Operation[]
    {
        new Operation(GetO.operation.Check),
        new Verify(GetO.verify.Teacher_Is_Here, true),
    };

    private static Operation[] Think_Aloud_1 = new Operation[]
    {
        new Operation(GetO.operation.Think_Aloud),
    };

    private static Operation[] Note = new Operation[]
    {
        //new Operation(GetO.operation.Go_To_Desk),

        new Operation(GetO.operation.StartCheat),
        new Special(GetO.special.Wait, 5),
        new Operation(GetO.operation.Note),
        new Operation(GetO.operation.EndCheat),
    };

    private static Operation[] Desk = new Operation[]
{
        //new Operation(GetO.operation.Go_To_Desk),
};

    private static Operation[] Execute = new Operation[]
    {
        new Operation(GetO.operation.Execute),
    };

    private static Operation[] Go_Home = new Operation[]
    {
        //new Operation(GetO.operation.Go_Home),
    };



    public static Dictionary<string, Operation[]> operations { get; private set; } = new Dictionary<string, Operation[]>()
    {
        {"Toilet_3", Toilet_3},
        {"Outside_2", Outside_2},
        {"Sink_2", Sink_2},
        {"Ask_1", Ask_1},
        {"Think_Aloud_1", Think_Aloud_1},
        {"Execute", Execute},
        {"Go_Home", Go_Home},
        {"Cheating_Note_1", Note},
        {"Cheating_Toilet_3", Cheating_Toilet_3},
        {"Cheating_Outside_2", Cheating_Outside_2},
        {"Cheating_Sink_2", Cheating_Sink_2},
        {"Desk", Desk },
    };


}
