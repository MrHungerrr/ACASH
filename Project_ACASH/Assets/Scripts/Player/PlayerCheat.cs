using UnityEngine;
using System.Collections;

public static class PlayerCheat
{
    private static KeyWord key = new KeyWord("Report"); 

    public static void IsExecuteRight(Scholar scholar)
    {
        key.Reset();

        if (scholar.Cheat.IsTryToCheat())
        {
            ScoreManager.Instance.Plus(ScoreManager.scores_names.Executed_Right);
            key += "Executed_Right";
        }
        else
        {
            ScoreManager.Instance.Plus(ScoreManager.scores_names.Executed_Wrong);
            key += "Executed_Wrong";
            HUDManager.Instance.ReportHUD(key);
        }


        //Report(key_buf);
        //Вставить показывание очков за действие.
    }


    public static void IsAnswerRight(Scholar scholar, bool answer)
    {
        key.Reset();

        if (answer)
        {
            ScoreManager.Instance.Plus(ScoreManager.scores_names.Answered_Right);
            key += "Answered_Right";
        }
        else
        {
            ScoreManager.Instance.Plus(ScoreManager.scores_names.Answered_Wrong);

            if (scholar.Question.question_answered)
            {
                key += "Answered_Wrong";
            }
            else
            {
                key += "Answered_No_Answer";
            }
        }

        HUDManager.Instance.ReportHUD(key);

        //Report(key_buf);
        //Вставить показывание очков за действие.
    }


    private static IEnumerator Report(KeyWord key)
    {
        yield return new WaitForSeconds(2f);

        HUDManager.Instance.ReportHUD(key);
    }
}
