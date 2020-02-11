using System;
using System.Collections.Generic;
using UnityEngine;
using Single;


public class SettingsManager : Singleton<SettingsManager>
{
    [HideInInspector]
    public string settings_menu;
    [HideInInspector]
    public string type_of_setting;
    [HideInInspector]
    public SliderController slider;
    [HideInInspector]
    public SelectorController selector;

    public Dictionary<string, string[]> settings = new Dictionary<string, string[]>();

    private static string[] settings_name = new string[]
    {
      /*00*/  "Resolution",
      /*01*/  "Volume General",
      /*02*/  "Volume Voice",
      /*03*/  "Volume Music",
      /*04*/  "Volume SFX",
      /*05*/  "Sensitivity",
      /*06*/  "Screen Mode",
      /*07*/  "Language",
      /*08*/  "Voice Language",
      /*09*/  "Subtitles",
      /*10*/  "Crouch",
    };

    public Dictionary<string, int> settings_current = new Dictionary<string, int>()
    {
        {settings_name[0], 4},
        {settings_name[1], 10},
        {settings_name[2], 10},
        {settings_name[3], 10},
        {settings_name[4], 10},
        {settings_name[5], 5},
        {settings_name[6], 0},
        {settings_name[7], 0},
        {settings_name[8], 0},
        {settings_name[9], 0},
        {settings_name[10], 0},

    };

    public Dictionary<string, int> settings_new = new Dictionary<string, int>()
    {
        {settings_name[0], 1},
        {settings_name[1], 1},
        {settings_name[2], 1},
        {settings_name[3], 1},
        {settings_name[4], 1},
        {settings_name[5], 1},
        {settings_name[6], 0},
        {settings_name[7], 0},
        {settings_name[8], 0},
        {settings_name[9], 0},
        {settings_name[10], 0},
    };

    public Dictionary<string, int> settings_standart = new Dictionary<string, int>()
    {
        {settings_name[0], 4},
        {settings_name[1], 10},
        {settings_name[2], 10},
        {settings_name[3], 10},
        {settings_name[4], 10},
        {settings_name[5], 5},
        {settings_name[6], 0},
        {settings_name[7], 0},
        {settings_name[8], 0},
        {settings_name[9], 0},
        {settings_name[10], 0},
    };

    private string[] resolution = new string[]
        {
          /*00*/  "1024×576",
          /*01*/  "1280×720",
          /*02*/  "1366×768",
          /*03*/  "1600×900",
          /*04*/  "1920×1080",
          /*05*/  "2560×1440",
          /*06*/  "3840×2160",
          /*07*/  "7680x4320",
        };

    private string[] screen_mode = new string[]
    {
          /*00*/  "FullScreen",
          /*01*/  "Windowed",
    };

    private string[] language = new string[]
    {
          /*00*/  "Rus",
          /*01*/  "Eng",
    };

    private string[] voice_language = new string[]
    {
          /*00*/  "Rus",
          /*01*/  "Eng",
    };

    private string[] subtitles = new string[]
    {
          /*00*/  "Yes",
          /*01*/  "No",
    };

    private string[] crouch = new string[]
{
          /*00*/  "Hold",
          /*01*/  "Press",
};








    private void Awake()
    {
        settings.Add("Resolution", resolution);
        settings.Add("Screen Mode", screen_mode);
        settings.Add("Language", language);
        settings.Add("Voice Language", voice_language);
        settings.Add("Subtitles", subtitles);
        settings.Add("Crouch", crouch);
        Reset();
        //BackUp();
    }


    public void SwtichSettings(bool plus)
    {
        switch (type_of_setting)
        {
            case "slider":
                {
                    slider.SwitchSelect(plus);
                    break;
                }
            case "selector":
                {
                    selector.SwitchSelect(plus);
                    break;
                }
        }
    }



    public void AcceptSettings()
    {
        Debug.Log("Настройки приняты!");
        settings_menu = Menu.get.current_menu;

        foreach(string i in settings_name)
        {
            settings_current[i] = settings_new[i];
        }

        SettingsBlanks.Set(settings_menu);
    }


    public void BackUp()
    {
        foreach (string i in settings_name)
        {
            settings_new[i] = settings_standart[i];
        }
    }

    public void Reset()
    {
        foreach (string i in settings_name)
        {
            settings_current[i] = settings_standart[i];
            settings_new[i] = settings_standart[i];
        }

        SettingsBlanks.Set("Menu");
        SettingsBlanks.Set("Gameplay");
        SettingsBlanks.Set("Sound");
        SettingsBlanks.Set("Video");
    }
}
