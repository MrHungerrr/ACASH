using UnityEngine;
using UnityEngine.UI;
using Single;
using TMPro;

public class StudentHUDController : Singleton<StudentHUDController>
{
    private HUDSelect hud;
    private bool active;
    private StressCellHUD student;
    private const float time_const = 1f;
    private float timer = time_const;

    private void Awake()
    {
        student = transform.Find("Box").GetComponentInChildren<StressCellHUD>();
        active = false;
        hud = GetComponent<HUDSelect>();
    }


    private void Update()
    {
        if(active)
        {
            if(timer <= 0)
            {
                student.Refresh();
                timer = time_const;
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }
            
    }

    public void Set(Scholar s)
    {
        active = true;
        student.Set(s);
        student.Refresh();
        timer = time_const;
        hud.Select(true);
    }


    public void Clear()
    {
        active = false;
        hud.Select(false);
    }
}
