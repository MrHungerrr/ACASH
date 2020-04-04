using System.Collections;
using PlayerOptions;
using UnityEngine;
using Single;

public class Level_1 : Singleton<Level_1>
{


    private KeyWord key = new KeyWord("Level_1");
    private KeyWord key_mistake = new KeyWord("Level_1", "Mistake");

    private KeyAction zoom;


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
        zoom.Setup(GetP.actions.Hud);

        zoom.OnKeyDown += CloseTabHint;

        ExamManager.get.PrepareDone -= StartTabHint;
    }


    private void CloseTabHint()
    {
        zoom.Remove();
        HUDManager.get.CloseHintHUD();
    }






    private void StartEnding()
    {
        ExamManager.get.ExamDone -= StartEnding;
        StartCoroutine(Ending());
    }


    private IEnumerator Ending()
    {

        while (InputManager.get.gameType != "computer")
            yield return new WaitForEndOfFrame();

        key *= "Ending";

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
