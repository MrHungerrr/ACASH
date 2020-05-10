using UnityEngine;
using ScholarOptions;

public class TutorialScholarController
{


    public const string calculate = "Cheating_Calculate_1";
    public const string note = "Cheating_Note_1";
    public const string answer = "Answer";


    public void RandomScholarsCheatSet(Scholar[] scholars, string cheat = "random")
    {
        SetScholarsCheat(scholars, Random.Range(0, 4), cheat);
    }


    public void SetScholarsCheat(Scholar[] scholars, int number, string cheat = "random")
    {
        foreach (Scholar s in scholars)
        {
            s.ResetType();
            s.Action.Reset("Login");
        }



        for(int i = 0; i < scholars.Length; i++)
        {

            if (number > 3)
            {
                number = 0;
            }

            switch (number)
            {
                case 0:
                    {
                        SetScholarCheat_0(ref scholars[i], CheatName(cheat));
                        break;
                    }
                case 1:
                    {
                        SetScholarCheat_1(ref scholars[i], CheatName(cheat));
                        break;
                    }
                case 2:
                    {
                        SetScholarCheat_2(ref scholars[i], CheatName(cheat));
                        break;
                    }
                case 3:
                    {
                        SetScholarCheat_3(ref scholars[i], CheatName(cheat));
                        break;
                    }
            }

            number++;
        }
    }


    public string CheatName(string cheat)
    {
        if (cheat == "random")
        {
            int buf = Random.Range(0, 2);

            switch (buf)
            {
                case 0:
                    {
                        cheat = calculate;
                        break;
                    }
                case 1:
                    {
                        cheat = note;
                        break;
                    }
            }
        }

        return cheat;
    }

    private void SetScholarCheat_0(ref Scholar scholar, string cheat)
    {
        scholar.Action.AddAction("Answer");
        scholar.Action.AddAction("Answer");
        scholar.Action.AddAction(cheat);
        scholar.Action.AddAction("Answer");
        scholar.Action.AddAction(cheat);
        scholar.Action.AddAction("Answer");
        scholar.Action.AddAction(cheat);
        scholar.Action.AddAction("Answer");
        scholar.Action.AddAction(cheat);
        scholar.Action.AddAction("Answer");
        scholar.Action.AddAction(cheat);
        scholar.Action.AddAction("Answer");
        scholar.Action.AddAction(cheat);
        scholar.Action.AddAction("Answer");
        scholar.Action.AddAction(cheat);
        scholar.Action.AddAction("Answer");
        scholar.Action.AddAction("Answer");
        scholar.Action.AddAction(cheat);
        scholar.Action.AddAction("Answer");
        scholar.Action.AddAction(cheat);
        scholar.Action.AddAction("Answer");
        scholar.Action.AddAction(cheat);
        scholar.Action.AddAction("Answer");
        scholar.Action.AddAction(cheat);
        scholar.Action.AddAction("Answer");
        scholar.Action.AddAction(cheat);
        scholar.Action.AddAction("Answer");
        scholar.Action.AddAction(cheat);
        scholar.Action.AddAction("Answer");
        scholar.Action.AddAction(cheat);
    }

    private void SetScholarCheat_1(ref Scholar scholar, string cheat)
    {
        scholar.Action.AddAction(cheat);
        scholar.Action.AddAction("Write");
        scholar.Action.AddAction(cheat);
        scholar.Action.AddAction("Watch_Rules");
        scholar.Action.AddAction(cheat);
        scholar.Action.AddAction("Write");
        scholar.Action.AddAction(cheat);
        scholar.Action.AddAction("Watch_Rules");
        scholar.Action.AddAction(cheat);
        scholar.Action.AddAction("Write");
        scholar.Action.AddAction(cheat);
        scholar.Action.AddAction("Watch_Rules");
        scholar.Action.AddAction(cheat);
        scholar.Action.AddAction("Write");
        scholar.Action.AddAction(cheat);
        scholar.Action.AddAction("Watch_Rules");
        scholar.Action.AddAction(cheat);
        scholar.Action.AddAction("Write");
        scholar.Action.AddAction(cheat);
        scholar.Action.AddAction("Watch_Rules");
        scholar.Action.AddAction(cheat);
        scholar.Action.AddAction("Write");
        scholar.Action.AddAction(cheat);
        scholar.Action.AddAction("Watch_Rules");
        scholar.Action.AddAction(cheat);
        scholar.Action.AddAction("Write");
        scholar.Action.AddAction(cheat);
        scholar.Action.AddAction("Watch_Rules");
        scholar.Action.AddAction(cheat);
        scholar.Action.AddAction("Write");
        scholar.Action.AddAction(cheat);
        scholar.Action.AddAction("Watch_Rules");
    }

