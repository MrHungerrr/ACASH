using System.Collections;
using PlayerOptions;
using UnityEngine;
using Single;

public class Tutorial_1 : A_Level
{
    private TutorialPlayerEyes Eyes;
    private TutorialPlayerWatcher Watcher;
    private TutorialScholarController Cheat;
    private TutorialKeyHint_1 Hint;


    [Header("Begining")]
    public GameObject phone;


    [Header("Room")]
    public TriggerAction trigger_room;
    private Scholar[] scholars;
    public Door door_enter;
    public TeacherComputer computer;




    protected override void Setup()
    {
        Key("Tutorial_1");
        trigger_room.OnEnter.AddListener(StartTutorial);
        phone.GetComponent<InteractAction>().OnInteraction.AddListener(StartElevator);

        Watcher = new TutorialPlayerWatcher();
        Cheat = new TutorialScholarController();
        Hint = GetComponent<TutorialKeyHint_1>();
        Eyes = GetComponent<TutorialPlayerEyes>();

        InputManager.get.Controls.Gameplay.HUD.Disable();
    }


    protected override void Begin()
    {
        StartCoroutine(Begining());
    }





        private IEnumerator Begining()
    {
       // while (!LevelManager.get.IsLoad())
           // yield return new WaitForEndOfFrame();

        Transform point = GameObject.FindGameObjectWithTag("PlayerPoint").transform;
        Player.get.Move.Position(point.position);
        Destroy(point.gameObject);

        yield return new WaitForSeconds(1f);

        key *= "Introdaction_Home";

        HUDManager.get.IntrodactionHUD(key);
        NoiseManager.get.Play(NoiseManager.sounds.Rain);
        Hint.Begin();

        yield return new WaitForSeconds(2f);

        phone.GetComponent<ObjectSound>().Play();

        HUDManager.get.CloseIntrodactionHUD();

        yield return new WaitForSeconds(1f);

        scholars = ScholarManager.get.scholars;

        GameManager.get.StartGame();

        yield return new WaitForSeconds(1f);
    }


    //===================================================================================================================================
    // Первая комната
    //===================================================================================================================================
    private void StartElevator()
    {
        phone.GetComponent<InteractAction>().Remove();
        phone.GetComponent<ObjectSound>().Stop();

        FadeHUDController.get.FastFade(true);
        InputManager.get.SwitchGameInput("cutscene");

        StartCoroutine(ElevatorRoom());

    }


    private IEnumerator ElevatorRoom()
    {
        Player.get.Move.Position(Elevator.get.position);

        key *= "Begining";

        SubtitleManager.get.Say(key);

        while (SubtitleManager.get.act)
            yield return new WaitForEndOfFrame();

        key *= "Introdaction_Training";

        HUDManager.get.IntrodactionHUD(key);

        NoiseManager.get.Stop(NoiseManager.sounds.Rain);


        yield return new WaitForSeconds(3f);

        HUDManager.get.CloseIntrodactionHUD();
        ElevatorController.get.Close();

        yield return new WaitForSeconds(1f);

        GameManager.get.StartGame();

        key *= "Elevator";

        SubtitleManager.get.Say(key);

        while (SubtitleManager.get.act)
            yield return new WaitForEndOfFrame();



        ElevatorController.get.Ready();

        yield return new WaitForSeconds(1f);

        ElevatorController.get.Open();

        while (!Elevator.get.open)
            yield return new WaitForEndOfFrame();
    }





    //===================================================================================================================================
    // Вторая комната
    //===================================================================================================================================

    private void StartTutorial()
    {
        StartCoroutine(Tutorial_Part_1());
        trigger_room.Remove();
        door_enter.Close();
        door_enter.locked = true;
    }


