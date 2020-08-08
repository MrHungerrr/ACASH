
using System;
using System.Collections.Generic;
using UnityEngine;
using ScholarOptions;

public static class ScholarFaces
{
    public static Dictionary<GetS.faces, Sprite> get = new Dictionary<GetS.faces, Sprite>();

    public static void Setup()
    {
        int emotions_count = Enum.GetNames(typeof(GetS.faces)).Length;
        GetS.faces face;
        string name;
        Sprite sprite;

        for (int i = 0; i < emotions_count; i++)
        {
            face = (GetS.faces)i;
            name = face.ToString();

            sprite = Resources.Load<Sprite>("Sprites/Faces/" + name);

            get.Add(face, sprite);
        }
    }
}
