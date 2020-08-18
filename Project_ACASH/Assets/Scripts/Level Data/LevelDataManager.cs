using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Overwatch;
using System.IO;
using Single;

public static class LevelDataManager
{
    public static string Path => PATH;
    private static readonly string PATH = Application.dataPath + @"\LevelData";


    public static void Setup()
    {
        DirectoryManager.Create(Path);
        OverwatchDataSaver.Setup();
    }



    public static void SetLevel()
    {
        OverwatchDataSaver.SetLevel();
    }

}
