using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action
{
    public string name { get; }
    public string scholar { get; }

    public Action(string name, string scholar)
    {
        this.name = name;
        this.scholar = scholar;
    }
}