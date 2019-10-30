using System;
using System.Collections.Generic;
using UnityEngine;
using N_BH;


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

    [HideInInspector]
    public string[] settings_name = new string[]
    {
      /*00*/  "Resolution",
      /*01*/  "Volume General",
      /*02*/  "Volume Voice",
      /*03*/  "Volume Music",
      /*04*/  "Volume SFX",
      /*05*/  "Sensitivity",
      /*06*/  "Screen Type",
    };

    public Dictionary<string, int> settings_current = new Dictionary<string, int>()
    {
        {"Resolution", 8},
        {"Volume General", 9},
        {"Volume Voice", 9},
        {"Volume Music", 9},
        {"Volume SFX", 9},
        {"Sensitivity", 9},
        {"Screen Type", 0},
    };

    public Dictionary<string, int> settings_new = new Dictionary<string, int>()
    {
        {"Resolution", 1},
        {"Volume General", 1},
        {"Volume Voice", 1},
        {"Volume Music", 1},
        {"Volume SFX", 1},
        {"Sensitivity", 1},
        {"Screen Type", 0},
    };

    private string[] resolution = new string[]
        {
          /*00*/  "1024×768",
          /*01*/  "1280×720",
          /*02*/  "1280x1024",
          /*03*/  "1366×768",
          /*04*/  "1440×900",
          /*05*/  "1440×1080",
          /*06*/  "1600×900",
          /*07*/  "1600×1200",
          /*08*/  "1920×1080",
          /*09*/  "1920×1200",
          /*10*/  "1920×1440",
          /*11*/  "2560×1440",
          /*12*/  "2560×1600",
          /*13*/  "3840×2160",
          /*14*/  "7680x4320",
        };

    private string[] screen_type = new string[]
    {
          /*00*/  "FullScreen",
          /*01*/  "Windowed",
    };







    private void Awake()
    {
        settings.Add("Resolution", resolution);
        settings.Add("Screen Type", screen_type);

        BackUp();
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
        settings_current = settings_new;
        SettingsBlanks.Set(settings_menu);
    }

    public void BackUp()
    {
        settings_new = settings_current;
    }
}
