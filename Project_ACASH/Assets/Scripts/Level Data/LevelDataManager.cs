using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Single;

public static class LevelDataManager
{
    public static string Path => _path;
    private static readonly string _path = Application.dataPath + @"\LevelData";


    public static void Setup()
    {
        DirectoryManager.Create(_path);
        OverwatchData.Setup();
    }





    public static void SetLevel()
    {
        OverwatchData.SetLevel();
    }

}
