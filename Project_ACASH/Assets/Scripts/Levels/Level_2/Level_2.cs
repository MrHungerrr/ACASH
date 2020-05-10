using System.Collections;
using PlayerOptions;
using UnityEngine;
using Single;

public class Level_2 : A_Level
{

    private KeyAction hint;


    protected override void Setup()
    {
        Key("Level_2");

        LevelSettings.get.ExamOver.AddListener(StartEnding);
        ExamManager.get.ChillDone.AddListener(StartShoutHint);
    }

    protected override void Begin()
    {
        StartCoroutine(ElevatorRoom());
    }


    private IEnumerator ElevatorRoom()
    {
        //while (!LevelManager.get.IsLoad())
        //  yield return new WaitForEndOfFrame();

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

        key += 0;
        SubtitleManager.get.Say(key);

        while (SubtitleManager.get.act)
            yield return new WaitForEndOfFrame();

        ElevatorController.get.Ready();

        yield return new WaitForSeconds(1f);

        ElevatorController.get.Open();


        while (Elevator.get.inside)
            yield return new WaitForEndOfFrame();

        key += 1;
        SubtitleManager.get.Say(key);

        GameManager.get.StartExam();

        Player.get.Talk.AllowAll();
    }


    private void StartShoutHint()
    {
        StartCoroutine(Hinting());
        ExamManager.get.ChillDone.RemoveListener(StartShoutHint);
    }


    private IEnumerator Hinting()
    {
        yield return new WaitForSeconds(10f);

        HUDManager.get.HintHUD(GetP.actions.Shout);
        hint = new KeyAction();
        hint.Setup(GetP.actions.Shout);
        hint.OnKeyDown.AddListener(CloseShoutHint);
    }

    private void CloseShoutHint()
    {
        hint.Remove();
        HUDManager.get.CloseHintHUD();
    }




    private void StartEnding()
    {
        LevelSettings.get.ExamOver.RemoveListener(StartEnding);
        StartCoroutine(Ending());
    }


    private IEnumerator Ending()
    {
        key *= "Ending";


        InputManager.get.SwitchGameInput("cutscene");

        FadeHUDController.get.FastFade(true);

        key += 0;

        SubtitleManager.get.Say(key);

        while (SubtitleManager.get.act)
            yield return new WaitForEndOfFrame();

        StartCoroutine(EndLevel());
    }

    private IEnumerator EndLevel()
    {
        key *= "Thanks";

        HUDManager.get.IntrodactionHUD(key);

        yield return new WaitForSeconds(25f);

        HUDManager.get.CloseIntrodactionHUD();


        GameManager.get.MainMenu();
    }
}
