using UnityEngine;
using Single;


public class Difficulty: Singleton<Difficulty>
{
    private enum list
    {
        Easy,
        Normal,
        Hard
    }

    [SerializeField]
    private list difficultyType;

    [HideInInspector]
    public string difficulty;

    [SerializeField]
    [Range(0,20)]
    private int actionsCount;




    [HideInInspector]
    public ActionsCount[] actions = new ActionsCount[3];


    public void SetDifficulty()
    {
        difficulty = difficultyType.ToString();
        GetActionsCount();
    }

    private void GetActionsCount()
    {
        actions[0] = FirstWave(actionsCount);
        actions[1] = SecondWave(actionsCount);
        actions[2] = ThirdWave(actionsCount);
    }

    private ActionsCount FirstWave(int act_count)
    {
        int quotient;
        ActionsCount act;

        switch (difficulty)
        {
            case "Easy":
                {
                    act = new ActionsCount(act_count, 0,0);
                    return act;
                }
            case "Normal":
                {
                    act = new ActionsCount(act_count, 0, 0);
                    return act;
                }
            case "Hard":
                {
                    quotient = Quotient(ref act_count, 2);
                    act = new ActionsCount(quotient, quotient + act_count, 0);
                    return act;
                }
        }

        return null;
    }

    private ActionsCount SecondWave(int act_count)
    {
        int quotient;
        ActionsCount act;

        switch (difficulty)
        {
            case "Easy":
                {
                    quotient = Quotient(ref act_count, 3);
                    act = new ActionsCount(quotient * 2 + act_count, quotient, 0);
                    return act;
                }
            case "Normal":
                {
                    quotient = Quotient(ref act_count, 2);
                    act = new ActionsCount(quotient, quotient + act_count, 0);
                    return act;
                }
            case "Hard":
                {
                    quotient = Quotient(ref act_count, 3);
                    act = new ActionsCount(quotient, quotient * 2, act_count);
                    return act;
                }
        }

        return null;
    }



    private ActionsCount ThirdWave(int act_count)
    {
        int quotient;
        ActionsCount act;

        switch (difficulty)
        {
            case "Easy":
                {
                    quotient = Quotient(ref act_count, 4);
                    act = new ActionsCount(quotient * 2 + act_count, quotient, quotient);
                    return act;
                }
            case "Normal":
                {
                    quotient = Quotient(ref act_count, 3);
                    act = new ActionsCount(quotient, quotient + act_count, quotient);
                    return act;
                }
            case "Hard":
                {
                    quotient = Quotient(ref act_count, 4);
                    act = new ActionsCount(quotient, quotient, quotient * 2 + act_count);
                    return act;
                }
        }

        return null;
    }




    private int Quotient(ref int number, int devideBy)
    {
        int quotionet = number / devideBy;
        number %= devideBy;
        return quotionet;
    }
}
