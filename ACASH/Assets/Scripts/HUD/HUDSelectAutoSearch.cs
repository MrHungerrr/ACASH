using UnityEngine;
using UnityEditor;

public static class HUDSelectAutoSearch
{

    [MenuItem("Tools/Hud/Auto Search")]
    public static void Search()
    {
        HUDSelect[] huds = GameObject.FindObjectsOfType<HUDSelect>();

        foreach (HUDSelect hud in huds)
        {
            hud.AutoFill();
        }
    }

}
