using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScholarOptions;
using Single;

public class CutSceneCotroller : MonoBehaviour
{

    ScholarCutSceneController Scholars;

    private void Awake()
    {
        Scholars = new ScholarCutSceneController();
        GameManager.get.StartLevel();
        GameManager.get.StartGame();
    }

    private void Start()
    {
        Scholars.Setup();
        StartCoroutine(CutScene());
    }


    private IEnumerator CutScene()
    {
        CrossHair.get.Enable(false);

        yield return new WaitForSeconds(4f);

        Scholars.ResetScholars();

        yield return new WaitForSeconds(3f);

        Scholars.StartWorking();

        yield return new WaitForSeconds(11f);

        Scholars.End();

        yield return new WaitForSeconds(2f);

        Scholars.Off();

        yield return new WaitForSeconds(4f);

        Scholars.LastStandingManDisableVeryBad();

        yield return new WaitForSeconds(7f);

        Scholars.LastStandingManDeadVeryBad();
    }

}