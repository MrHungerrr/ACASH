using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class HUDSelect : MonoBehaviour
{

    [SerializeField]
    private Image[] images;
    [SerializeField]
    private TextMeshProUGUI[] texts;
    private bool active;
    private float targetFade;
    private float currentFade;
    private bool selectNeed;


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
        if (selectNeed)
        {
            currentFade = Mathf.Lerp(currentFade, (targetFade + 0.01f), Time.unscaledDeltaTime * 30f);
        }
        else
        {
            currentFade = Mathf.Lerp(currentFade, (targetFade - 0.01f), Time.unscaledDeltaTime * 30f);
        }

        foreach (Image i in images)
        {
            i.color = new Color(1f, 1f, 1f, currentFade);
        }

        foreach(TextMeshProUGUI t in texts)
        {
            t.color = new Color(1f, 1f, 1f, currentFade);
        }

        if ((targetFade - Mathf.Sign(targetFade - 0.5f) * currentFade) < 0.00001f)
        {

            currentFade = targetFade;
            foreach (Image i in images)
            {
                if(i.gameObject.activeSelf)
                    i.color = new Color(1f, 1f, 1f, currentFade);
            }

            foreach (TextMeshProUGUI t in texts)
            {
                if (t.gameObject.activeSelf)
                    t.color = new Color(1f, 1f, 1f, currentFade);
            }
            active = false;
        }
    }

}

