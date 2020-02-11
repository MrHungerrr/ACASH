using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Single;

public class MenuAgent : Singleton<MenuAgent>
{

    [HideInInspector]
    private Dictionary<string, GameObject> menu_types = new Dictionary<string, GameObject>();

    private void Awake()
    {
        GameObject[] topics = GameObject.FindGameObjectsWithTag("MenuTopic");

        foreach(GameObject i in topics)
        {
            menu_types.Add(i.name, i);
            i.SetActive(false);
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
            case "Restart":
                {
                    GameManager.get.Restart();
                    break;
                }
            case "Settings":
                {
                    if (GameManager.get.game)
                        Disable("Pause");
                    else
                        Disable("Main Menu");

                    Set("Settings");
                    break;
                }
            case "Main Menu":
                {
                    if (GameManager.get.game)
                        Disable("Pause");
                    GameManager.get.MainMenu();
                    break;
                }
            case "New Game":
                {
                    GameManager.get.NewGame();
                    break;
                }
            case "Continue Story":
                {
                    GameManager.get.Continue();
                    break;
                }
            case "Exit":
                {
                    GameManager.get.Quit();
                    break;
                }
            case "Back":
                {
                    Escape(Menu.get.current_menu);
                    break;
                }
            case "Main":
                {
                    Disable("Settings");
                    Set("Main");
                    break;
                }
            case "Gameplay":
                {
                    Disable("Settings");
                    Set("Gameplay");
                    break;
                }
            case "Sound":
                {
                    Disable("Settings");
                    Set("Sound");
                    break;
                }
            case "Video":
                {
                    Disable("Settings");
                    Set("Video");
                    break;
                }
            case "Reset":
                {
                    SettingsManager.get.Reset();
                    break;
                }


            //-----------------------------------------------------------------------------------------------------------------
            //Settings_Accept
            //-----------------------------------------------------------------------------------------------------------------

            case "Language":
                {
                    SettingsManager.get.AcceptSettings();
                    break;
                }
            case "Voice Language":
                {
                    SettingsManager.get.AcceptSettings();
                    break;
                }
            case "Subtitles":
                {
                    SettingsManager.get.AcceptSettings();
                    break;
                }
            case "Sensitivity":
                {
                    SettingsManager.get.AcceptSettings();
                    break;
                }
            case "Accept Settings":
                {
                    SettingsManager.get.AcceptSettings();
                    break;
                }
            case "Volume General":
                {
                    SettingsManager.get.AcceptSettings();
                    break;
                }
            case "Volume Voice":
                {
                    SettingsManager.get.AcceptSettings();
                    break;
                }
            case "Volume Music":
                {
                    SettingsManager.get.AcceptSettings();
                    break;
                }
            case "Volume SFX":
                {
                    SettingsManager.get.AcceptSettings();
                    break;
                }
            case "Resolution":
                {
                    SettingsManager.get.AcceptSettings();
                    break;
                }
            case "Screen Mode":
                {
                    SettingsManager.get.AcceptSettings();
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
                    if (GameManager.get.game)
                        Set("Pause");
                    else
                        Set("Main Menu");
                    break;
                }
            case "Main Menu":
                {
                    GameManager.get.Quit();
                    break;
                }
            case "Main":
                {
                    SettingsManager.get.BackUp();
                    Disable("Main");
                    Set("Settings");
                    break;
                }
            case "Gameplay":
                {
                    SettingsManager.get.BackUp();
                    Disable("Gameplay");
                    Set("Settings");
                    break;
                }
            case "Sound":
                {
                    SettingsManager.get.BackUp();
                    Disable("Sound");
                    Set("Settings");
                    break;
                }
            case "Video":
                {
                    SettingsManager.get.BackUp();
                    Disable("Video");
                    Set("Settings");
                    break;
                }
        }
    }

    public void Set(string type)
    {
        menu_types[type].SetActive(true);
        Debug.Log(menu_types[type].name);
        Debug.Log(type);
    }


    public void Disable(string type)
    {
        menu_types[type].SetActive(false);
    }


}
