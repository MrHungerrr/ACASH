using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;


public class GamePlay_Script : MonoBehaviour
{
    public GameObject pc;
    [Range(0f,2f)]
    public float zoom = 0.05f;

    public void GamePlay()
    {
        StartCoroutine(TakeScrShot());
    }

    private IEnumerator TakeScrShot()
    {
        yield return new WaitForEndOfFrame();
      

        int width = Screen.width;
        int height = Screen.height;

        
        int zoomwidth = (int)(zoom * width);
        int zoomheight = (int)(zoom * height);


        Texture2D tex = new Texture2D(width + zoomwidth, height + zoomheight, TextureFormat.RGB24, false);    
        tex.ReadPixels(new Rect(0, 0, width, height), (int)(zoom/2 * width), (int)(zoom/2 * height));
        tex.Apply();
        /*
                Debug.Log(width + " " + height);

                var bytes = tex.EncodeToJPG();

                Destroy(tex);

                File.WriteAllBytes("F:/Bogdan/NBH/AKASH/AKASH/Assets/Resources/scr1.jpg", bytes);

                yield return new WaitForSeconds(3f);

                Debug.Log(File.Exists("F:/Bogdan/NBH/AKASH/AKASH/Assets/Resources/scr1.jpg"));

                */
        Sprite sprite = Sprite.Create(tex, new Rect(0, 0, width + zoomwidth, height + zoomheight), Vector2.zero);
        pc.GetComponent<Image>().sprite = sprite;
        
    }
}
