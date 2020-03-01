using UnityEngine;
using System.Collections;

public static class PlayerCheat
{
    public static void IsExecuteRight(Scholar scholar)
    {
        if (scholar.Cheat.IsTryToCheat())
            ScoreManager.get.Plus(ScoreManager.scores_names.Executed_Right);
        else
            ScoreManager.get.Plus(ScoreManager.scores_names.Executed_Wrong);

        //Вставить показывание очков за действие.
    }


    public static void IsAnswerRight(Scholar scholar, bool answer)
    {
        if (answer)
            ScoreManager.get.Plus(ScoreManager.scores_names.Answered_Right);
        else
            ScoreManager.get.Plus(ScoreManager.scores_names.Answered_Wrong);

        //Вставить показывание очков за действие.
    }
}
