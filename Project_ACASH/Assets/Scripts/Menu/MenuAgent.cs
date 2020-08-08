using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Single;

public class MenuAgent : MonoSingleton<MenuAgent>
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
                    InputManager.MenuResume();
                    break;
                }
            case "Restart":
                {
                    GameManager.Instance.Restart();
                    break;
                }
            case "Settings":
                {
                    if (GameManager.Instance.Game)
                        Disable("Pause");
                    else
                        Disable("Main Menu");

                    Set("Settings");
                    break;
                }
            case "Main Menu":
                {
                    if (GameManager.Instance.Game)
                        Disable("Pause");
                    GameManager.Instance.MainMenu();
                    break;
                }
            case "New Game":
                {
                    GameManager.Instance.NewGame();
                    break;
                }
            case "Continue Story":
                {
                    GameManager.Instance.Continue();
                    break;
                }
            case "Exit":
                {
                    GameManager.Instance.Quit();
                    break;
                }
            case "Back":
                {
                    Escape(Menu.Instance.current_menu);
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
                    SettingsManager.Instance.Reset();
                    break;
                }


            //-----------------------------------------------------------------------------------------------------------------
            //Settings_Accept
            //-----------------------------------------------------------------------------------------------------------------

            case "Language":
                {
                    SettingsManager.Instance.AcceptSettings();
                    break;
                }
            case "Voice Language":
                {
                    SettingsManager.Instance.AcceptSettings();
                    break;
                }
            case "Subtitles":
                {
                    SettingsManager.Instance.AcceptSettings();
                    break;
                }
            case "Sensitivity":
                {
                    SettingsManager.Instance.AcceptSettings();
                    break;
                }
            case "Accept Settings":
                {
                    SettingsManager.Instance.AcceptSettings();
                    break;
                }
            case "Volume General":
                {
                    SettingsManager.Instance.AcceptSettings();
                    break;
                }
            case "Volume Voice":
                {
                    SettingsManager.Instance.AcceptSettings();
                    break;
                }
            case "Volume Music":
                {
                    SettingsManager.Instance.AcceptSettings();
                    break;
                }
            case "Volume SFX":
                {
                    SettingsManager.Instance.AcceptSettings();
                    break;
                }
            case "Resolution":
                {
                    SettingsManager.Instance.AcceptSettings();
                    break;
                }
            case "Screen Mode":
                {
                    SettingsManager.Instance.AcceptSettings();
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
                    InputManager.MenuResume();
                    break;
                }
            case "Settings":
                {
                    Disable("Settings");
                    if (GameManager.Instance.Game)
                        Set("Pause");
                    else
                        Set("Main Menu");
                    break;
                }
            case "Main Menu":
                {
                    GameManager.Instance.Quit();
                    break;
                }
            case "Main":
                {
                    SettingsManager.Instance.BackUp();
                    Disable("Main");
                    Set("Settings");
                    break;
                }
            case "Gameplay":
                {
                    SettingsManager.Instance.BackUp();
                    Disable("Gameplay");
                    Set("Settings");
                    break;
                }
            case "Sound":
                {
                    SettingsManager.Instance.BackUp();
                    Disable("Sound");
                    Set("Settings");
                    break;
                }
            case "Video":
                {
                    SettingsManager.Instance.BackUp();
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
