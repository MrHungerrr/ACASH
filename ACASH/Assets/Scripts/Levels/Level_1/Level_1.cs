using System.Collections;
using PlayerOptions;
using UnityEngine;
using Single;

public class Level_1 : Singleton<Level_1>
{


    private KeyWord key = new KeyWord("Level_1");
    private KeyWord key_mistake = new KeyWord("Level_1", "Mistake");

    private KeyAction hint;


    private void Start()
    {
        StartLevel();

        ExamManager.get.ExamDone += StartEnding;
        ExamManager.get.PrepareDone += StartTabHint;
    }

    private void StartLevel()
    {
        StartCoroutine(ElevatorRoom());
    }


    private IEnumerator ElevatorRoom()
    {
        //while (!LevelManager.get.IsLoad())
        //  yield return new WaitForEndOfFrame();

        GameManager.get.StartLevel();


        Player.get.Move.Position(Elevator.get.position);

        yield return new WaitForSeconds(1f);

        key *= "Introdaction";

        HUDManager.get.IntrodactionHUD(key);

        yield return new WaitForSeconds(3f);

        HUDManager.get.CloseIntrodactionHUD();
        ElevatorController.get.Close();

        yield return new WaitForSeconds(1f);

        GameManager.get.StartGame();

        key *= "Begining";

        SubtitleManager.get.Say(key);

        while (SubtitleManager.get.act)
            yield return new WaitForEndOfFrame();

        ElevatorController.get.Ready();

        yield return new WaitForSeconds(1f);

        ElevatorController.get.Open();


        while(Elevator.get.inside)
            yield return new WaitForEndOfFrame();

        GameManager.get.StartExam();
    }



    private void StartTabHint()
    {
        HUDManager.get.HintHUD(GetP.actions.Hud);
        hint = new KeyAction();
        hint.Setup(GetP.actions.Hud);
        hint.OnKeyDown += CloseTabHint;

        ExamManager.get.PrepareDone -= StartTabHint;
    }


    private void CloseTabHint()
    {
        hint.Remove();
        HUDManager.get.CloseHintHUD();
    }






    private void StartEnding()
    {
        ExamManager.get.ExamDone -= StartEnding;
        StartCoroutine(Ending());
    }


    private IEnumerator Ending()
    {
        key *= "Ending";
        key_mistake *= "Ending";
        float option_time = 0f;

        key += 0;
        SubtitleManager.get.Say(key);

        while (InputManager.get.gameType != "computer")
        {
            if(!SubtitleManager.get.act)
                option_time += Time.deltaTime;

            if(option_time >10f)
            {
                key_mistake += 0;
                SubtitleManager.get.Say(key_mistake);
                    
                option_time = 0f;
            }

            yield return new WaitForEndOfFrame();
        }



        key += 1;

        SubtitleManager.get.Say(key);
        Elevator.get.Open();

        while (SubtitleManager.get.act)
            yield return new WaitForEndOfFrame();

        option_time = 0f;

        while (!Elevator.get.inside)
        {
            if (!SubtitleManager.get.act)
                option_time += Time.deltaTime;

            if (option_time > 10f)
            {
                key_mistake += 1;
                SubtitleManager.get.Say(key_mistake);

                option_time = 0f;
            }

            yield return new WaitForEndOfFrame();
        }




        key += 2;

        SubtitleManager.get.Say(key);

        while (SubtitleManager.get.act)
            yield return new WaitForEndOfFrame();

        EndLevel();
    }


    private void EndLevel()
    {
        FadeHUDController.get.FastFade(true);
        LevelManager.get.LoadInstead("Tutorial_2");
    }
}
