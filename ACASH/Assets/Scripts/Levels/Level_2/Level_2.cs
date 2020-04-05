using System.Collections;
using PlayerOptions;
using UnityEngine;
using Single;

public class Level_2 : Singleton<Level_2>
{


    private KeyWord key = new KeyWord("Level_2");
    private KeyWord key_mistake = new KeyWord("Level_2", "Mistake");


    private void Start()
    {
        StartLevel();

        LevelSettings.get.ExamOver += StartEnding;
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


        while (Elevator.get.inside)
            yield return new WaitForEndOfFrame();

        GameManager.get.StartExam();
    }




    private void StartEnding()
    {
        LevelSettings.get.ExamOver -= StartEnding;
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

        yield return new WaitForSeconds(3f);

        HUDManager.get.CloseIntrodactionHUD();

        GameManager.get.MainMenu();
    }
}
