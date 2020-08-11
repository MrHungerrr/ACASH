using Animations;
using ComputerActions;
using UnityEngine;
using System.Collections;
using Places;

public class OperationsExecuter : OperationsExecuterBase
{

    //=========================================================================================================================================================
    // Думать
    private IEnumerator Think(int time)
    {
        _scholar.Anim.SetAnimation(Get.animations.Thinking);
        yield return new WaitForSeconds(time);

        OperationEnd();
    }



    //=========================================================================================================================================================
    // Писать экзамен
    private IEnumerator Write(int time)
    {
        _scholar.Anim.SetAnimation(Get.animations.Writing);
        yield return new WaitForSeconds(time);

        OperationEnd();
    }



    //=========================================================================================================================================================
    //Пописать
    private IEnumerator Pee(int index)
    {
        _scholar.Anim.SetAnimation(Get.animations.Peeing);
        yield return new WaitForSeconds(index);

        OperationEnd();
    }



    //=========================================================================================================================================================
    //Помыл руки
    private IEnumerator Wash_Hands(int index)
    {
        _scholar.Anim.SetAnimation(Get.animations.Washing_Hands);
        yield return new WaitForSeconds(index);

        OperationEnd();
    }



    private IEnumerator Think_Outside(int index)
    {
        _scholar.Anim.SetAnimation(Get.animations.Thinking_Outside);
        yield return new WaitForSeconds(index);

        OperationEnd();
    }




    //=========================================================================================================================================================
    //Достать бумажку и посмотреть в нее

    private IEnumerator Note()
    {
        _scholar.Anim.SetAnimation(Get.animations.Take_Out);

        yield return new WaitForSeconds(0.25f);

        _scholar.Objects.Hold(ScholarObjectsManager.objectType.Note);

        yield return new WaitForSeconds(0.5f);

        _scholar.Anim.SetAnimation(Get.animations.Take_Up);

        yield return new WaitForSeconds(5f);

        _scholar.Objects.ThrowOut();

        _scholar.Anim.SetAnimation(Get.animations.Nothing);

        OperationEnd();
    }



    //=========================================================================================================================================================
    //Думание вслух

    private IEnumerator Think_Aloud()
    {
        _scholar.Anim.SetAnimation(Get.animations.Thinking_Aloud);
        _scholar.Talk.SayThoughts("Think_Aloud");

        while(_scholar.Talk.Talking)
            yield return new WaitForEndOfFrame();

        OperationEnd();
    }















    //=========================================================================================================================================================
    //Компьютерный раздел

    private IEnumerator Computer_Text()
    {
        
        _scholar.Anim.SetAnimation(Get.animations.Writing);
        yield return new WaitForSeconds(0.5f);

        _scholar.Desk.ExecuteCommand(GetC.commands.Text);
        _scholar.Anim.SetAnimation(Get.animations.Nothing);
        yield return new WaitForSeconds(0.5f);

        _scholar.Anim.SetAnimation(Get.animations.Writing);
        _scholar.Desk.Controller.Typing.Type(FiveDigitInt.R_Count(Random.Range(6, 15)));

        while(_scholar.Desk.Controller.Typing.typing)
        yield return new WaitForEndOfFrame();


        _scholar.Anim.SetAnimation(Get.animations.Nothing);
        yield return new WaitForSeconds(1f);

        _scholar.Anim.SetAnimation(Get.animations.Writing);
        _scholar.Desk.Controller.Typing.Type(FiveDigitInt.R_Count(Random.Range(6, 15)));

        while(_scholar.Desk.Controller.Typing.typing)
        yield return new WaitForEndOfFrame();

        _scholar.Anim.SetAnimation(Get.animations.Nothing);
        yield return new WaitForSeconds(1f);

        _scholar.Anim.SetAnimation(Get.animations.Writing);
        _scholar.Desk.ExecuteCommand(GetC.commands.Desktop);
        yield return new WaitForSeconds(0.5f);

        _scholar.Anim.SetAnimation(Get.animations.Nothing);
   
        
        OperationEnd();
    }




