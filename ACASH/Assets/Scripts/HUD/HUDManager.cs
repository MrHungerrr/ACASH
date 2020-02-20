using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Single;

public class HUDManager : Singleton<HUDManager>
{
    private bool active;
    private float hud_cd;
    private const float hud_const_cd = 5f;
    

    Dictionary<string, HUDSelect> huds = new Dictionary<string, HUDSelect>();

    private void Awake()
    {
        HUDSelect[] bufs = GameObject.FindObjectsOfType<HUDSelect>();

        foreach(HUDSelect buf in bufs)
        {
            huds.Add(buf.name, buf);
        }

        CloseHUD();
    }

    private void Update()
    {
        if(active)
        {
            CloseTime();
        }
    }

    public void ControlHUD()
    {
        if(!active)
        {
            GameHud();
        }
        else
        {
            CloseHUD();
        }
        Debug.Log("<color=#00ff00> Показываю HUD </color>");
    }


    private void GameHud()
    {
        huds["Timer"].Select(true);
        huds["Reputation"].Select(true);
        //huds["Specials"].Select(true);

        hud_cd = hud_const_cd;
        active = true;
    }

    public void ChangeReputationHUD(int change)
    {
        HUDController.get.Reputation(ScoreManager.get.reputation);
        HUDController.get.ReputationChange(change);

        huds["Reputation"].Select(true);
        huds["Reputation Change"].Select(true);

        hud_cd = hud_const_cd;
        active = true;
    }

    public void ReportHUD(KeyWord information_key)
    {
        HUDController.get.Report(ScriptManager.get.GetLine(information_key));
        huds["Report"].Select(true);

        hud_cd = hud_const_cd;
        active = true;
    }

    public void ExecuteHUD(bool option)
    {
        ExecuteHUDController.get.active = option;
        ExecuteHUDController.get.Select(1);

        if (option)
        {
            CloseHUD();
            Time.timeScale = 0.1f;
            ExecuteHUDController.get.InputType();
        }
        else
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        huds["Execute"].Select(option);
        PostProcessManager.get.Blur(option);
    }



    private void CloseTime()
    {
        if(hud_cd>0)
        {
            hud_cd -= Time.deltaTime;
        }
        else
        {
            CloseHUD();
        }
    }

    public void CloseHUD()
    {
        active = false;

        foreach(KeyValuePair<string, HUDSelect> pair in huds)
        {
            pair.Value.Select(false);
        }
    }
}
