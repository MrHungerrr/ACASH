using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public static class ClassManager
{
    private static ClassAgent[] classes;

    public static void SetLevel()
    {
        classes = GameObject.FindObjectsOfType<ClassAgent>();

        foreach(var classAgent in classes)
        {
            classAgent.SetLevel();
        }
    }
}
