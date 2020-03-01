using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Single;

public class ScoreAgent : MonoBehaviour
{

    private Dictionary<ScoreManager.scores_names, ScoreLine> score_lines = new Dictionary<ScoreManager.scores_names, ScoreLine>();
    private ScoreLine amount;
    private ScoreLine total;



    public void Setup()
    {
        Transform Scores = transform.Find("Score").Find("Score");
        Transform Var = Scores.Find("Variables");
        Transform Count = Var.Find("Count");
        Transform Reputation = Var.Find("Reputation");

        TextMeshProUGUI text_count;
        TextMeshProUGUI text_rep;

        for (int i = 0; i < ScoreManager.get.scores_length; i++)
        {
            ScoreManager.scores_names score = (ScoreManager.scores_names)i;

            string score_name = score.ToString().Replace("_", " ");

            text_count = Count.Find(score_name).GetComponent<TextMeshProUGUI>();
            text_rep = Reputation.Find(score_name).GetComponent<TextMeshProUGUI>();

            ScoreLine buf = new ScoreLine(text_count, text_rep);
            score_lines.Add(score, buf);
        }

        text_rep = Reputation.Find("Amount").GetComponent<TextMeshProUGUI>();
        amount = new ScoreLine(text_rep);

        text_rep = Reputation.Find("Total").GetComponent<TextMeshProUGUI>();
        total = new ScoreLine(text_rep);
    }



    public void SetScore()
    {
        for (int i = 0; i < ScoreManager.get.scores_length; i++)
        {
            GetScoreLine(i).SetScore(GetScore(i));
        }

        amount.AmountSet(ScoreManager.get.reputation_amount);
        total.TotalSet(ScoreManager.get.reputation);
    }



    private ScoreItem GetScore(int i)
    {
        return ScoreManager.get.GetScore(i);
    }

    private ScoreLine GetScoreLine(int i)
    {
        return score_lines[(ScoreManager.scores_names)i];
    }
}

