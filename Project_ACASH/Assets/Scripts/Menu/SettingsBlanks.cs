using UnityEngine;
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
        Language(SettingsManager.Instance.settings_current["Language"]);
        VoiceLanguage(SettingsManager.Instance.settings_current["Voice Language"]);
        Subtitles(SettingsManager.Instance.settings_current["Subtitles"]);
    }

    private static void Language(int option)
    {
        string language = SettingsManager.Instance.settings["Language"][option];
        Debug.Log("Язык - " + language);

        language = LanguageHelper.Get2LetterFromFullWord(language);
        ScriptManager.Instance.SwitchLanguageText(language);
    }

    private static void VoiceLanguage(int option)
    {
        string language = SettingsManager.Instance.settings["Voice Language"][option];
        Debug.Log("Язык - " + language);

        language = LanguageHelper.Get2LetterFromFullWord(language);
        ScriptManager.Instance.SwitchLanguageVoice(language);
    }

    private static void Subtitles(int option)
    {
        if (option == 0)
            HUDManager.Instance.SubtitleDisable(false);
        else
            HUDManager.Instance.SubtitleDisable(true);
    }
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------
    // Gameplay Settings 
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------


    private static void Gameplay()
    {
        Sensitivity(SettingsManager.Instance.settings_current["Sensitivity"]);
    }

    private static void Sensitivity(int option)
    {
        PlayerSettings.Sensetivity(option);
    }


    //----------------------------------------------------------------------------------------------------------------------------------------------------------------
    // Sound Settings 
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------


    private static void Sound()
    {
        VolumeGeneral(SettingsManager.Instance.settings_current["Volume General"]);
        VolumeVoice(SettingsManager.Instance.settings_current["Volume Voice"]);
        VolumeMusic(SettingsManager.Instance.settings_current["Volume Music"]);
        VolumeSFX(SettingsManager.Instance.settings_current["Volume SFX"]);
    }

    private static void VolumeGeneral(int option)
    {
        //Debug.Log("Общая громкость = " + option);
        //SetVolume()
    }

    private static void VolumeVoice(int option)
    {
        //Debug.Log("Громкость голоса = " + option);
        //SetVolume()
    }

    private static void VolumeMusic(int option)
    {
        //Debug.Log("Громкость музыки = " + option);
        //SetVolume()
    }

    private static void VolumeSFX(int option)
    {
        //Debug.Log("Громкость эффектов = " + option);
        //SetVolume()
    }




    //----------------------------------------------------------------------------------------------------------------------------------------------------------------
    // Video Settings 
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------


    private static void Video()
    {
        SetResolution(SettingsManager.Instance.settings_current["Resolution"]);
        ScreenType(SettingsManager.Instance.settings_current["Screen Mode"]);
    }

    private static void SetResolution(int option)
    {

        switch (option)
        {

            case 0:
                {
                    Screen.SetResolution(1024, 576, Screen.fullScreen);
                    break;
                }
            case 1:
                {
                    Screen.SetResolution(1280, 720, Screen.fullScreen);
                    break;
                }
            case 2:
                {
                    Screen.SetResolution(1366, 768, Screen.fullScreen);
                    break;
                }
            case 3:
                {
                    Screen.SetResolution(1600, 900, Screen.fullScreen);
                    break;
                }
            case 4:
                {
                    Screen.SetResolution(1920, 1080, Screen.fullScreen);
                    break;
                }
            case 5:
                {
                    Screen.SetResolution(2560, 1440, Screen.fullScreen);
                    break;
                }
            case 6:
                {
                    Screen.SetResolution(3840, 2160, Screen.fullScreen);
                    break;
                }
            case 7:
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
