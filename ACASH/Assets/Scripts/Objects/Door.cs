﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, I_Interaction
{

    private bool open = false;
    public bool locked;
    private bool ninety;
    private bool active = false;

    private Rigidbody RB;
    private Quaternion commonRot;
    private Quaternion targetRot;


    private Vector3 door_position;
    private const float close_distance = 1f;
    private float close_time;
    private const float close_time_cd = 3f;
    private Vector3 last_pos;
    private bool scholar_open;
    private bool in_range;
    


    void Start()
    {
        RB = transform.parent.GetComponent<Rigidbody>();

        door_position = transform.parent.parent.position;
        commonRot = transform.parent.rotation;
        close_time = close_time_cd;
        this.tag = "Door";
        in_range = false;

        if (transform.parent.parent.rotation.eulerAngles.y == 0)
        {
            ninety = false;
        }
        else if (transform.parent.parent.rotation.eulerAngles.y == 90)
        {
            ninety = true;
        }
        else
        {
            Debug.Log("Не правильно поставлена дверь!");
        }

        active = false;
    }

    

    void FixedUpdate()
    {
        if (active)
            DoorRot();

        ScholarOpen();
    }



    public void Interaction()
    {
        if(Player.get.Move.type_movement != PlayerMove.movement.Crouch)
        {
            DoorInteract(Player.get.Move.Position());
        }
        else
        {
            DoorQuietInteract(Player.get.Move.Position());
        }

        Player.get.Action.doing = false;
    }


    public void DoorInteract(Vector3 pos)
    {
        if (!locked)
        {
            open = !open;

            if (open)
            {
                if (ninety)
                {
                    if (transform.position.z > pos.z)
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
                    if (transform.position.x > pos.x)
                    {
                        targetRot = Quaternion.Euler(commonRot.eulerAngles.x, commonRot.eulerAngles.y + 90, commonRot.eulerAngles.z);
                    }
                    else
                    {
                        targetRot = Quaternion.Euler(commonRot.eulerAngles.x, commonRot.eulerAngles.y - 90, commonRot.eulerAngles.z);
                    }
                }

                ScholarManager.get.SpecialHear(door_position);
            }
            else
            {
                targetRot = commonRot;
            }

            last_pos = pos;
            active = true;
        }
    }

    public void DoorQuietInteract(Vector3 pos)
    {
        if (!locked)
        {
            open = !open;

            if (open)
            {
                if (ninety)
                {
                    if (transform.position.z > pos.z)
                    {
                        targetRot = Quaternion.Euler(commonRot.eulerAngles.x, commonRot.eulerAngles.y + 50, commonRot.eulerAngles.z);
                    }
                    else
                    {
                        targetRot = Quaternion.Euler(commonRot.eulerAngles.x, commonRot.eulerAngles.y - 50, commonRot.eulerAngles.z);
                    }
                }
                else
                {
                    if (transform.position.x > pos.x)
                    {
                        targetRot = Quaternion.Euler(commonRot.eulerAngles.x, commonRot.eulerAngles.y + 50, commonRot.eulerAngles.z);
                    }
                    else
                    {
                        targetRot = Quaternion.Euler(commonRot.eulerAngles.x, commonRot.eulerAngles.y - 50, commonRot.eulerAngles.z);
                    }
                }
            }
            else
            {
                targetRot = commonRot;
            }

            last_pos = pos;
            active = true;
        }
    }



    private void DoorRot()
    {   
        RB.MoveRotation(Quaternion.Lerp(transform.rotation, targetRot, 6 * Time.deltaTime));


        if ((Mathf.Abs(transform.rotation.eulerAngles.y - targetRot.eulerAngles.y) < 0.1f) && (Mathf.Abs(RB.angularVelocity.y)  < 0.1f))
        {
            if(open)
            {
                if(close_time>0)
                {
                    close_time -= Time.deltaTime;
                }
                else if(Vector3.Distance(door_position, Player.get.transform.position) > close_distance)
                {
                    close_time = close_time_cd;
                    DoorInteract(last_pos);
                }
            }
            else
            {
                active = false;
            }
        }
    }



    private void ScholarOpen()
    {
        int i;
        in_range = false;

        for(i = 0; i < ScholarManager.get.scholars.Length; i++)
        {
            try
            { 
                if (Vector3.Distance(door_position, ScholarManager.get.scholars[i].transform.position) < 0.4f)
                {
                    if (!open)
                    {
                        if (Mathf.Abs(BaseGeometry.GetQuaternionTo(ScholarManager.get.scholars[i].Move.transform, door_position).eulerAngles.y - ScholarManager.get.scholars[i].Move.Rotation().eulerAngles.y) < 45)
                        {
                            in_range = true;
                            break;
                        }
                    }
                    else
                    {
                        in_range = true;
                        break;
                    }
                }
            }
            catch
            {
                in_range = false;
            }
        }

        if (in_range && !open && !scholar_open)
        {
            DoorInteract(ScholarManager.get.scholars[i].Move.Position());
            scholar_open = true;
        }
        else if (!in_range && open && scholar_open)
        {
            DoorInteract(last_pos);
            scholar_open = false;
        }
    }
}
