using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public static class ClassManager
{

    public enum places
    {

    }



    private static ClassAgent[] classes;

    public static void SetLevel()
    {
        classes = GameObject.FindObjectsOfType<ClassAgent>();

        foreach(var @class in classes)
        {
            @class.SetLevel();
        }
    }
}
