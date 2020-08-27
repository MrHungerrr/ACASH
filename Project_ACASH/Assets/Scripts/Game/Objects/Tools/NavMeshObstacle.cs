#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine.AI;

public static class NavMeshObstacle
{

    [MenuItem("Tools/Obstacles/On")]
    public static void TurnOnObstacles()
    {
        Obstacles(true);
    }


    [MenuItem("Tools/Obstacles/Off")]
    public static void TurnOffObstacles()
    {
        Obstacles(false);
    }


    private static void Obstacles(bool option)
    {
        List<GameObject> obstacles = new List<GameObject>();

        GameObject[] objects = Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[];
        for (int i = objects.Length - 1; i >= 0; i--)
            if (objects[i].tag == "Obstacle")
            {
                obstacles.Add(objects[i]);
            }

        foreach (GameObject obstacle in obstacles)
        {
            obstacle.SetActive(option);
        }
    }
}
#endif