using System.Collections;
using PlayerOptions;
using UnityEngine;
using Single;
using ScholarOptions;
using TMPro;

public class Tutorial_2 : Singleton<Tutorial_2>
{
    private TutorialPlayerEyes Eyes;
    private TutorialPlayerWatcher Watcher;
    private TutorialScholarController ScholarController;
    private KeyHint Hint;

    private KeyWord key = new KeyWord("Tutorial_2");
    private KeyWord key_mistake = new KeyWord("Tutorial_2", "Mistake");




    [Header("First Room")]
    public Scholar[] scholars_first;
    public Door door_exit;
    public TriggerAction first_room;


    [Header("Second Room")]
    public TriggerAction second_room;
    private Scholar[] scholars;
    public Door door_enter;
    public TeacherComputer computer;
    public GameObject pointer;
    public TextMeshProUGUI countdown;



    private void Start()
    {

        Watcher = new TutorialPlayerWatcher(false);

        Hint = GetComponent<KeyHint>();
        Eyes = GetComponent<TutorialPlayerEyes>();
        ScholarController = new TutorialScholarController();

        second_room.OnEnter += StartSecondRoom;
        Player.get.Talk.DenyAll();

        StartCoroutine(StartLevel());
    }


    private IEnumerator StartLevel()
    {
        //while (!LevelManager.get.IsLoad())
          //  yield return new WaitForEndOfFrame();

        GameManager.get.StartLevel();

        Transform point = GameObject.FindGameObjectWithTag("PlayerPoint").transform;
        Player.get.Move.Position(point.position);
        Destroy(point.gameObject);

        yield return new WaitForSeconds(1f);

        key *= "Introdaction";

        HUDManager.get.IntrodactionHUD(key);

        yield return new WaitForSeconds(3f);

        HUDManager.get.CloseIntrodactionHUD();
        door_exit.locked = true;
        pointer.SetActive(false);
        scholars = ScholarManager.get.scholars;

        yield return new WaitForSeconds(1f);

        GameManager.get.StartGame();

        key *= "Begining";

        SubtitleManager.get.Say(key);

        while (SubtitleManager.get.act)
            yield return new WaitForEndOfFrame();

        StartFirstRoom();
    }




    //===================================================================================================================================
    // Первая комната
    //===================================================================================================================================

    private void StartFirstRoom()
    {
        StartCoroutine(First_Room());
    }