    private void SetScholarCheat_2(ref Scholar scholar, string cheat)
    {
        scholar.Action.AddAction("Write");
        scholar.Action.AddAction(cheat);
        scholar.Action.AddAction("Write");
        scholar.Action.AddAction(cheat);
        scholar.Action.AddAction("Write");
        scholar.Action.AddAction(cheat);
        scholar.Action.AddAction("Write");
        scholar.Action.AddAction(cheat);
        scholar.Action.AddAction("Write");
        scholar.Action.AddAction(cheat);
        scholar.Action.AddAction("Write");
        scholar.Action.AddAction(cheat);
        scholar.Action.AddAction("Write");
        scholar.Action.AddAction(cheat);
        scholar.Action.AddAction("Write");
        scholar.Action.AddAction(cheat);
        scholar.Action.AddAction("Write");
        scholar.Action.AddAction(cheat);
        scholar.Action.AddAction("Write");
        scholar.Action.AddAction(cheat);
        scholar.Action.AddAction("Write");
        scholar.Action.AddAction(cheat);
        scholar.Action.AddAction("Write");
        scholar.Action.AddAction(cheat);
        scholar.Action.AddAction("Write");
        scholar.Action.AddAction(cheat);
        scholar.Action.AddAction("Write");
        scholar.Action.AddAction(cheat);
        scholar.Action.AddAction("Write");
        scholar.Action.AddAction(cheat);
        scholar.Action.AddAction("Write");
        scholar.Action.AddAction(cheat);
    }

    private void SetScholarCheat_3(ref Scholar scholar, string cheat)
    {
        scholar.Action.AddAction("Write");
        scholar.Action.AddAction("Write");
        scholar.Action.AddAction(cheat);
        scholar.Action.AddAction("Watch_Rules");
        scholar.Action.AddAction("Write");
        scholar.Action.AddAction("Write");
        scholar.Action.AddAction(cheat);
        scholar.Action.AddAction("Watch_Rules");
        scholar.Action.AddAction("Write");
        scholar.Action.AddAction("Write");
        scholar.Action.AddAction(cheat);
        scholar.Action.AddAction("Watch_Rules");
        scholar.Action.AddAction("Write");
        scholar.Action.AddAction("Write");
        scholar.Action.AddAction(cheat);
        scholar.Action.AddAction("Write");
        scholar.Action.AddAction("Write");
        scholar.Action.AddAction("Write");
        scholar.Action.AddAction(cheat);
        scholar.Action.AddAction("Watch_Rules");
        scholar.Action.AddAction("Write");
        scholar.Action.AddAction("Write");
        scholar.Action.AddAction(cheat);
        scholar.Action.AddAction("Watch_Rules");
        scholar.Action.AddAction("Write");
        scholar.Action.AddAction("Write");
        scholar.Action.AddAction(cheat);
        scholar.Action.AddAction("Watch_Rules");
        scholar.Action.AddAction("Write");
        scholar.Action.AddAction("Write");
        scholar.Action.AddAction(cheat);
        scholar.Action.AddAction("Write");
    }




    public bool StressCheck(Scholar[] scholars)
    {
        if (scholars != null)
        {
            foreach (Scholar s in scholars)
            {
                if (s.Stress.GetMoodType() != GetS.mood.Normal)
                {
                    return false;
                }
            }

            return true;
        }
        return false;
    }

}
