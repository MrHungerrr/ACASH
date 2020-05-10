using UnityEngine;
using System.Collections;
using TMPro;

public class ScoreLine
{
    public TextMeshProUGUI row_count;
    public TextMeshProUGUI row_rep;

    public ScoreLine(TextMeshProUGUI count, TextMeshProUGUI reputation)
    {
        row_count = count;
        row_rep = reputation;
    }

    public ScoreLine(TextMeshProUGUI reputation)
    {
        row_count = null;
        row_rep = reputation;
    }

    public void SetScore(ScoreItem score)
    {
        Text(score.count, score.rep);
    }





    private void Text(int count, int reputation)
    {
        if (row_count != null)
        {
            row_count.text = count.ToString();

            if (reputation > 0)
                row_rep.text = "+" + reputation.ToString();
            else
                row_rep.text = reputation.ToString();
        }
        else
        {
            Debug.LogError("Error");
        }
    }

    public void AmountSet(int reputation)
    {
        if (row_count == null)
        {
            if (reputation > 0)
                row_rep.text = "+" + reputation.ToString();
            else
                row_rep.text = reputation.ToString();
        }
        else
        {
            Debug.LogError("Error");
        }
    }

    public void TotalSet(int reputation)
    {
        if (row_count == null)
        {
            row_rep.text = reputation.ToString();
        }
        else
        {
            Debug.LogError("Error");
        }
    }
}
