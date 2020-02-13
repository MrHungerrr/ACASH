using System.Collections.Generic;
using Questions;
using Operations;

public static class ActionsOperations
{

    private static Operation[] Toilet_3 = new Operation[]
    {
        new Operation(GetO.operation.Check),
        new Verify(GetO.verify.Teacher_Is_Here, true),

        new Question(GetQ.questions.Toilet),
        new Verify(GetO.verify.Answer, true),

        new Operation(GetO.operation.Go_To_Toilet),
        new Operation(GetO.operation.Wait_For_Toilet),
        new Operation(GetO.operation.Pee),
        new Operation(GetO.operation.Go_Home),
    };


    private static Operation[] Air_2 = new Operation[]
    {
        new Operation(GetO.operation.Check),
        new Verify(GetO.verify.Teacher_Is_Here, true),

        new Question(GetQ.questions.Air),
        new Verify(GetO.verify.Answer, true),

        new Operation(GetO.operation.Go_Outside),
        new Operation(GetO.operation.Think),
        new Operation(GetO.operation.Go_Home),
    };

    private static Operation[] Sink_2 = new Operation[]
    {
        new Operation(GetO.operation.Check),
        new Verify(GetO.verify.Teacher_Is_Here, true),

        new Question(GetQ.questions.Sink),
        new Verify(GetO.verify.Answer, true),

        new Operation(GetO.operation.Go_To_Sink),
        new Operation(GetO.operation.Wait_For_Sink),
        new Operation(GetO.operation.Wash_Hands),
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
        new Special(GetO.operation.Think_Aloud, 15),
    };



    public static Dictionary<string, Operation[]> operations = new Dictionary<string, Operation[]>()
    {
        {"Toilet_3", Toilet_3},
        {"Air_2", Air_2},
        {"Sink_2", Sink_2},
        {"Ask_1", Ask_1},
        {"Think_Aloud_1", Think_Aloud_1},
    };

}
