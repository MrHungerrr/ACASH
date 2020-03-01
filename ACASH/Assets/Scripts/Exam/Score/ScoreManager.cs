using System.Collections.Generic;
using UnityEngine;
using Single;
using System;

public class ScoreManager : Singleton<ScoreManager>
{

    private Dictionary<scores_names, ScoreItem> scores = new Dictionary<scores_names, ScoreItem>();
    public int scores_length { get; private set ; }

    private ScoreAgent[] agents;

    public enum scores_names
    {
        Left,
        Cheated,
        Not_Finished,
        Executed_Right,
        Executed_Wrong,
        Answered_Right,
        Answered_Wrong,
    };




    private int reputation_saved;
    private int kindness_saved;
    [HideInInspector]
    public int reputation;
    [HideInInspector]
    public int kindness;
    [HideInInspector]
    public int reputation_amount;
    [HideInInspector]
    public int kindness_amount;


    public void Setup()
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

        scores_length = Enum.GetNames(typeof(scores_names)).Length;

        for (int i = 0; i < scores_length; i++)
        {
            ScoreItem buf = new ScoreItem(rep_coef[i], kind_coef[i]);
            scores.Add((scores_names)i, buf);
        }

        agents = FindObjectsOfType<ScoreAgent>();


        foreach(ScoreAgent a in agents)
        {
            a.Setup();
        }

        ExamManager.get.ExamDone += FinalScore;
    }





    private void Amount()
    {
        reputation_amount = 0;
        kindness_amount = 0;

        for (int i = 0; i < scores_length; i++)
        {
            reputation_amount += GetScore(i).rep;
            kindness_amount += GetScore(i).kind;
        }
    }

    public void FinalScore()
    {
        int left = ScholarManager.get.GetCount(ScholarManager.Left);
        int cheated = ScholarManager.get.GetCount(ScholarManager.Cheated);
        int notFinished = ScholarManager.get.GetCount(ScholarManager.NotFinished);


        GetScore(scores_names.Left).ChangeCount(left);
        GetScore(scores_names.Cheated).ChangeCount(cheated);
        GetScore(scores_names.Not_Finished).ChangeCount(notFinished);


        Amount();


        reputation = reputation_saved + reputation_amount;
        kindness = kindness_saved + kindness_amount;

        foreach(ScoreAgent a in agents)
        {
            a.SetScore();
        }
    }

    public void Accept()
    {
        reputation_saved = reputation;
        kindness_saved = kindness;

        Zeroing();
    }


    public void Plus(scores_names score)
    {
        scores[score]++;
    }


    public void Minus(scores_names score)
    {
        scores[score]--;
    }



    public void Zeroing()
    {
        for (int i = 0; i < scores_length; i++)
        {
            GetScore(i).Zeroing();
        }
    }

    public ScoreItem GetScore(int i)
    {
        return scores[(scores_names)i];
    }


    public ScoreItem GetScore(scores_names name)
    {
        return scores[name];
    }
}
