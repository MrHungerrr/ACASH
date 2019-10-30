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
        menu_types.Add("Sound", transform.Find("Sound").gameObject);

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
                    Set("Settings");
                    SettingsManager.get.BackUp();
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
                    SettingsManager.get.AcceptSettings();
                    break;
                }
            case "Sound":
                {
                    Disable("Settings");
                    Set("Sound");
                    SettingsManager.get.BackUp();
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
            case "Settings":
                {
                    Disable("Settings");
                    Set("Pause");
                    break;
                }
            case "Sound":
                {
                    Disable("Sound");
                    Set("Settings");
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
