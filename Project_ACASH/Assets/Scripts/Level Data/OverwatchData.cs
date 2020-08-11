using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Single;


public class OverwatchData : MonoSingleton<OverwatchData>
{
    private string _path;
    private Rect _rect;
    private Texture2D _render;

    private int _index;

    public void Setup()
    {
        _path = LevelDataManager.Path + @"\OverwatchHistory";

        var width = OverwatchCameraManager.Instance.renderTextureReference.width;
        var height = OverwatchCameraManager.Instance.renderTextureReference.height;

        _rect = new Rect(0, 0, width, height);
        _render = new Texture2D(width, height, TextureFormat.RGB24, false);

        DirectoryManager.Create(_path);
    }


    public void SetLevel()
    {
        DirectoryManager.Clear(_path);


        for (int i = 0; i < OverwatchCameraManager.Instance.CamerasHolders.Length; i++)
        {
            DirectoryManager.Create($"{_path}/{i}");
        }

        _index = 0;

        TakeAShots();
    }



    public void TakeAShots()
    {
        StartCoroutine(TakeAShot());
    }
   
    public IEnumerator TakeAShot()
    {
        for (int i = 0; i < OverwatchCameraManager.Instance.CamerasHolders.Length; i++)
        {
            RenderTexture.active = OverwatchCameraManager.Instance.CamerasHolders[i].Texture;
            _render.ReadPixels(_rect, 0, 0);
            yield return new WaitForEndOfFrame();

            _render.Apply();
            yield return new WaitForEndOfFrame();

            byte[] byteArray = _render.GetRawTextureData();
            yield return new WaitForEndOfFrame();

            DataReader.SaveData($"{_path}/{i}/{_index}.t2D", byteArray);
            yield return new WaitForEndOfFrame();
        }

        _index++;
        ActionSchedule.Instance.AddActionInTime(5, TakeAShots);

        Debug.Log("ScreenShot");
    }
}
