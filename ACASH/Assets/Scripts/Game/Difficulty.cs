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
    public string difficulty { get; private set; }

    [SerializeField]
    [Range(0,20)]
    private int actionsCount;


    public void Setup()
    {
        difficulty = difficultyType.ToString();
    }


    public ActionsCount GetWave(int wave)
    {
        return GetWave(wave, actionsCount);
    }

    public ActionsCount GetWave(int wave, int act_count)
    {
        switch(wave)
        {
            case 0:
                return GetZeroWave(act_count);
            case 1:
                return GetFirstWave(act_count);
            case 2:
                return GetSecondWave(act_count);
            case 3:
                return GetThirdWave(act_count);
        }

        Debug.LogError("Подана не та волна");
        return null;
    }



    private ActionsCount GetZeroWave(int act_count)
    {
        int quotient;
        ActionsCount act;

        switch (difficulty)
        {
            case "Easy":
                {
                    act = new ActionsCount(act_count, 0, 0);
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



    private ActionsCount GetFirstWave(int act_count)
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


    private ActionsCount GetSecondWave(int act_count)
    {
        int quotient;
        ActionsCount act;

        switch (difficulty)
        {
            case "Easy":
                {
                    quotient = Quotient(ref act_count, 4);
                    act = new ActionsCount(quotient * 2, quotient + act_count, quotient);
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
                    quotient = Quotient(ref act_count, 3);
                    act = new ActionsCount(quotient, quotient, quotient + act_count);
                    return act;
                }
        }

        return null;
    }

    private ActionsCount GetThirdWave(int act_count)
    {
        int quotient;
        ActionsCount act;

        switch (difficulty)
        {
            case "Easy":
                {
                    quotient = Quotient(ref act_count, 3);
                    act = new ActionsCount(quotient, quotient + act_count, quotient);
                    return act;
                }
            case "Normal":
                {
                    quotient = Quotient(ref act_count, 2);
                    act = new ActionsCount(act_count, quotient, quotient);
                    return act;
                }
            case "Hard":
                {
                    quotient = Quotient(ref act_count, 3);
                    act = new ActionsCount(0, quotient, quotient * 2 + act_count);
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
