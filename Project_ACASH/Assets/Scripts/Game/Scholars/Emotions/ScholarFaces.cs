using System;
using System.Collections.Generic;
using UnityEngine;

public static class ScholarFaces
{
    public enum Types
    {
        Ask,
        Dead,
        Happy,
        Sad,
        Smile,
        Suprised,
        Upset,
        Ussual
    }

    public static IReadOnlyDictionary<Types, Sprite> Get => _get;

    private static Dictionary<Types, Sprite> _get = new Dictionary<Types, Sprite>();

    public static void Setup()
    {
        int emotions_count = Enum.GetNames(typeof(Types)).Length;
        Types face;
        string name;
        Sprite sprite;

        for (int i = 0; i < emotions_count; i++)
        {
            face = (Types)i;
            name = face.ToString();

            sprite = Resources.Load<Sprite>("Sprites/Faces/" + name);

            _get.Add(face, sprite);
        }
    }
}
