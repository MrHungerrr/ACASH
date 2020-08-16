#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public static class DebugUI
{
    [MenuItem("Tools/Assistants/Show")]
    public static void Show()
    {
        Initializator.Initializate();

        var assistants = new List<IDebugUI>();

        GameObject[] objects = Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[];
        IDebugUI assistantOut;

        for (int i = objects.Length - 1; i >= 0; i--)
            if (objects[i].TryGetComponent<IDebugUI>(out assistantOut))
            {
                assistants.Add(assistantOut);
            }

        foreach (var assistant in assistants)
        {
            assistant.Show();
        }
    }

    [MenuItem("Tools/Assistants/Hide")]
    public static void Hide()
    {
        Initializator.Initializate();

        var assistants = new List<IDebugUI>();

        GameObject[] objects = Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[];
        IDebugUI assistantOut;

        for (int i = objects.Length - 1; i >= 0; i--)
            if (objects[i].TryGetComponent<IDebugUI>(out assistantOut))
            {
                assistants.Add(assistantOut);
            }

        foreach (var assistant in assistants)
        {
            assistant.Hide();
        }
    }
}

#endif