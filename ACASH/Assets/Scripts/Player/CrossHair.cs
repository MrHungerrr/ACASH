﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using N_BH;

public class CrossHair : Singleton<CrossHair>
{
    private bool active;
    private Texture2D crossHair;
    [SerializeField]
    private Texture2D crossSimple;
    [SerializeField]
    private Texture2D crossAction;
    [SerializeField]
    private Texture2D crossSpeak;
    [SerializeField]
    private Texture2D crossCant;
    [SerializeField]
    private Texture2D crossEye;


    private void Awake()
    {
        SwitchHair("Simple");
    }

    public void Enable(bool option)
    {
        active = option;
    }

    private void OnGUI()
    {
        if(active)
            GUI.DrawTexture(new Rect(Screen.width / 2 - 31, Screen.height / 2 - 31, 64, 64), crossHair);
    }

    private void SwitchHair(string cross)
    {
        switch(cross)
        {
            case "Simple":
                {
                    crossHair = crossSimple;
                    break;
                }
            case "Action":
                {
                    crossHair = crossAction;
                    break;
                }
            case "Speak":
                {
                    crossHair = crossSpeak;
                    break;
                }
            case "Cant":
                {
                    crossHair = crossCant;
                    break;
                }
            case "Eye":
                {
                    crossHair = crossEye;
                    break;
                }
        }
    }

    public void SelectHair()
    {
        switch (Player.get.actTag)
        {
           /* case "Scholar":
                {
                    SwitchHair("Speak");
                    break;
                }
                */
            case "Computer":
                {
                    if (Player.get.actSpecialOption)
                        SwitchHair("Action");
                    else
                        SwitchHair("Simple");
                    break;
                }
            case "Door":
                {
                    if(Player.get.actSpecialOption)
                        SwitchHair("Action");
                    else
                        SwitchHair("Cant");
                    break;
                }
            case "Elevator":
                {
                    if (Player.get.actSpecialOption)
                        SwitchHair("Action");
                    else
                        SwitchHair("Simple");
                    break;
                }
            default:
                {
                    SwitchHair("Simple");
                    break;
                }
        }
    }

}