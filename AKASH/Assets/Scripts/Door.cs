using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    private bool open = false;
    public bool locked;
    private bool ninety;
    private bool act = false;
    private Quaternion commonRot;
    private Quaternion targetRot;
    private Transform trans;
    private const float close_distance = 1f;
    private float close_time;
    private const float close_time_cd = 3f;
    private float last_angle;
    private Transform player;
    


    void Start()
    {

        player = GameObject.FindWithTag("Player").transform;
        commonRot = transform.parent.transform.rotation;
        trans = transform.parent.transform;
        close_time = close_time_cd;
        this.tag = "Door";

        if (commonRot.eulerAngles.y == 0)
        {
            ninety = false;
        }
        else if (commonRot.eulerAngles.y == 90)
        {
            ninety = true;
        }
        else
        {
            Debug.Log("Не правильно поставлена дверь!");
        }



    }

    

    void Update()
    {
        if (act)
            DoorRot();
    }

    public void DoorInteract(float angle)
    {
        if (!locked)
        {
            open = !open;

            if (open)
            {
                if (ninety)
                {
                    if (angle > 90 && angle < 270)
                    {
                        targetRot = Quaternion.Euler(commonRot.eulerAngles.x, commonRot.eulerAngles.y + 90, commonRot.eulerAngles.z);
                    }
                    else
                    {
                        targetRot = Quaternion.Euler(commonRot.eulerAngles.x, commonRot.eulerAngles.y - 90, commonRot.eulerAngles.z);
                    }
                }
                else
                {
                    if(angle < 180)
                    {
                        targetRot = Quaternion.Euler(commonRot.eulerAngles.x, commonRot.eulerAngles.y + 90, commonRot.eulerAngles.z);
                    }
                    else
                    {
                        targetRot = Quaternion.Euler(commonRot.eulerAngles.x, commonRot.eulerAngles.y - 90, commonRot.eulerAngles.z);
                    }
                }
            }
            else
            {
                targetRot = commonRot;
            }

            last_angle = angle;
            act = true;
        }
    }



    private void DoorRot()
    {
       trans.rotation = Quaternion.Lerp(trans.rotation, targetRot, 6 * Time.deltaTime);

        if (Mathf.Abs(trans.rotation.eulerAngles.y - targetRot.eulerAngles.y) < 0.1f)
        {
            if(open)
            {
                if(close_time>0)
                {
                    close_time -= Time.deltaTime;
                }
                else if(Vector3.Distance(trans.position, player.position) > close_distance)
                {
                    close_time = close_time_cd;
                    DoorInteract(last_angle);
                }
            }
            else
            {
                act = false;
            }
        }
    }
}
