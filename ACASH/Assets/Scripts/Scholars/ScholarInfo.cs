using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScholarInfo
{
    private Scholar Scholar { get; }
    public int number { get; private set; }
    public string surname { get; private set; }
    public string name { get; private set; }


    public ScholarInfo(Scholar Scholar)
    {
        this.Scholar = Scholar; 
        surname = "Akimov";
        name = "Egor";
    }


    public void SetNumber(int number)
    {
        this.number = number;
        Scholar.TextBox.Number(number);
        Scholar.Desk = DeskManager.get.desks[number];
    }
}
