using System.Collections.Generic;
using UnityEngine;
using Single;

public class ScoreManager : Singleton<ScoreManager>
{

    public Dictionary<string, ScoreInfo> scores = new Dictionary<string, ScoreInfo>();

    [HideInInspector]
    public string[] scores_names = new string[]
    {
        "Left",
        "Cheated",
        "Not Finished",
        "Executed Right",
        "Executed Wrong",
        "Answered Right",
        "Answered Wrong",
    };

    [HideInInspector]
    public int reputation;
    [HideInInspector]
    public int kindness;
    [HideInInspector]
    public int reputation_amount;
    [HideInInspector]
    public int kindness_amount;
    [HideInInspector]
    public int bulls;
    [HideInInspector]
    public int jokes;


    private void Awake()
    {
        reputation = 500;
        kindness = 25;

        int[] rep_coef = new int[7]
        {
            200, // Left
            -400, // Cheated
            -200, // Not Finished
            200, // Executed Right
            -100, // Executed Wrong
            100, // Answered Right
            -100, // Answered Wrong
        };

        int[] kind_coef = new int[7]
        {
            2, // Left
            0, // Cheated
            0, // Not Finished
            -2, // Executed Right
            -4, // Executed Wrong
            2, // Answered Right
            -2, // Answered Wrong
        };

        for (int i = 0; i < scores_names.Length; i++)
        {
            ScoreInfo buf = new ScoreInfo(rep_coef[i], kind_coef[i]);
            scores.Add(scores_names[i], buf);
        }

    }

    public void Bull(Scholar scholar, bool strong)
    {
        if (strong)
        {
            bulls++;
        }
        else
        {
            jokes++;
        }
    }

    public void Execute(string reason, Scholar scholar)
    {
        if (scholar.reason[reason])
        {
            scores["Executed Right"].Plus();
        }
        else
        {
            scores["Executed Wrong"].Plus();
        }
    }

    public void QuestionScore(string topic, bool answer)
    {
        if ((ExamManager.get.banned[topic] || !answer) && (!ExamManager.get.banned[topic] || answer))
        {
            scores["Answered Right"].Plus();
        }
        else
        {
            scores["Answered Wrong"].Plus();
        }
    }

    public void EndScore()
    {
        //Need Change
        scores["Left"].ChangeCount(1); //Get From Scholar Manager
        scores["Cheated"].ChangeCount(1); //Get From Scholar Manager
        scores["Not Finished"].ChangeCount(1); //Get From Scholar Manager
        Amount();
    }

    private void Amount()
    {
        reputation_amount = 0;
        kindness_amount = 0;

        for (int i = 0; i < scores_names.Length; i++)
        {
            reputation_amount += GetScore(i).rep;
            kindness_amount += GetScore(i).kind;
        }
    }



    public void Zeroing()
    {
        for (int i = 0; i < scores_names.Length; i++)
        {
            GetScore(i).Zeroing();
        }
    }

    public ScoreInfo GetScore(int i)
    {
        return scores[scores_names[i]];
    }
}

public class ScoreInfo
{
    public int count;
    public int rep;
    public int kind;
    private int rep_coef;
    private int kind_coef;

    public ScoreInfo(int reputation_coef, int kindness_coef)
    {
        rep_coef = reputation_coef;
        kind_coef = kindness_coef;
        Zeroing();
    }

    public void Plus()
    {
        ChangeCount(count + 1);
    }

    public void Minus()
    {
        ChangeCount(count + 1);
    }

    public void Zeroing()
    {
        ChangeCount(0);
    }

    public void ChangeCount(int value)
    {
        int buf_rep = (value - count) * rep_coef;
        ScoreManager.get.reputation += buf_rep;
        ScoreManager.get.kindness += (value - count) * kind_coef;

        count = value;
        rep = value * rep_coef;
        kind = value * kind_coef;

        HUDManager.get.ChangeReputationHUD(buf_rep);
    }


}
