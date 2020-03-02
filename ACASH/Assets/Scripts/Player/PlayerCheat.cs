using UnityEngine;
using System.Collections;

public static class PlayerCheat
{
    private static KeyWord key = new KeyWord("Report"); 

    public static void IsExecuteRight(Scholar scholar)
    {
        KeyWord key_buf = new KeyWord(key);

        if (scholar.Cheat.IsTryToCheat())
        {
            ScoreManager.get.Plus(ScoreManager.scores_names.Executed_Right);
            key_buf += "Executed_Right";
        }
        else
        {
            ScoreManager.get.Plus(ScoreManager.scores_names.Executed_Wrong);
            key_buf += "Executed_Wrong";
        }


        HUDManager.get.ReportHUD(key_buf);
        //Report(key_buf);
        //Вставить показывание очков за действие.
    }


    public static void IsAnswerRight(Scholar scholar, bool answer)
    {
        KeyWord key_buf = new KeyWord(key);

        if (answer)
        {
            ScoreManager.get.Plus(ScoreManager.scores_names.Answered_Right);
            key_buf += "Answered_Right";
        }
        else
        {
            ScoreManager.get.Plus(ScoreManager.scores_names.Answered_Wrong);
            key_buf += "Answered_Wrong";
        }



        HUDManager.get.ReportHUD(key_buf);
        //Report(key_buf);
        //Вставить показывание очков за действие.
    }


    private static IEnumerator Report(KeyWord key)
    {
        yield return new WaitForSeconds(2f);

        HUDManager.get.ReportHUD(key);
    }
}
