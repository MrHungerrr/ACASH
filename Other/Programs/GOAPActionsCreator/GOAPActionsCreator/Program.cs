using GOAP;
using GOAP.Cost;
using System;
using System.Collections.Generic;
using System.IO;
using Application;

class Program
{
    static void Main(string[] args)
    {
        Create();

        GOAPTest.Construct("Cheat");
        GOAPInspector.Check();

        GOAPTest.SaveLoad();

        GOAPTest.Construct("Cheat");
        GOAPInspector.Check();

        Console.ReadKey();
    }


    private static void Create()
    {
        GOAPBlanksFactory.Create();
        GOAPGoalsFactory.Create();
        GOAPActionsFactory.Create();
    }
}

