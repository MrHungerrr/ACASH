﻿using UnityEngine;
using System.Collections;
using System;


public static class SettingsBlanks
{



    public static void Set(string type)
    {
        switch(type)
        {
            case "Main":
                {
                    Main();
                    break;
                }
            case "Gameplay":
                {
                    Gameplay();
                    break;
                }
            case "Sound":
                {
                    Sound();
                    break;
                }
            case "Video":
                {
                    Video();
                    break;
                }
        }
    }




    //----------------------------------------------------------------------------------------------------------------------------------------------------------------
    // Main Settings 
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------


    private static void Main()
    {



    }




    //----------------------------------------------------------------------------------------------------------------------------------------------------------------
    // Gameplay Settings 
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------


    private static void Gameplay()
    {
        Sensitivity(SettingsManager.get.settings_current["Sensitivity"]);
    }

    private static void Sensitivity(int option)
    {
        PlayerCamera.get.Sensitivity(option);
    }




    //----------------------------------------------------------------------------------------------------------------------------------------------------------------
    // Sound Settings 
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------


    private static void Sound()
    {
        VolumeGeneral(SettingsManager.get.settings_current["Volume General"]);
        VolumeVoice(SettingsManager.get.settings_current["Volume Voice"]);
        VolumeMusic(SettingsManager.get.settings_current["Volume Music"]);
        VolumeSFX(SettingsManager.get.settings_current["Volume SFX"]);
    }
    private static void VolumeGeneral(int option)
    {
        Debug.Log("Общая громкость = " + option);
        //SetVolume()
    }

    private static void VolumeVoice(int option)
    {
        Debug.Log("Громкость голоса = " + option);
        //SetVolume()
    }

    private static void VolumeMusic(int option)
    {
        Debug.Log("Громкость музыки = " + option);
        //SetVolume()
    }

    private static void VolumeSFX(int option)
    {
        Debug.Log("Громкость эффектов = " + option);
        //SetVolume()
    }




    //----------------------------------------------------------------------------------------------------------------------------------------------------------------
    // Video Settings 
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------


    private static void Video()
    {
        SetResolution(SettingsManager.get.settings_current["Resolution"]);
        ScreenType(SettingsManager.get.settings_current["Screen Type"]);
    }

    private static void SetResolution(int option)
    {
        switch(option)
        {

            case 0:
                {
                    Screen.SetResolution(1024, 768, Screen.fullScreen);
                    break;
                }
            case 1:
                {
                    Screen.SetResolution(1280, 720, Screen.fullScreen);
                    break;
                }
            case 2:
                {
                    Screen.SetResolution(1280, 1024, Screen.fullScreen);
                    break;
                }
            case 3:
                {
                    Screen.SetResolution(1366, 768, Screen.fullScreen);
                    break;
                }
            case 4:
                {
                    Screen.SetResolution(1440, 900, Screen.fullScreen);
                    break;
                }
            case 5:
                {
                    Screen.SetResolution(1440, 1080, Screen.fullScreen);
                    break;
                }
            case 6:
                {
                    Screen.SetResolution(1600, 900, Screen.fullScreen);
                    break;
                }
            case 7:
                {
                    Screen.SetResolution(1600, 1200, Screen.fullScreen);
                    break;
                }
            case 8:
                {
                    Screen.SetResolution(1920, 1080, Screen.fullScreen);
                    break;
                }
            case 9:
                {
                    Screen.SetResolution(1920, 1200, Screen.fullScreen);
                    break;
                }
            case 10:
                {
                    Screen.SetResolution(1920, 1440, Screen.fullScreen);
                    break;
                }
            case 11:
                {
                    Screen.SetResolution(2560, 1440, Screen.fullScreen);
                    break;
                }
            case 12:
                {
                    Screen.SetResolution(2560, 1600, Screen.fullScreen);
                    break;
                }
            case 13:
                {
                    Screen.SetResolution(3840, 2160, Screen.fullScreen);
                    break;
                }
            case 14:
                {
                    Screen.SetResolution(7680, 4320, Screen.fullScreen);
                    break;
                }
        }
    }

    private static void ScreenType(int option)
    {
        if (option == 0)
            Screen.fullScreen = true;
        else
            Screen.fullScreen = false;
    }


}
