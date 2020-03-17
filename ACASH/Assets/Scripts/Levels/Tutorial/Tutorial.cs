using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Single;

public class Tutorial : Singleton<Tutorial>
{
    private TutorialPlayerWatcher Watcher;


    public TriggerAction first_room;
    public Scholar first_room_scholar;
    public Scholar[] first_room_other_scholars = new Scholar[2];
    public StressScreen[] first_room_text_screens = new StressScreen[3];

    public Door first_room_door_enter;
    public Door first_room_door_exit;


    private void Awake()
    {
        first_room.OnEnter += StartFirstRoom;
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

        StartCoroutine(First_Room());
    }



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

    private IEnumerator Second_Room()
    {
        yield return new WaitForEndOfFrame();
    }



    private IEnumerator EndLevel()
    {
        while (SubtitleManager.get.act)
            yield return new WaitForEndOfFrame();

        Elevator.get.Open();
    }
}