    private IEnumerator Tutorial_Part_1()
    {
        key *= "Room";
        key_mistake *= "Room";
        key += 0;



        while (SubtitleManager.get.act)
            yield return new WaitForEndOfFrame();


        Player.get.Talk.DenyAll();
        Player.get.Talk.all_controll = false;

        SubtitleManager.get.Say(key);

        scholars[1].ResetType();

        scholars[1].Action.Reset("Login");

        while (SubtitleManager.get.act)
            yield return new WaitForEndOfFrame();

        Hint.Set(GetP.actions.Execute);
        Player.get.Talk.all_controll = true;
        Player.get.Talk.execute_control = true;
        Watcher.Reset();


        bool option = true;

        while (option)
        {
            if(scholars[1].Execute.executed)
            {
                Hint.Disable();
            }

            if (Watcher.done)
            {
                if (Watcher.execute)
                {
                    option = false;
                }
                else
                {
                    while (SubtitleManager.get.act)
                       yield return new WaitForEndOfFrame();

                    key_mistake += 0;
                    SubtitleManager.get.Say(key_mistake);
                }
                Watcher.Reset();
            }

            yield return new WaitForEndOfFrame();
        }

        Player.get.Talk.all_controll = false;

        while (SubtitleManager.get.act)
            yield return new WaitForEndOfFrame();

        yield return new WaitForSeconds(1f);

        key += 1;
        SubtitleManager.get.Say(key);



        float option_time = 0f;

        while (InputManager.get.gameType != "computer")
        {
            if (!SubtitleManager.get.act)
                option_time += Time.deltaTime;

            if (option_time > 40f)
            {
                key_mistake += 10;
                SubtitleManager.get.Say(key_mistake);

                option_time = 0f;
            }

            yield return new WaitForEndOfFrame();
        }

        option_time = 0f;

        if (computer.Windows.current_window != "Rules") 
        {
            key += 2;
            SubtitleManager.get.Say(key);

            while (computer.Windows.current_window != "Desktop")
            {
                yield return new WaitForEndOfFrame();
            }

            key += 3;
            SubtitleManager.get.Say(key);

            while (computer.Windows.current_window != "Rules")
            {
                if (!SubtitleManager.get.act)
                    option_time += Time.deltaTime;

                if (option_time > 40f)
                {
                    key_mistake += 11;
                    SubtitleManager.get.Say(key_mistake);

                    option_time = 0f;
                }

                yield return new WaitForEndOfFrame();
            }
        }

        key += 4;
        SubtitleManager.get.Say(key);

        while (SubtitleManager.get.act)
            yield return new WaitForEndOfFrame();


        Player.get.Talk.all_controll = true;

        Cheat.RandomScholarsCheatSet(scholars, TutorialScholarController.calculate);
        Watcher.Reset();

        option = true;
        int option_num = 0;
        option_time = 0f;

        while (option)
        {
            if (Watcher.execute)
            {
                if (Player.get.Talk.scholar.Cheat.cheating)
                {
                    option = false;
                }
                else
                {
                    while (SubtitleManager.get.act)
                        yield return new WaitForEndOfFrame();

                    key_mistake += 2;
                    SubtitleManager.get.Say(key_mistake);
                    option_num++;

                    if (option_num == 3)
                    {
                        foreach (Scholar s in scholars)
                        {
                            s.Execute.EndExamForScholar();
                        }

                        while (SubtitleManager.get.act)
                            yield return new WaitForEndOfFrame();

                        key_mistake += 3;
                        SubtitleManager.get.Say(key_mistake);

                        while (SubtitleManager.get.act)
                            yield return new WaitForEndOfFrame();

                        Cheat.RandomScholarsCheatSet(scholars, TutorialScholarController.calculate);
                        option_num = 0;
                    }
                }

                option_time = 0f;
                Watcher.Reset();
            }




            if (!SubtitleManager.get.act)
                option_time += Time.deltaTime;

            if (option_time > 40f)
            {
                key_mistake += 8;
                SubtitleManager.get.Say(key_mistake);

                option_time = 0f;
            }

            yield return new WaitForEndOfFrame();
        }


        Player.get.Talk.all_controll = false;

        while (SubtitleManager.get.act)
            yield return new WaitForEndOfFrame();

        yield return new WaitForSeconds(1f);

        key += 5;
        SubtitleManager.get.Say(key);


        foreach (Scholar s in scholars)
        {
            s.Execute.EndExamForScholar();
        }

        yield return new WaitForSeconds(5f);

        while (SubtitleManager.get.act)
            yield return new WaitForEndOfFrame();

        Player.get.Talk.all_controll = true;

        Cheat.RandomScholarsCheatSet(scholars, TutorialScholarController.note);
        Watcher.Reset();

        option = true;

        option_time = 0f;

        while (option)
        {

            if (Watcher.execute)
            {
                if (Player.get.Talk.scholar.Cheat.IsTryToCheat())
                {
                    option = false;
                }
                else
                {
                    while (SubtitleManager.get.act)
                        yield return new WaitForEndOfFrame();

                    key_mistake += 4;
                    SubtitleManager.get.Say(key_mistake);
                    option_num++;

                    if (option_num == 3)
                    {
                        foreach (Scholar s in scholars)
                        {
                            s.Execute.EndExamForScholar();
                        }

                        while (SubtitleManager.get.act)
                            yield return new WaitForEndOfFrame();

                        key_mistake += 5;
                        SubtitleManager.get.Say(key_mistake);

                        while (SubtitleManager.get.act)
                            yield return new WaitForEndOfFrame();

                        Cheat.RandomScholarsCheatSet(scholars, TutorialScholarController.calculate);
                        option_num = 0;
                    }
                }
                option_time = 0f;
                Watcher.Reset();

            }


            if (!SubtitleManager.get.act)
                option_time += Time.deltaTime;

            if (option_time > 40f)
            {
                key_mistake += 9;
                SubtitleManager.get.Say(key_mistake);

                option_time = 0f;
            }


            yield return new WaitForEndOfFrame();
        }

        Player.get.Talk.all_controll = false;

        StartCoroutine(Tutorial_Part_2());
    }






