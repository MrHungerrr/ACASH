#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public static class Initializator
{
    [MenuItem("Tools/Initilizate")]
    public static void Initilizate()
    {
        var Initilizators = new List<IInitialization>();

        GameObject[] objects = Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[];
        IInitialization outInitilizator;
        IInitialization[] bufInitilizators;

        for (int i = objects.Length - 1; i >= 0; i--)
            if (objects[i].TryGetComponent<IInitialization>(out outInitilizator))
            {
                bufInitilizators = objects[i].GetComponents<IInitialization>();
                
                for(int k = 0; k < bufInitilizators.Length; k++)
                    Initilizators.Add(bufInitilizators[k]);
            }

        for(int i = 0; i < Initilizators.Count; i++)
        {
            Debug.Log($"Инициализация {Initilizators[i].GetType()}");

            if (!Initilizators[i].TryInitializate())
                Debug.LogError($"Ошибка в Инициализации {Initilizators[i].GetType()}");
        }
    }
}

#endif