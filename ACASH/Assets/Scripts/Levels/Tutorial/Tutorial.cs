using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Single;

public class Tutorial : Singleton<Tutorial>
{
    private TutorialPlayerWatcher Watcher;

    [Header("First Room")]
    public TriggerAction first_room;
    public Scholar first_room_scholar;
    public Scholar[] first_room_other_scholars = new Scholar[2];
    public StressScreen[] first_room_text_screens = new StressScreen[3];

    public Door first_room_door_enter;
    public Door first_room_door_exit;


    [Header("Second Room")]
    public TriggerAction second_room;
    public Scholar[] second_room_scholars = new Scholar[3];
    public Door second_room_door_enter;
    public TeacherComputer computer;


    private void Awake()
    {
        first_room.OnEnter += StartFirstRoom;
        second_room.OnEnter += StartSecondRoom;

        Watcher = GetComponent<TutorialPlayerWatcher>();
        Watcher.Setup();

        StartCoroutine(StartLevel());
    }


    private IEnumerator StartLevel()
    {

        ElevatorController.get.Close();

        while (LevelManager.get.IsLoad())
            yield return new WaitForEndOfFrame();

        yield return new WaitForSeconds(1f);

        GameManager.get.SetLevel();

        while(SubtitleManager.get.act)
            yield return new WaitForEndOfFrame();

        yield return new WaitForSeconds(3f);


        ElevatorController.get.Ready();

        while (!Elevator.get.open)
            yield return new WaitForEndOfFrame();

    }




    //===================================================================================================================================
    // Первая комната
    //===================================================================================================================================

    private void StartFirstRoom()
    {

        StartCoroutine(First_Room());
        first_room.Remove();
    }

    private IEnumerator First_Room()
    {
        first_room_scholar.ResetType();
        yield return new WaitForEndOfFrame();



        Player.get.Talk.talk_good_control = true;


        while(!Watcher.talk_good)
        {
            yield return new WaitForEndOfFrame();
        }




        Player.get.Talk.talk_bad_control = true;

        while (!Watcher.talk_bad)
        {
            yield return new WaitForEndOfFrame();
        }


        foreach(Scholar s in first_room_other_scholars)
        {
            s.ResetType();
        }


        Player.get.Talk.shout_control = true;

        while (!Watcher.shout)
        {
            yield return new WaitForEndOfFrame();
        }




        first_room_door_exit.locked = false;
    }


    private IEnumerator Second_Corridor()
    {
        yield return new WaitForEndOfFrame();
    }






    //===================================================================================================================================
    // Вторая комната
    //===================================================================================================================================

    private void StartSecondRoom()
    {
        StartCoroutine(Second_Room_Part_1());
        second_room.Remove();
        second_room_door_enter.Close();
        second_room_door_enter.locked = true;
    }


