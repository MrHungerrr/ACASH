using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuObject : MonoBehaviour
{
    [SerializeField]
    private GameObject[] objects;
    private MenuSelectable[] topics;
    [SerializeField]
    private bool settings;

    private void Awake()
    {
        topics = new MenuSelectable[objects.Length];
        
        for(int i = 0; i < objects.Length; i++)
        {
            topics[i] = objects[i].transform.GetComponentInChildren<MenuSelectable>();
        }
    }

    private void OnEnable()
    {
        InfoToMenu();
    }

    private void InfoToMenu()
    {
        Menu.get.Set(topics, gameObject.name, settings);
    }
}
