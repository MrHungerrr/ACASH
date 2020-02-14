using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScholarCheck
{
    private Scholar Scholar;
    public bool checking { get; private set; } = false;
    private int index;


    public ScholarCheck(Scholar Scholar)
    {
        this.Scholar = Scholar;
    }


    public void Update()
    {
        if (checking)
            Check();
    }

    public void StartCheck()
    {
        checking = true;
        index = 0;
    }


    private void Check()
    {
        if (!Scholar.Senses.T_here)
        {
            if (!Scholar.Move.rotating)
            {
                Scholar.Move.SetRotateGoal(120);
                if (index > 2)
                {
                    CheckEnd();
                }
                else
                {
                    index++;
                }
            }
        }
        else
        {
            CheckEnd();
        }
    }


    private void CheckEnd()
    {
        if (Scholar.Senses.T_here)
            Scholar.Move.SetRotateGoal(Player.get.transform.position);

        checking = false;
    }

    public void Stop()
    {
        checking = false;
    }
}
