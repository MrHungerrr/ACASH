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

        GOAPTest.SaveLoad();
        GOAPTest.ConstructAll("Cheat");
        GOAPTest.ConstructBest("Cheat");
        GOAPTest.ConstructAll("End");
        GOAPTest.ConstructBest("End");


        GOAPInspector.Check();

        Console.ReadKey();
    }


    private static void Create()
    {
        GOAPBlanksFactory.Create();
        GOAPGoalsFactory.Create();
        GOAPConxtextFactory.Create();
        GOAPActionsFactory.Create();
    }
}