    private IEnumerator Second_Room_Part_1()
    {

        Player.get.Talk.execute_control = true;
        second_room_scholars[1].ResetType();

        second_room_scholars[1].Action.Reset("Login");

        while (!second_room_scholars[1].Execute.executed)
        {
            yield return new WaitForEndOfFrame();
        }


        while (InputManager.get.gameType != "computer")
        {
            yield return new WaitForEndOfFrame();
        }


        if (computer.Windows.current_window == "Rules")
        {
            while (computer.Windows.current_window != "Rules")
            {
                yield return new WaitForEndOfFrame();
            }

            Debug.LogWarning("Rules!");
        }
        else
        {
            while (computer.Windows.current_window != "Desktop")
            {
                yield return new WaitForEndOfFrame();
            }

            Debug.LogWarning("Desktop!");

            while (computer.Windows.current_window != "Rules")
            {
                yield return new WaitForEndOfFrame();
            }

            Debug.LogWarning("Rules!");
        }


        foreach (Scholar s in second_room_scholars)
        {
            s.ResetType();
            s.Action.Reset("Login");
        }

        second_room_scholars[0].Action.AddAction("Cheating_Calculate_1");
        second_room_scholars[0].Action.AddAction("Cheating_Calculate_1");
        second_room_scholars[0].Action.AddAction("Cheating_Calculate_1");
        second_room_scholars[0].Action.AddAction("Cheating_Calculate_1");
        second_room_scholars[0].Action.AddAction("Cheating_Calculate_1");
        second_room_scholars[0].Action.AddAction("Cheating_Calculate_1");
        second_room_scholars[0].Action.AddAction("Cheating_Calculate_1");
        second_room_scholars[0].Action.AddAction("Cheating_Calculate_1");
        second_room_scholars[0].Action.AddAction("Cheating_Calculate_1");
        second_room_scholars[0].Action.AddAction("Cheating_Calculate_1");
        second_room_scholars[0].Action.AddAction("Cheating_Calculate_1");
        second_room_scholars[0].Action.AddAction("Cheating_Calculate_1");
        second_room_scholars[0].Action.AddAction("Cheating_Calculate_1");
        second_room_scholars[0].Action.AddAction("Cheating_Calculate_1");
        second_room_scholars[0].Action.AddAction("Cheating_Calculate_1");
        second_room_scholars[0].Action.AddAction("Cheating_Calculate_1");
        Watcher.Reset();

        bool option = true;

        while (option)
        {
            if (Watcher.execute)
            {
                if (second_room_scholars[0].Execute.executed)
                {
                    option = false;
                }
                else
                {
                    //Сказать реплику, что не правильный ученик
                    Watcher.Reset();
                }
            }
            yield return new WaitForEndOfFrame();
        }

        foreach (Scholar s in second_room_scholars)
        {
            s.Execute.EndExamForScholar();
        }

        yield return new WaitForSeconds(5f);


        foreach (Scholar s in second_room_scholars)
        {
            s.ResetType();
            s.Action.Reset("Login");
        }

        second_room_scholars[2].Action.AddAction("Cheating_Note_1");
        second_room_scholars[2].Action.AddAction("Cheating_Note_1");
        second_room_scholars[2].Action.AddAction("Cheating_Note_1");
        second_room_scholars[2].Action.AddAction("Cheating_Note_1");
        second_room_scholars[2].Action.AddAction("Cheating_Note_1");
        second_room_scholars[2].Action.AddAction("Cheating_Note_1");
        second_room_scholars[2].Action.AddAction("Cheating_Note_1");
        second_room_scholars[2].Action.AddAction("Cheating_Note_1");
        second_room_scholars[2].Action.AddAction("Cheating_Note_1");
        second_room_scholars[2].Action.AddAction("Cheating_Note_1");
        second_room_scholars[2].Action.AddAction("Cheating_Note_1");
        second_room_scholars[2].Action.AddAction("Cheating_Note_1");
        second_room_scholars[2].Action.AddAction("Cheating_Note_1");
        second_room_scholars[2].Action.AddAction("Cheating_Note_1");
        second_room_scholars[2].Action.AddAction("Cheating_Note_1");
        second_room_scholars[2].Action.AddAction("Cheating_Note_1");
        Watcher.Reset();

        option = true;

        while (option)
        {
            if (Watcher.execute)
            {
                if (second_room_scholars[2].Execute.executed)
                {
                    option = false;
                }
                else
                {
                    //Сказать реплику, что не правильный ученик
                    Watcher.Reset();
                }
            }
            yield return new WaitForEndOfFrame();
        }




        StartCoroutine(Second_Room_Part_2());
    }