    private IEnumerator Tutorial_Part_2()
    {
        while (SubtitleManager.get.act)
            yield return new WaitForEndOfFrame();

        yield return new WaitForSeconds(1f);

        Player.get.Talk.all_controll = false;
        key += 6;
        SubtitleManager.get.Say(key);

        foreach (Scholar s in scholars)
        {
            s.Execute.EndExamForScholar();
        }



        while (SubtitleManager.get.act)
            yield return new WaitForEndOfFrame();

        Player.get.Talk.all_controll = true;
        Player.get.Talk.answer_no_control = true;

        Hint.Set(GetP.actions.Answer_No);


        Watcher.Reset();
        scholars[1].ResetType();
        scholars[1].Action.DoAction("Cheating_Toilet_3");

        while (!scholars[1].Question.question)
            yield return new WaitForEndOfFrame();

        bool option = true;

        while (option)
        {
            if (Watcher.answer_no)
            {
                option = false;
            }

            if (!scholars[1].Question.question && !scholars[1].Question.question_answered)
            {
                key_mistake += 6;
                SubtitleManager.get.Say(key_mistake);

                while (SubtitleManager.get.act)
                    yield return new WaitForEndOfFrame();

                scholars[1].Action.DoAction("Cheating_Toilet_3");

                while (!scholars[1].Question.question)
                    yield return new WaitForEndOfFrame();
            }

            yield return new WaitForEndOfFrame();
        }

        Hint.Disable();

        while (SubtitleManager.get.act)
            yield return new WaitForEndOfFrame();

        Player.get.Talk.all_controll = false;

        yield return new WaitForSeconds(1f);

        key += 7;
        SubtitleManager.get.Say(key);


        while (SubtitleManager.get.act)
            yield return new WaitForEndOfFrame();

        Player.get.Talk.all_controll = true;
        Player.get.Talk.answer_yes_control = true;


        Hint.Set(GetP.actions.Answer_Yes);

        Watcher.Reset();
        scholars[1].Action.DoAction("Cheating_Toilet_3");

        while (!scholars[1].Question.question)
            yield return new WaitForEndOfFrame();


        option = true;

        while (option)
        {
            if (Watcher.answer_yes)
            {
                option = false;
            }

            if (!scholars[1].Question.question)
            {
                if (!scholars[1].Question.question_answered)
                {
                    key_mistake += 6;
                    SubtitleManager.get.Say(key_mistake);

                    while (SubtitleManager.get.act)
                        yield return new WaitForEndOfFrame();

                    scholars[1].Action.DoAction("Cheating_Toilet_3");

                    while (!scholars[1].Question.question)
                        yield return new WaitForEndOfFrame();
                }
                else if (!scholars[1].Question.answer)
                {
                    while (SubtitleManager.get.act)
                        yield return new WaitForEndOfFrame();

                    key_mistake += 7;
                    SubtitleManager.get.Say(key_mistake);

                    while (SubtitleManager.get.act)
                        yield return new WaitForEndOfFrame();

                    scholars[1].Action.DoAction("Cheating_Toilet_3");

                    while (!scholars[1].Question.question)
                        yield return new WaitForEndOfFrame();
                }
            }

            yield return new WaitForEndOfFrame();
        }

        Hint.Disable();
        Player.get.Talk.all_controll = false;

        while (SubtitleManager.get.act)
            yield return new WaitForEndOfFrame();

        yield return new WaitForSeconds(1f);

        key += 8;
        SubtitleManager.get.Say(key);

        while (SubtitleManager.get.act)
            yield return new WaitForEndOfFrame();

        Player.get.Talk.all_controll = true;
        Player.get.Talk.execute_control = true;





        option = true;
        float option_time = 0f;

        while (!Watcher.execute)
        {

            if(Eyes.obj.tag == "Note" && option)
            {
                option = false;
                key += 9;
                SubtitleManager.get.Say(key);
            }

            if (!option)
            {

                if (!SubtitleManager.get.act)
                    option_time += Time.deltaTime;

                if (option_time > 40f)
                {
                    key_mistake += 12;
                    SubtitleManager.get.Say(key);

                    option_time = 0f;
                }
            }

            yield return new WaitForEndOfFrame();
        }

        while (SubtitleManager.get.act)
            yield return new WaitForEndOfFrame();

        Player.get.Talk.all_controll = false;

        yield return new WaitForSeconds(1f);

        key += 10;
        SubtitleManager.get.Say(key);

        while (SubtitleManager.get.act)
            yield return new WaitForEndOfFrame();


        EndLevel();
    }



    private void EndLevel()
    {

        Player.get.Talk.all_controll = true;
        InputManager.get.Controls.Gameplay.HUD.Enable();

        GameManager.get.SwitchLevel(LevelManager.levels.Level_1);

    }
}
