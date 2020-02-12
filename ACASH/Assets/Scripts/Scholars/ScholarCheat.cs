using UnityEngine;
using System.Collections;

public class ScholarCheat
{

    private Scholar Scholar;
    public bool cheatNeed;
    public int cheat_finish_type;


    public ScholarCheat(Scholar s)
    {
        Scholar = s;
    }

    public void CheatingFinish()
    {

        // Обозначения переменных для завершения списывания
        //  1 - Звук от учителя
        //  2 - Я вижу учителя
        //  3 - Учитель возможно смотрит на меня
        //  4 - Учитель точно смотрит на меня

        switch (cheat_finish_type)
        {
            case 1:
                {
                    if (Scholar.Senses.T_here)
                        Stop();

                    break;
                }
            case 2:
                {
                    if (Scholar.Senses.T_in_sight)
                        Stop();

                    break;
                }
            case 3:
                {
                    if (Scholar.Senses.T_in_sight && Scholar.Senses.T_look_near_at_us)
                        Stop();

                    break;
                }
            case 4:
                {
                    if (Scholar.Senses.T_in_sight && Scholar.Senses.T_look_at_us)
                        Stop();

                    break;
                }
        }
    }

    public void Stop()
    {
        //Остановка читерства
    }

    public bool Probability()
    {
        return false;
    }

}
