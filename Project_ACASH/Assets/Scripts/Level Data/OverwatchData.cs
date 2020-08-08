using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Single;

public static class OverwatchData
{

    private static readonly int WIDTH = OverwatchCameraManager.Instance.renderTextureReference.width;
    private static readonly int HEIGHT = OverwatchCameraManager.Instance.renderTextureReference.height;
    private static readonly Rect RECT = new Rect(0, 0, WIDTH, HEIGHT);
    private static readonly Texture2D RENDER = new Texture2D(WIDTH, HEIGHT, TextureFormat.RGB24, false);

    private static readonly string _path = LevelDataManager.Path + @"\OverwatchHistory";
    private static int index;

    public static void Setup()
    {
        DirectoryManager.Create(_path);
    }


    public static void SetLevel()
    {
        DirectoryManager.Clear(_path);


        for (int i = 0; i < OverwatchCameraManager.Instance.CamerasHolders.Length; i++)
        {
            DirectoryManager.Create($"{_path}/{i}");
        }

        index = 0;

        TakeAShots();
    }


    public static void TakeAShots()
    {
        for (int i = 0; i < OverwatchCameraManager.Instance.CamerasHolders.Length; i++)
        {
            RenderTexture.active = OverwatchCameraManager.Instance.CamerasHolders[i].Texture;
            RENDER.ReadPixels(RECT, 0, 0);
            RENDER.Apply();

            byte[] byteArray = RENDER.GetRawTextureData();

            DataReader.SaveData($"{_path}/{i}/{index}.t2D", byteArray);
        }

        index++;
        ActionSchedule.Instance.AddActionInTime(5, TakeAShots);

        Debug.Log("ScreenShot");
    }
}
