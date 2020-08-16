#if UNITY_EDITOR
using System;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEditor.SceneManagement;

public static class Initializator
{
    public enum mode
    {
        Auto,
        Manual
    }



    [MenuItem("Tools/Initilizate")]
    public static void Initializate()
    {
        var Initializators = new List<IInitialization>();

        GameObject[] objects = Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[];
        IInitialization outInitilizator;
        IInitialization[] bufInitilizators;

        for (int i = objects.Length - 1; i >= 0; i--)
            if (objects[i].TryGetComponent<IInitialization>(out outInitilizator))
            {
                bufInitilizators = objects[i].GetComponents<IInitialization>();
                
                for(int k = 0; k < bufInitilizators.Length; k++)
                    Initializators.Add(bufInitilizators[k]);
            }

        for(int i = 0; i < Initializators.Count; i++)
        {
            if (Initializators[i].AutoInitializate)
            {
                Debug.Log($"Инициализация {Initializators[i].GetType()}");
                Initializators[i].Initializate();
            }
        }

        EditorSceneManager.SaveOpenScenes();
    }
}

#endif