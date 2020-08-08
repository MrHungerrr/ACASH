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
        IInitialization initializator;

        for (int i = objects.Length - 1; i >= 0; i--)
            if (objects[i].TryGetComponent<IInitialization>(out initializator))
            {
                Initilizators.Add(initializator);
            }

        foreach (var initializate in Initilizators)
        {
            if (!initializate.TryInitializate())
                Debug.LogError($"Ошибка в Инициализации {initializate.GetType()}");
        }
    }
}

#endif