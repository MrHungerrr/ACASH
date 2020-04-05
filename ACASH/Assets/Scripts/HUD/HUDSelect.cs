using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using Searching;

public class HUDSelect : MonoBehaviour
{
    [SerializeField]
    private bool instant;
    [SerializeField]
    private Image[] images;
    [SerializeField]
    private TextMeshProUGUI[] texts;
    private bool active;
    private float targetFade;
    private float currentFade;
    private bool selectNeed;

    [ContextMenu("AutoFill")]
    public void AutoFill()
    {
        SIC<Image>.Components(this.gameObject, out images);
        SIC<TextMeshProUGUI>.Components(this.gameObject, out texts);
        Debug.Log(name + " is Filled");
    }





    void Awake()
    {
        Select(false);
    }



    void Update()
    {
        if (active)
            Selecting();
    }


    public void Select(bool option)
    {
        active = true;

        if(option)
            targetFade = 1f;
        else
            targetFade = 0f;
    }


    private void Selecting()
    {
        if (!instant)
        {
            if (selectNeed)
            {
                currentFade = Mathf.Lerp(currentFade, targetFade, Time.unscaledDeltaTime * 30f);
            }
            else
            {
                currentFade = Mathf.Lerp(currentFade, targetFade, Time.unscaledDeltaTime * 30f);
            }
        }
        else
        {
            currentFade = targetFade;
        }

        foreach (Image i in images)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, currentFade);
        }

        foreach(TextMeshProUGUI t in texts)
        {
            t.color = new Color(t.color.r, t.color.g, t.color.b, currentFade);
        }

        if ( Mathf.Abs(currentFade - targetFade) < 0.001f)
        {

            currentFade = targetFade;
            foreach (Image i in images)
            {
                if(i.gameObject.activeSelf)
                    i.color = new Color(i.color.r, i.color.g, i.color.b, currentFade);
            }

            foreach (TextMeshProUGUI t in texts)
            {
                if (t.gameObject.activeSelf)
                    t.color = new Color(t.color.r, t.color.g, t.color.b, currentFade);
            }
            active = false;
        }
    }

}

