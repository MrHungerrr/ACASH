using System.Collections.Generic;
using UnityEngine;
using Single;
using PlayerOptions;

public class HUDManager : Singleton<HUDManager>
{
    private bool active;
    private float hud_cd;
    private const float hud_const_cd = 5f;
    

    private Dictionary<string, HUDSelect> huds = new Dictionary<string, HUDSelect>();
    private HUDSelect hint;
    private HUDSelect subtitle;
    private HUDSelect introdaction;
    private bool subtitle_active;

    private void Awake()
    {
        HUDSelect[] bufs = GameObject.FindObjectsOfType<HUDSelect>();

        foreach (HUDSelect buf in bufs)
        {
            switch(buf.name)
            {
                case "Hint":
                    {
                        hint = buf;
                        break;
                    }
                case "Subtitles":
                    {
                        subtitle = buf;
                        break;
                    }
                case "Introdaction":
                    {
                        introdaction = buf;
                        break;
                    }
                case "Student":
                    {
                        break;
                    }
                default:
                    {
                        huds.Add(buf.name, buf);
                        break;
                    }

            }
        }

        CloseHUD();
        CloseHintHUD();
        SubtitleHUD(false);
        SubtitleDisable(false);
        CloseIntrodactionHUD();
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

        ScoreManager.get.InterimScore();
        HUDController.get.Reputation(ScoreManager.get.reputation);

        huds["Reputation"].Select(true);
        //huds["Specials"].Select(true);

        hud_cd = hud_const_cd;
        active = true;
    }

    public void ChangeReputationHUD(int change)
    {
        ScoreManager.get.InterimScore();
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
        //huds["Timer"].Select(true);

        hud_cd = hud_const_cd;
        active = true;
    }

    public void HintHUD(GetP.actions action)
    {
        HintHUDController.get.SetHint(action);
        hint.Select(true);
    }

    public void CloseHintHUD()
    {
        hint.Select(false);
    }

    public void IntrodactionHUD(KeyWord introdaction_key)
    {
        introdaction_key += 0;
        string text = ScriptManager.get.GetLine(introdaction_key) + "\n";
        introdaction_key += 1;
        text += ScriptManager.get.GetLine(introdaction_key);

        HUDController.get.Introdaction(text);
        introdaction.Select(true);
    }

    public void CloseIntrodactionHUD()
    {
        introdaction.Select(false);
    }


    public void SubtitleHUD(bool enable)
    {
        if(subtitle_active)
            subtitle.Select(enable);
    }

    public void SubtitleDisable(bool option)
    {
        subtitle_active = !option;

        if(option)
            subtitle.Select(false);
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

    public void ExamHUD(KeyWord exam_key)
    {
        HUDController.get.Exam(ScriptManager.get.GetLine(exam_key));
        huds["Exam"].Select(true);

        hud_cd = hud_const_cd;
        active = true;
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
