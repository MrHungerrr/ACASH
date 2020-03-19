using Animations;
using ComputerActions;
using UnityEngine;
using System.Collections;

public class OperationsExecuter : OperationsExecuterBase
{

    //=========================================================================================================================================================
    // Идти к парте
    private IEnumerator Go_To_Desk()
    {
        GoTo(PlaceManager.place.Desk, Scholar.Info.number);

        yield return new WaitForEndOfFrame();

        while (!IsHere())
            yield return new WaitForEndOfFrame();

        WatchTo(PlaceManager.place.Desk, Scholar.Info.number);

        OperationEnd();
    }

    //=========================================================================================================================================================
    // Думать
    private IEnumerator Think(int time)
    {
        Scholar.Anim.SetAnimation(GetA.animations.Thinking);
        yield return new WaitForSeconds(time);

        OperationEnd();
    }



    //=========================================================================================================================================================
    // Писать экзамен
    private IEnumerator Write(int time)
    {
        Scholar.Anim.SetAnimation(GetA.animations.Writing);
        yield return new WaitForSeconds(time);

        OperationEnd();
    }



    //=========================================================================================================================================================
    //Выход в туалет
    private IEnumerator Go_To_Toilet(int index)
    {
        GoTo(PlaceManager.place.Toilet, index);

        while (!IsHere())
            yield return new WaitForEndOfFrame();

        WatchTo(PlaceManager.place.Toilet, index);

        OperationEnd();
    }


    //=========================================================================================================================================================
    //Пописать
    private IEnumerator Pee(int index)
    {
        Scholar.Anim.SetAnimation(GetA.animations.Peeing);
        yield return new WaitForSeconds(index);

        OperationEnd();
    }




    //=========================================================================================================================================================
    //Выход к раковина
    private IEnumerator Go_To_Sink(int index)
    {
        GoTo(PlaceManager.place.Sink, index);

        while (!IsHere())
            yield return new WaitForEndOfFrame();

        WatchTo(PlaceManager.place.Sink, index);

        OperationEnd();
    }



    //=========================================================================================================================================================
    //Помыл руки
    private IEnumerator Wash_Hands(int index)
    {
        Scholar.Anim.SetAnimation(GetA.animations.Washing_Hands);
        yield return new WaitForSeconds(index);

        OperationEnd();
    }




    //=========================================================================================================================================================
    //Выход подышать воздухом
    private IEnumerator Go_Outside(int index)
    {
        GoTo(PlaceManager.place.Outside, index);

        while (!IsHere())
            yield return new WaitForEndOfFrame();

        WatchTo(PlaceManager.place.Outside, index);

        OperationEnd();
    }


    private IEnumerator Think_Outside(int index)
    {
        Scholar.Anim.SetAnimation(GetA.animations.Thinking_Outside);
        yield return new WaitForSeconds(index);

        OperationEnd();
    }




    //=========================================================================================================================================================
    //Достать бумажку и посмотреть в нее

    private IEnumerator Note()
    {
        Scholar.Anim.SetAnimation(GetA.animations.Take_Out);

        yield return new WaitForSeconds(0.25f);

        Scholar.Objects.Hold(ScholarObjectsManager.obj_name.Note);

        yield return new WaitForSeconds(0.5f);

        Scholar.Anim.SetAnimation(GetA.animations.Take_Up);

        yield return new WaitForSeconds(5f);

        Scholar.Objects.ThrowOut();

        Scholar.Anim.SetAnimation(GetA.animations.Nothing);

        OperationEnd();
    }



    //=========================================================================================================================================================
    //Думание вслух

    private IEnumerator Think_Aloud()
    {
        Scholar.Anim.SetAnimation(GetA.animations.Thinking_Aloud);
        Scholar.Talk.SayThoughts("Think_Aloud");

        while(Scholar.Talk.talking)
            yield return new WaitForEndOfFrame();

        OperationEnd();
    }















    //=========================================================================================================================================================
    //Компьютерный раздел

