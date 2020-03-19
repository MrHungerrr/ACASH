using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DesktopController : MonoBehaviour
{

    private Image background;
    private IconUI[] icons;
    private User user;

    public void Setup()
    {
        Transform desktop = transform.Find("Desktop");
        background = desktop.GetComponentInChildren<Image>();

        Transform iconsDest = desktop.Find("Icons");

        icons = new IconUI[iconsDest.GetComponentsInChildren<IconUI>().Length];

        for(int i = 0; i< icons.Length; i++)
        {
            icons[i] = iconsDest.Find("Icon_" + i).GetComponent<IconUI>();
            icons[i].Setup();
        }
    }


    public void SetUser(User new_user)
    {
        user = new_user;
        background.sprite = new_user.background;
        ChangeIcons(user.icons);
    }



    public void ChangeIcons(Icon[] new_icons)
    {
        int i;

        for(i = 0; i < new_icons.Length; i++)
        {
            Debug.Log(i);
            icons[i].Enable(true);
            icons[i].Change(new_icons[i]);
        }

        for(; i < icons.Length; i++)
        {
            icons[i].Enable(false);
        }
    }


}
