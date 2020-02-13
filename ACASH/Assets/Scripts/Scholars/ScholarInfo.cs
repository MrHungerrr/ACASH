using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScholarInfo
{
    public int number { get; private set; }
    public string surname { get; private set; }
    public string name { get; private set; }

    public Vector3 home { get; private set; }
    public Vector3 desk { get; private set; }


    public ScholarInfo()
    {
        surname = "Akimov";
        name = "Egor";
    }


    public void SetNumber(int number)
    {
        this.number = number;
        home = ScholarManager.get.desks[0, number].position;
        desk = ScholarManager.get.desks[1, number].position;
    }
}
