using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using N_BH;

public class ScoreAgent : Singleton<ScoreAgent>
{

    private Dictionary<string, ScoreLine> score_lines = new Dictionary<string, ScoreLine>();
    private ScoreLine amount;
    private ScoreLine total;




    void Start()
    {
        Transform Var = transform.Find("Variables");
        Transform Count = Var.Find("Count");
        Transform Reputation = Var.Find("Reputation");
        Transform Kindness = Var.Find("Kindness");

        TextMeshProUGUI buf_count;
        TextMeshProUGUI buf_rep;

        for (int i = 0; i < ScoreManager.get.scores_names.Length; i++)
        {

            buf_count = Count.Find(ScoreManager.get.scores_names[i]).GetComponent<TextMeshProUGUI>();
            buf_rep = Reputation.Find(ScoreManager.get.scores_names[i]).GetComponent<TextMeshProUGUI>();

            ScoreLine buf = new ScoreLine(buf_count, buf_rep);
            score_lines.Add(ScoreManager.get.scores_names[i], buf);
        }

        buf_rep = Reputation.Find("Amount").GetComponent<TextMeshProUGUI>();
        amount = new ScoreLine(null, buf_rep);

        buf_rep = Reputation.Find("Total").GetComponent<TextMeshProUGUI>();
        total = new ScoreLine(null, buf_rep);

        Set();
    }

    public void Set()
    {
        ScoreManager.get.EndScore();

        for (int i = 0; i < ScoreManager.get.scores_names.Length; i++)
        {
            GetScoreLine(i).SetScore(GetScore(i));
        }

        amount.AmountSet(ScoreManager.get.reputation_amount);
        total.TotalSet(ScoreManager.get.reputation);
    }

    private ScoreInfo GetScore(int i)
    {
        return ScoreManager.get.GetScore(i);
    }

    private ScoreLine GetScoreLine(int i)
    {
        return score_lines[ScoreManager.get.scores_names[i]];
    }
}

public class ScoreLine
{
    public TextMeshProUGUI row_count;
    public TextMeshProUGUI row_rep;

    public ScoreLine(TextMeshProUGUI count, TextMeshProUGUI reputation)
    {
        row_count = count;
        row_rep = reputation;
    }

    public void SetScore(ScoreInfo score)
    {
        Text(score.count, score.rep);
    }


    private void Text(int count, int reputation)
    {
        row_count.text = count.ToString();

        if (reputation > 0)
            row_rep.text = "+" + reputation.ToString();
        else
            row_rep.text = reputation.ToString();
    }

    public void AmountSet(int reputation)
    {
        if (reputation > 0)
            row_rep.text = "+" + reputation.ToString();
        else
            row_rep.text = reputation.ToString();
    }

    public void TotalSet(int reputation)
    {
        row_rep.text = reputation.ToString();
    }
}