    private IEnumerator Computer_Calculator()
    {
        _scholar.Anim.SetAnimation(Get.animations.Writing);
        yield return new WaitForSeconds(0.5f);

        _scholar.Desk.ExecuteCommand(GetC.commands.Calculator);
        _scholar.Anim.SetAnimation(Get.animations.Nothing);
        yield return new WaitForSeconds(0.5f);




        //Вычисление_1
        _scholar.Anim.SetAnimation(Get.animations.Writing);
        _scholar.Desk.ExecuteCommand(GetC.commands.Reset);
        _scholar.Desk.Controller.Typing.Type(FiveDigitInt.R_Count(Random.Range(1, 6)));

        while (_scholar.Desk.Controller.Typing.typing)
            yield return new WaitForEndOfFrame();


        _scholar.Desk.ExecuteCommand(GetC.GetCommand(Calculator.RandomOpeartion().ToString()));
        _scholar.Desk.Controller.Typing.Type(FiveDigitInt.R_Count(Random.Range(1, 6)));

        while (_scholar.Desk.Controller.Typing.typing)
            yield return new WaitForEndOfFrame();


        _scholar.Desk.ExecuteCommand(GetC.commands.Calculate);
        _scholar.Anim.SetAnimation(Get.animations.Thinking);
        yield return new WaitForSeconds(5f);




        //Вычисление_2
        _scholar.Desk.ExecuteCommand(GetC.commands.Reset);
        _scholar.Anim.SetAnimation(Get.animations.Writing);
        _scholar.Desk.Controller.Typing.Type(FiveDigitInt.R_Count(Random.Range(1, 6)));

        while (_scholar.Desk.Controller.Typing.typing)
            yield return new WaitForEndOfFrame();


        _scholar.Desk.ExecuteCommand(GetC.GetCommand(Calculator.RandomOpeartion().ToString()));
        _scholar.Desk.Controller.Typing.Type(FiveDigitInt.R_Count(Random.Range(1, 6)));

        while (_scholar.Desk.Controller.Typing.typing)
            yield return new WaitForEndOfFrame();


        _scholar.Desk.ExecuteCommand(GetC.commands.Calculate);
        _scholar.Anim.SetAnimation(Get.animations.Nothing);
        yield return new WaitForSeconds(5f);



        _scholar.Anim.SetAnimation(Get.animations.Writing);
        _scholar.Desk.ExecuteCommand(GetC.commands.Desktop);
        yield return new WaitForSeconds(0.5f);

        _scholar.Anim.SetAnimation(Get.animations.Nothing);
        OperationEnd();
    }



    private IEnumerator Computer_Question(int question)
    {
        _scholar.Anim.SetAnimation(Get.animations.Writing);
        yield return new WaitForSeconds(0.5f);

        _scholar.Desk.ExecuteCommand(GetC.commands.Exam);
        _scholar.Anim.SetAnimation(Get.animations.Nothing);
        yield return new WaitForSeconds(0.5f);


        _scholar.Anim.SetAnimation(Get.animations.Writing);
        yield return new WaitForSeconds(0.5f);

        _scholar.Desk.ExecuteCommand("Question " + question);
        _scholar.Anim.SetAnimation(Get.animations.Nothing);
        yield return new WaitForSeconds(0.5f);



        _scholar.Anim.SetAnimation(Get.animations.Thinking);
        yield return new WaitForSeconds(Random.Range(3, 10));


        _scholar.Anim.SetAnimation(Get.animations.Writing);
        _scholar.Desk.ExecuteCommand("Answer " + Random.Range(1, 5));
        yield return new WaitForSeconds(0.5f);


        _scholar.Anim.SetAnimation(Get.animations.Thinking);
        yield return new WaitForSeconds(Random.Range(3, 10));

        _scholar.Anim.SetAnimation(Get.animations.Writing);
        _scholar.Desk.ExecuteCommand("Answer " + Random.Range(1, 5));
        yield return new WaitForSeconds(0.5f);


        _scholar.Anim.SetAnimation(Get.animations.Writing);
        _scholar.Desk.ExecuteCommand(GetC.commands.Exam);
        yield return new WaitForSeconds(0.5f);
        _scholar.Desk.ExecuteCommand(GetC.commands.Desktop);

        _scholar.Anim.SetAnimation(Get.animations.Nothing);
        OperationEnd();
    }



    private IEnumerator Computer_Rules()
    {
        _scholar.Anim.SetAnimation(Get.animations.Writing);
        yield return new WaitForSeconds(0.5f);

        _scholar.Desk.ExecuteCommand(GetC.commands.Rules);
        _scholar.Anim.SetAnimation(Get.animations.Nothing);
        yield return new WaitForSeconds(0.5f);


        _scholar.Anim.SetAnimation(Get.animations.Thinking);
        yield return new WaitForSeconds(Random.Range(5, 15));


        _scholar.Anim.SetAnimation(Get.animations.Writing);
        yield return new WaitForSeconds(0.5f);

        _scholar.Desk.ExecuteCommand(GetC.commands.Desktop);
        _scholar.Anim.SetAnimation(Get.animations.Nothing);
        OperationEnd();
    }





    //=========================================================================================================================================================
    //Зайти в комп под своим логином

    private IEnumerator Computer_Login()
    {
        _scholar.Anim.SetAnimation(Get.animations.Writing);
        yield return new WaitForSeconds(0.5f);

        _scholar.Desk.ExecuteCommand(GetC.commands.Input_Field_Login);
        _scholar.Anim.SetAnimation(Get.animations.Nothing);
        yield return new WaitForSeconds(0.5f);

        _scholar.Anim.SetAnimation(Get.animations.Writing);
        _scholar.Desk.Controller.Typing.Type(new FiveDigitInt("1111"));

        while (_scholar.Desk.Controller.Typing.typing)
            yield return new WaitForEndOfFrame();

        yield return new WaitForSeconds(0.5f);

        _scholar.Desk.ExecuteCommand(GetC.commands.Input_Field_Password);
        _scholar.Anim.SetAnimation(Get.animations.Nothing);


        _scholar.Anim.SetAnimation(Get.animations.Writing);
        _scholar.Desk.Controller.Typing.Type(new FiveDigitInt("1111"));

        while (_scholar.Desk.Controller.Typing.typing)
            yield return new WaitForEndOfFrame();

        yield return new WaitForSeconds(0.5f);

        _scholar.Desk.ExecuteCommand(GetC.commands.Log_In_Computer);
        _scholar.Anim.SetAnimation(Get.animations.Nothing);
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
