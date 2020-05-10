using UnityEngine;
using Single;


public static class GetActionsCount
{


    public static ActionsCount Wave(int wave)
    {
        return Wave(wave, LevelSettings.get.actionsCount);
    }

    public static ActionsCount Wave(int wave, int act_count)
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



    private static ActionsCount GetZeroWave(int act_count)
    {
        int quotient;
        ActionsCount act;

        switch (LevelSettings.get.difficultyType)
        {
            case LevelSettings.difficultyes.Easy:
                {
                    act = new ActionsCount(act_count, 0, 0);
                    return act;
                }
            case LevelSettings.difficultyes.Normal:
                {
                    act = new ActionsCount(act_count, 0, 0);
                    return act;
                }
            case LevelSettings.difficultyes.Hard:
                {
                    quotient = Quotient(ref act_count, 2);
                    act = new ActionsCount(quotient, quotient + act_count, 0);
                    return act;
                }
        }

        return null;
    }



    private static ActionsCount GetFirstWave(int act_count)
    {
        int quotient;
        ActionsCount act;

        switch (LevelSettings.get.difficultyType)
        {
            case LevelSettings.difficultyes.Easy:
                {
                    quotient = Quotient(ref act_count, 3);
                    act = new ActionsCount(quotient * 2 + act_count, quotient, 0);
                    return act;
                }
            case LevelSettings.difficultyes.Normal:
                {
                    quotient = Quotient(ref act_count, 2);
                    act = new ActionsCount(quotient, quotient + act_count, 0);
                    return act;
                }
            case LevelSettings.difficultyes.Hard:
                {
                    quotient = Quotient(ref act_count, 3);
                    act = new ActionsCount(quotient, quotient * 2, act_count);
                    return act;
                }
        }

        return null;
    }


    private static ActionsCount GetSecondWave(int act_count)
    {
        int quotient;
        ActionsCount act;

        switch (LevelSettings.get.difficultyType)
        {
            case LevelSettings.difficultyes.Easy:
                {
                    quotient = Quotient(ref act_count, 4);
                    act = new ActionsCount(quotient * 2, quotient + act_count, quotient);
                    return act;
                }
            case LevelSettings.difficultyes.Normal:
                {
                    quotient = Quotient(ref act_count, 3);
                    act = new ActionsCount(quotient, quotient + act_count, quotient);
                    return act;
                }
            case LevelSettings.difficultyes.Hard:
                {
                    quotient = Quotient(ref act_count, 3);
                    act = new ActionsCount(quotient, quotient, quotient + act_count);
                    return act;
                }
        }

        return null;
    }

    private static ActionsCount GetThirdWave(int act_count)
    {
        int quotient;
        ActionsCount act;

        switch (LevelSettings.get.difficultyType)
        {
            case LevelSettings.difficultyes.Easy:
                {
                    quotient = Quotient(ref act_count, 3);
                    act = new ActionsCount(quotient, quotient + act_count, quotient);
                    return act;
                }
            case LevelSettings.difficultyes.Normal:
                {
                    quotient = Quotient(ref act_count, 2);
                    act = new ActionsCount(act_count, quotient, quotient);
                    return act;
                }
            case LevelSettings.difficultyes.Hard:
                {
                    quotient = Quotient(ref act_count, 3);
                    act = new ActionsCount(0, quotient, quotient * 2 + act_count);
                    return act;
                }
        }

        return null;
    }




    private static int Quotient(ref int number, int devideBy)
    {
        int quotionet = number / devideBy;
        number %= devideBy;
        return quotionet;
    }
}
