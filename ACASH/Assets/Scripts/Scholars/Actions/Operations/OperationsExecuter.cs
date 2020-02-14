using Animations;
using UnityEngine;
using System.Collections;

public class OperationsExecuter : OperationsExecuterBase
{

    //=========================================================================================================================================================
    // Идти домой
    private IEnumerator Go_Home()
    {
        GoTo(PlaceManager.place.Desk, Scholar.Info.number);

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
    //Исключение

    private IEnumerator Execute()
    {
        Scholar.Select.Selectable(false);
        Scholar.executed = true;
        yield return new WaitForSeconds(1f);

        Scholar.Emotions.ChangeEmotion("dead");
        Scholar.Stop();
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