    private IEnumerator First_Room()
    {
        key *= "First_Room";
        key_mistake *= "First_Room";


        //================================================================================================================================
        //Сказать что-то жесткое

        scholars_first[0].ResetType();

        Hint.Set(GetP.actions.Zoom);

        while (!Player.get.Camera.zoom)
            yield return new WaitForEndOfFrame();

        Hint.Disable();

        key += 0;

        SubtitleManager.get.Say(key);

        yield return new WaitForSeconds(1f);



        while (SubtitleManager.get.act)
            yield return new WaitForEndOfFrame();


        Watcher.Reset();
        Hint.Set(GetP.actions.Talk_Bad);
        Player.get.Talk.all_controll = true;
        Player.get.Talk.talk_bad_control = true;


        while(!Watcher.talk_bad)
        {
            yield return new WaitForEndOfFrame();
        }

        Hint.Disable();

        while (SubtitleManager.get.act)
            yield return new WaitForEndOfFrame();




        //================================================================================================================================
        //Поднять стресс до 70 жесткими

        Player.get.Talk.all_controll = false;

        yield return new WaitForSeconds(1f);

        key += 1;

        SubtitleManager.get.Say(key);

        while (SubtitleManager.get.act)
            yield return new WaitForEndOfFrame();


        Watcher.Reset();
        Player.get.Talk.all_controll = true;
        float option_time = 0f;
        bool option = true;

        while (option)
        {
            if(Watcher.talk_bad)
            {

                if (scholars_first[0].Stress.value >= 99f)
                {
                    option = false;
                }

                option_time = 0f;
                Watcher.Reset();
            }

            if (!SubtitleManager.get.act)
            {
                option_time += Time.deltaTime;
            }

            if(option_time >= 15f)
            {
                while (SubtitleManager.get.act)
                    yield return new WaitForEndOfFrame();

                key_mistake += 0;

                SubtitleManager.get.Say(key_mistake);

                option_time = 0f;
            }

            yield return new WaitForEndOfFrame();
        }

        while (SubtitleManager.get.act)
            yield return new WaitForEndOfFrame();




        //================================================================================================================================
        //Сказать че-нить легкое

        Player.get.Talk.all_controll = false;

        yield return new WaitForSeconds(1f);

        key += 2;

        SubtitleManager.get.Say(key);

        while (SubtitleManager.get.act)
            yield return new WaitForEndOfFrame();


        scholars_first[0].Stress.Reset();
        Hint.Set(GetP.actions.Talk_Good);
        Watcher.Reset();
        Player.get.Talk.all_controll = true;
        Player.get.Talk.talk_good_control = true;
        option = true;


        while (option)
        {
            if (Watcher.done)
            {
                if (Watcher.talk_good)
                {
                    option = false;
                }
                else
                {
                    while (SubtitleManager.get.act)
                        yield return new WaitForEndOfFrame();

                    key_mistake += 1;
                    SubtitleManager.get.Say(key_mistake);
                }
            }

            yield return new WaitForEndOfFrame();
        }

        Hint.Disable();

        while (SubtitleManager.get.act)
            yield return new WaitForEndOfFrame();






        //================================================================================================================================
        //Поднять стресс до 30 легкими

        Player.get.Talk.all_controll = false;

        yield return new WaitForSeconds(1f);

        key += 3;

        SubtitleManager.get.Say(key);

        while (SubtitleManager.get.act)
            yield return new WaitForEndOfFrame();

        Watcher.Reset();
        Player.get.Talk.all_controll = true;
        option_time = 0f;
        option = true;


        while (option)
        {
            if (Watcher.done)
            {
                if (Watcher.talk_good)
                {
                    if (scholars_first[0].Stress.value >= 30)
                    {
                        option = false;
                    }
                }
                else
                {
                    while (SubtitleManager.get.act)
                        yield return new WaitForEndOfFrame();

                    key_mistake += 1;
                    SubtitleManager.get.Say(key_mistake);
                    scholars_first[0].Stress.Reset();
                }

                option_time = 0f;
                Watcher.Reset();
            }


            if (!SubtitleManager.get.act)
                option_time += Time.deltaTime;

            if (option_time >= 15f)
            {
                while (SubtitleManager.get.act)
                    yield return new WaitForEndOfFrame();

                key_mistake += 2;

                SubtitleManager.get.Say(key_mistake);

                option_time = 0f;
            }

            yield return new WaitForEndOfFrame();
        }

        door_exit.locked = false;

        while (SubtitleManager.get.act)
            yield return new WaitForEndOfFrame();


        Player.get.Talk.all_controll = false;

        yield return new WaitForSeconds(1f);

        key += 4;

        SubtitleManager.get.Say(key);

        while (SubtitleManager.get.act)
            yield return new WaitForEndOfFrame();

        Player.get.Talk.all_controll = true;


        option_time = 0f;

        while(first_room.inside)
        {
            if (!SubtitleManager.get.act)
                option_time += Time.deltaTime;

            if (option_time >= 20f)
            {
                while (SubtitleManager.get.act)
                    yield return new WaitForEndOfFrame();

                key_mistake += 3;

                SubtitleManager.get.Say(key_mistake);

                option_time = 0f;
            }

            yield return new WaitForEndOfFrame();
        }
        first_room.Remove();
    }









    //===================================================================================================================================
    // Вторая комната
    //===================================================================================================================================

    private void StartSecondRoom()
    {
        second_room.Remove();

        StartCoroutine(Second_Room());
    }

