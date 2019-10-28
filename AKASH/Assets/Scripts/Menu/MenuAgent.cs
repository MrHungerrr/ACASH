using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using N_BH;

public class MenuAgent : Singleton<MenuAgent>
{

    [HideInInspector]
    private Dictionary<string, GameObject> menu_types = new Dictionary<string, GameObject>();

    private void Awake()
    {

        menu_types.Add("Pause", transform.Find("Pause").gameObject);
        menu_types.Add("Settings", transform.Find("Settings").gameObject);

        foreach (KeyValuePair<string, GameObject> valuePair in menu_types)
        {
            valuePair.Value.SetActive(false);
        }
    }

    public void Enter(string type)
    {
        switch(type)
        {
            case "Continue":
                {
                    InputManager.get.MenuResume();
                    break;
                }
            case "Settings":
                {
                    Disable("Pause");
                    Set("Options");
                    break;
                }
            case "Main Menu":
                {
                    Application.Quit();
                    break;
                }
            case "Exit":
                {
                    Application.Quit();
                    break;
                }
            case "Back":
                {
                    Escape(Menu.get.current_menu);
                    break;
                }
            case "Accept Settings":
                {
                    break;
                }

        }
    }

    public void Escape(string type)
    {
        switch (type)
        {
            case "Pause":
                {
                    InputManager.get.MenuResume();
                    break;
                }
            case "Options":
                {
                    Disable("Options");
                    Set("Pause");
                    break;
                }
        }
    }

    public void Set(string type)
    {
        menu_types[type].SetActive(true);
    }

    public void Settings()
    {

    }

    public void Disable(string type)
    {
        menu_types[type].SetActive(false);
    }


}