    private IEnumerator Computer_Text()
    {
        Scholar.Anim.SetAnimation(GetA.animations.Writing);
        yield return new WaitForSeconds(0.5f);

        Scholar.Desk.ExecuteCommand(GetC.commands.Text);
        Scholar.Anim.SetAnimation(GetA.animations.Nothing);
        yield return new WaitForSeconds(0.5f);

        Scholar.Anim.SetAnimation(GetA.animations.Writing);
        Scholar.Desk.Controller.Typing.Type(FiveDigitInt.R_Count(Random.Range(6, 15)));

        while(Scholar.Desk.Controller.Typing.typing)
            yield return new WaitForEndOfFrame();

        Scholar.Anim.SetAnimation(GetA.animations.Nothing);
        yield return new WaitForSeconds(1f);

        Scholar.Anim.SetAnimation(GetA.animations.Writing);
        Scholar.Desk.Controller.Typing.Type(FiveDigitInt.R_Count(Random.Range(6, 15)));

        while (Scholar.Desk.Controller.Typing.typing)
            yield return new WaitForEndOfFrame();

        Scholar.Anim.SetAnimation(GetA.animations.Nothing);
        yield return new WaitForSeconds(1f);

        Scholar.Anim.SetAnimation(GetA.animations.Writing);
        Scholar.Desk.ExecuteCommand(GetC.commands.Desktop);
        yield return new WaitForSeconds(0.5f);

        Scholar.Anim.SetAnimation(GetA.animations.Nothing);
        OperationEnd();
    }




    private IEnumerator Computer_Calculator()
    {
        Scholar.Anim.SetAnimation(GetA.animations.Writing);
        yield return new WaitForSeconds(0.5f);

        Scholar.Desk.ExecuteCommand(GetC.commands.Calculator);
        Scholar.Anim.SetAnimation(GetA.animations.Nothing);
        yield return new WaitForSeconds(0.5f);




        //Вычисление_1
        Scholar.Anim.SetAnimation(GetA.animations.Writing);
        Scholar.Desk.ExecuteCommand(GetC.commands.Reset);
        Scholar.Desk.Controller.Typing.Type(FiveDigitInt.R_Count(Random.Range(1, 6)));

        while (Scholar.Desk.Controller.Typing.typing)
            yield return new WaitForEndOfFrame();


        Scholar.Desk.ExecuteCommand(GetC.GetCommand(Calculator.RandomOpeartion().ToString()));
        Scholar.Desk.Controller.Typing.Type(FiveDigitInt.R_Count(Random.Range(1, 6)));

        while (Scholar.Desk.Controller.Typing.typing)
            yield return new WaitForEndOfFrame();


        Scholar.Desk.ExecuteCommand(GetC.commands.Calculate);
        Scholar.Anim.SetAnimation(GetA.animations.Thinking);
        yield return new WaitForSeconds(5f);




        //Вычисление_2
        Scholar.Desk.ExecuteCommand(GetC.commands.Reset);
        Scholar.Anim.SetAnimation(GetA.animations.Writing);
        Scholar.Desk.Controller.Typing.Type(FiveDigitInt.R_Count(Random.Range(1, 6)));

        while (Scholar.Desk.Controller.Typing.typing)
            yield return new WaitForEndOfFrame();


        Scholar.Desk.ExecuteCommand(GetC.GetCommand(Calculator.RandomOpeartion().ToString()));
        Scholar.Desk.Controller.Typing.Type(FiveDigitInt.R_Count(Random.Range(1, 6)));

        while (Scholar.Desk.Controller.Typing.typing)
            yield return new WaitForEndOfFrame();


        Scholar.Desk.ExecuteCommand(GetC.commands.Calculate);
        Scholar.Anim.SetAnimation(GetA.animations.Nothing);
        yield return new WaitForSeconds(5f);



        Scholar.Anim.SetAnimation(GetA.animations.Writing);
        Scholar.Desk.ExecuteCommand(GetC.commands.Desktop);
        yield return new WaitForSeconds(0.5f);

        Scholar.Anim.SetAnimation(GetA.animations.Nothing);
        OperationEnd();
    }