    private IEnumerator Second_Room()
    {

        key *= "Second_Room";
        key_mistake *= "Second_Room";

        door_enter.Close();
        door_enter.locked = true;



        //================================================================================================================================
        //Надо подойти к ученику и посмотреть на монитор
        //Напоминание

        Player.get.Talk.all_controll = false;

        key += 0;

        SubtitleManager.get.Say(key);

        yield return new WaitForSeconds(1f);

        scholars[0].ResetType();
        scholars[0].Action.DoAction("Desk");

        while (SubtitleManager.get.act)
            yield return new WaitForEndOfFrame();

        Player.get.Talk.all_controll = true;

        bool option = true;
        float option_time = 0f;



        while(option)
        {
            if(scholars[0].Senses.Teacher.distance < 0.4f && Eyes.obj.tag == "DeskScreen")
            {
                option = false;
            }

            if(!SubtitleManager.get.act)
                option_time += Time.deltaTime;

            if(option_time > 15f)
            {
                key_mistake += 0;
                SubtitleManager.get.Say(key_mistake);

                option_time = 0f;
            }


            yield return new WaitForEndOfFrame();
        }

        while (SubtitleManager.get.act)
            yield return new WaitForEndOfFrame();






        //================================================================================================================================
        //Надо отойти от ученика но продолжать смотреть
        //Напоминание


        Player.get.Talk.all_controll = false;
        pointer.SetActive(true);


        key += 1;

        SubtitleManager.get.Say(key);

        while (SubtitleManager.get.act)
            yield return new WaitForEndOfFrame();



        Player.get.Talk.all_controll = true;
        option = true;
        option_time = 0f;

        while (option)
        {
            if (scholars[0].Senses.Teacher.distance > 1.5f && scholars[0].Senses.T_look_at_us)
            {
                option = false;
            }

            if(!SubtitleManager.get.act)
                option_time += Time.deltaTime;

            if (option_time > 15f)
            {
                key_mistake += 1;
                SubtitleManager.get.Say(key_mistake);

                option_time = 0f;
            }


            yield return new WaitForEndOfFrame();
        }

        pointer.SetActive(false);

        while (SubtitleManager.get.act)
            yield return new WaitForEndOfFrame();



        //================================================================================================================================
        //Надо зайти в компьютер
        //Напоминание


        Player.get.Talk.all_controll = false;

        key += 2;
        SubtitleManager.get.Say(key);

        while (SubtitleManager.get.act)
            yield return new WaitForEndOfFrame();

        Player.get.Talk.all_controll = true;
        option = true;
        option_time = 0f;

        while (option)
        {
            if (InputManager.get.gameType == "computer")
            {
                option = false;
            }

            if (!SubtitleManager.get.act)
                option_time += Time.deltaTime;

            if (option_time > 15f)
            {
                key_mistake += 2;
                SubtitleManager.get.Say(key_mistake);

                option_time = 0f;
            }


            yield return new WaitForEndOfFrame();
        }



        //================================================================================================================================
        //Зайдите в программу SS
        //Напоминание

        if (computer.Windows.current_window != "Student Stress")
        {
            key += 3;
            SubtitleManager.get.Say(key);

            option = true;
            option_time = 0f;

            while (option)
            {
                if (computer.Windows.current_window == "Student Stress")
                {
                    option = false;
                }

                if (!SubtitleManager.get.act)
                    option_time += Time.deltaTime;

                if (option_time > 7f)
                {
                    key_mistake += 3;
                    SubtitleManager.get.Say(key_mistake);

                    option_time = 0f;
                }

                yield return new WaitForEndOfFrame();
            }
        }

        InputManager.get.Controls.Computer.Disable();

        //================================================================================================================================
        //Держите оптимальный стресс у всех учеников
        //Напоминание
        //Обратный отсчёт


        Player.get.Talk.all_controll = false;

        key += 4;
        SubtitleManager.get.Say(key);

        yield return new WaitForSeconds(4f);

        computer.ExecuteCommand(ComputerActions.GetC.commands.Info);

        while (SubtitleManager.get.act)
            yield return new WaitForEndOfFrame();


        ComputerManager.get.Exit();
        InputManager.get.SwitchGameInput("gameplay");
        InputManager.get.Controls.Computer.Enable();

        ScholarController.RandomScholarsCheatSet(scholars, TutorialScholarController.answer);
        
        Player.get.Talk.all_controll = true;
        option = true;
        option_time = 0f;
        float option_time_2 = 0f;



        while (option)
        {
            if(ScholarController.StressCheck(scholars))
            {
                option_time_2 += Time.deltaTime;
                option_time = 0f;

                string text = "Hold on this stress...\n" + string.Format("{0:N1}",(30f - option_time_2));

                countdown.text = text;

                if(option_time_2 >= 30f)
                {
                    option = false;
                }
            }
            else
            {
                if (!SubtitleManager.get.act)
                    option_time += Time.deltaTime;

                option_time_2 = 0f;


                if (option_time >= 40f)
                {
                    key_mistake += 4;
                    SubtitleManager.get.Say(key_mistake);

                    option_time = 0f;
                }
            }

            yield return new WaitForEndOfFrame();
        }

        countdown.transform.parent.gameObject.SetActive(false);

        while (SubtitleManager.get.act)
            yield return new WaitForEndOfFrame();



        //Конец
        //================================================================================================================================

        foreach (Scholar s in scholars)
        {
            s.Execute.EndExamForScholar();
        }


        Player.get.Talk.all_controll = false;

        yield return new WaitForSeconds(1f);

        key += 5;
        SubtitleManager.get.Say(key);

        while (SubtitleManager.get.act)
            yield return new WaitForEndOfFrame();

        Player.get.Talk.all_controll = true;
        StartCoroutine(EndLevel());
    }


    private IEnumerator EndLevel()
    {
        while (SubtitleManager.get.act)
            yield return new WaitForEndOfFrame();

        Player.get.Talk.AllowAll();
        InputManager.get.SwitchGameInput("disable");
        FadeHUDController.get.FastFade(true);
        LevelManager.get.LoadInstead("Level_2");
    }

}
