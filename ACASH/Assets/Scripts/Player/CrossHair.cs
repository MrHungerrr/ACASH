using System.Collections;
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
    private Texture2D crossExecute;
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
            case "Exexute":
                {
                    crossHair = crossExecute;
                    break;
                }
            case "Eye":
                {
                    crossHair = crossEye;
                    break;
                }
        }
    }

    public void SelectHair(string type)
    {
        switch (type)
        {
           /* case "Scholar":
                {
                    SwitchHair("Speak");
                    break;
                }
                */
            case "Computer":
                {
                    SwitchHair("Action");
                    break;
                }
            case "Door":
                {
                    SwitchHair("Action");
                    break;
                }
            case "DoorHandle":
                {
                    SwitchHair("Eye");
                    break;
                }
            case "Subject":
                {
                    SwitchHair("Execute");
                    break;
                }
            case "ScholarSubject":
                {
                    SwitchHair("Execute");
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