    private IEnumerator Computer_Question(int question)
    {
        Scholar.Anim.SetAnimation(GetA.animations.Writing);
        yield return new WaitForSeconds(0.5f);

        Scholar.Desk.ExecuteCommand(GetC.commands.Exam);
        Scholar.Anim.SetAnimation(GetA.animations.Nothing);
        yield return new WaitForSeconds(0.5f);


        Scholar.Anim.SetAnimation(GetA.animations.Writing);
        yield return new WaitForSeconds(0.5f);

        Scholar.Desk.ExecuteCommand("Question " + question);
        Scholar.Anim.SetAnimation(GetA.animations.Nothing);
        yield return new WaitForSeconds(0.5f);



        Scholar.Anim.SetAnimation(GetA.animations.Thinking);
        yield return new WaitForSeconds(Random.Range(3, 10));


        Scholar.Anim.SetAnimation(GetA.animations.Writing);
        Scholar.Desk.ExecuteCommand("Answer " + Random.Range(1, 5));
        yield return new WaitForSeconds(0.5f);


        Scholar.Anim.SetAnimation(GetA.animations.Thinking);
        yield return new WaitForSeconds(Random.Range(3, 10));

        Scholar.Anim.SetAnimation(GetA.animations.Writing);
        Scholar.Desk.ExecuteCommand("Answer " + Random.Range(1, 5));
        yield return new WaitForSeconds(0.5f);


        Scholar.Anim.SetAnimation(GetA.animations.Writing);
        Scholar.Desk.ExecuteCommand(GetC.commands.Exam);
        yield return new WaitForSeconds(0.5f);
        Scholar.Desk.ExecuteCommand(GetC.commands.Desktop);

        Scholar.Anim.SetAnimation(GetA.animations.Nothing);
        OperationEnd();
    }



    private IEnumerator Computer_Rules()
    {
        Scholar.Anim.SetAnimation(GetA.animations.Writing);
        yield return new WaitForSeconds(0.5f);

        Scholar.Desk.ExecuteCommand(GetC.commands.Rules);
        Scholar.Anim.SetAnimation(GetA.animations.Nothing);
        yield return new WaitForSeconds(0.5f);


        Scholar.Anim.SetAnimation(GetA.animations.Thinking);
        yield return new WaitForSeconds(Random.Range(5, 15));


        Scholar.Anim.SetAnimation(GetA.animations.Writing);
        yield return new WaitForSeconds(0.5f);

        Scholar.Desk.ExecuteCommand(GetC.commands.Desktop);
        Scholar.Anim.SetAnimation(GetA.animations.Nothing);
        OperationEnd();
    }





    //=========================================================================================================================================================
    //Зайти в комп под своим логином

    private IEnumerator Computer_Login()
    {
        Scholar.Anim.SetAnimation(GetA.animations.Writing);
        yield return new WaitForSeconds(0.5f);

        Scholar.Desk.ExecuteCommand(GetC.commands.Input_Field_Login);
        Scholar.Anim.SetAnimation(GetA.animations.Nothing);
        yield return new WaitForSeconds(0.5f);

        Scholar.Anim.SetAnimation(GetA.animations.Writing);
        Scholar.Desk.Controller.Typing.Type(new FiveDigitInt("1111"));

        while (Scholar.Desk.Controller.Typing.typing)
            yield return new WaitForEndOfFrame();

        yield return new WaitForSeconds(0.5f);

        Scholar.Desk.ExecuteCommand(GetC.commands.Input_Field_Password);
        Scholar.Anim.SetAnimation(GetA.animations.Nothing);


        Scholar.Anim.SetAnimation(GetA.animations.Writing);
        Scholar.Desk.Controller.Typing.Type(new FiveDigitInt("1111"));

        while (Scholar.Desk.Controller.Typing.typing)
            yield return new WaitForEndOfFrame();

        yield return new WaitForSeconds(0.5f);

        Scholar.Desk.ExecuteCommand(GetC.commands.Log_In_Computer);
        Scholar.Anim.SetAnimation(GetA.animations.Nothing);
        OperationEnd();
    }



























    //=========================================================================================================================================================
    //=========================================================================================================================================================
    //=========================================================================================================================================================
    //=========================================================================================================================================================
    //=========================================================================================================================================================
    //=========================================================================================================================================================
    //=========================================================================================================================================================
    //=========================================================================================================================================================
    //=========================================================================================================================================================
    //=========================================================================================================================================================
    //=========================================================================================================================================================
    //Списывание
    //=========================================================================================================================================================




}