    private IEnumerator Second_Room_Part_2()
    {
        foreach (Scholar s in second_room_scholars)
        {
            s.Execute.EndExamForScholar();
        }

        Player.get.Talk.execute_control = false;

        yield return new WaitForSeconds(6f);


        foreach (Scholar s in second_room_scholars)
        {
            s.ResetType();
            s.Action.Reset("Login");
        }


        second_room_scholars[2].Action.AddAction("Write");
        second_room_scholars[2].Action.AddAction("Cheating_Note_1");
        second_room_scholars[2].Action.AddAction("Write");
        second_room_scholars[2].Action.AddAction("Cheating_Note_1");
        second_room_scholars[2].Action.AddAction("Write");
        second_room_scholars[2].Action.AddAction("Cheating_Note_1");
        second_room_scholars[2].Action.AddAction("Write");
        second_room_scholars[2].Action.AddAction("Cheating_Note_1");
        second_room_scholars[2].Action.AddAction("Write");
        second_room_scholars[2].Action.AddAction("Cheating_Note_1");
        second_room_scholars[2].Action.AddAction("Write");
        second_room_scholars[2].Action.AddAction("Cheating_Note_1");
        second_room_scholars[2].Action.AddAction("Write");
        second_room_scholars[2].Action.AddAction("Cheating_Note_1");
        second_room_scholars[2].Action.AddAction("Write");
        second_room_scholars[2].Action.AddAction("Cheating_Note_1");

        second_room_scholars[1].Action.AddAction("Answer");
        second_room_scholars[1].Action.AddAction("Answer");
        second_room_scholars[1].Action.AddAction("Cheating_Calculate_1");
        second_room_scholars[1].Action.AddAction("Answer");
        second_room_scholars[1].Action.AddAction("Cheating_Calculate_1");
        second_room_scholars[1].Action.AddAction("Answer");
        second_room_scholars[1].Action.AddAction("Cheating_Calculate_1");
        second_room_scholars[1].Action.AddAction("Answer");
        second_room_scholars[1].Action.AddAction("Cheating_Calculate_1");
        second_room_scholars[1].Action.AddAction("Answer");
        second_room_scholars[1].Action.AddAction("Cheating_Calculate_1");
        second_room_scholars[1].Action.AddAction("Answer");
        second_room_scholars[1].Action.AddAction("Cheating_Calculate_1");
        second_room_scholars[1].Action.AddAction("Answer");
        second_room_scholars[1].Action.AddAction("Cheating_Calculate_1");

        Watcher.Reset();

        int remarks = 0;

        while (remarks < 3)
        {
            if (Watcher.talk)
            {
                if (Player.get.Talk.scholar.Cheat.cheating)
                {
                    //Сказать реплику, что правильный ученик
                    remarks++;
                    Debug.LogError("Красава");
                    Watcher.Reset();
                }
                else
                {
                    Debug.LogError("Ты говно!");
                    //Сказать реплику, что ученик не списывал
                    Watcher.Reset();
                }
            }
            yield return new WaitForEndOfFrame();
        }

        StartCoroutine(Second_Room_Part_3());
    }






    private IEnumerator Second_Room_Part_3()
    {
        foreach (Scholar s in second_room_scholars)
        {
            s.Execute.EndExamForScholar();
        }

        yield return new WaitForSeconds(6f);

        Player.get.Talk.answer_no_control = true;

        Watcher.Reset();
        second_room_scholars[1].ResetType();
        second_room_scholars[1].Action.DoAction("Cheating_Toilet_3");


        bool option = true;

        while (option)
        {
            if (Watcher.answer_no)
            {
                option = false;
            }

            if (!second_room_scholars[1].Question.question && !second_room_scholars[1].Question.question_answered)
            {
                //Какого хуя не отвечаешь на вопрос
                second_room_scholars[1].Action.DoAction("Cheating_Toilet_3");
            }

            yield return new WaitForEndOfFrame();
        }


        Player.get.Talk.answer_no_control = false;
        Player.get.Talk.answer_yes_control = true;

        Watcher.Reset();
        second_room_scholars[1].Action.DoAction("Cheating_Toilet_3");


        option = true;

        while (option)
        {
            if (Watcher.answer_yes)
            {
                option = false;
            }

            if (!second_room_scholars[1].Question.question)
            {
                if (!second_room_scholars[1].Question.question_answered)
                {
                    //Какого хуя не отвечаешь на вопрос
                    second_room_scholars[1].Action.DoAction("Cheating_Toilet_3");
                }
                else if (!second_room_scholars[1].Question.answer)
                {
                    //Какого хуя не так отвечаешь на вопрос
                    second_room_scholars[1].Action.DoAction("Cheating_Toilet_3");
                }
            }

            yield return new WaitForEndOfFrame();
        }


        Player.get.Talk.execute_control = true;


        while (!Watcher.execute)
        {
            yield return new WaitForEndOfFrame();
        }


        Debug.Log("END OF LEVEL");

        yield return new WaitForEndOfFrame();
    }



    private IEnumerator EndLevel()
    {
        while (SubtitleManager.get.act)
            yield return new WaitForEndOfFrame();

        Elevator.get.Open();
    }
}
