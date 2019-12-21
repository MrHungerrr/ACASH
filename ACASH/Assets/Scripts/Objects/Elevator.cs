using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using N_BH;

public class Elevator : Singleton<Elevator>
{

    private bool open = false;
    private bool ninety;
    private bool active = false;
    private bool enter = false;
    private bool inside = false;
    private Transform doorLeft;
    private Transform doorRight;
    private Vector3 doorLeftPos;
    private Vector3 doorRightPos;
    private Vector3 doorLeftTarget;
    private Vector3 doorRightTarget;
    private Vector3 elevator_pos;
    private const float doorAddFloat = 0.135f;
    private const float close_distance = 1f;
    private float close_time;
    private const float close_time_cd = 3f;



    void Start()
    {
        doorLeft = transform.Find("Door_Left");
        doorRight = transform.Find("Door_Right");

        doorLeftPos = doorLeft.position;
        doorRightPos = doorRight.position;

        elevator_pos = transform.position;

        close_time = close_time_cd;
        this.tag = "Elevator";

        if (transform.rotation.eulerAngles.y == 0)
        {
            ninety = false;
        }
        else if (transform.rotation.eulerAngles.y == 90)
        {
            ninety = true;
        }

        active = false;
    }


    void Update()
    {
        if (active)
            DoorMove();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            inside = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            inside = false;
    }


    //--------------------------------------------------------------------------------------------------------------------------------------------
    // На случай, если понадобиться открывать лифт ручками.
    //--------------------------------------------------------------------------------------------------------------------------------------------

    /*
    public void DoorInteract()
    {
        open = !open;

        if (open)
        {
            if (ninety)
            {
                doorLeftTarget = new Vector3(doorLeftPos.x - doorAddFloat, doorLeftPos.y, doorLeftPos.z);
                doorRightTarget = new Vector3(doorRightPos.x + doorAddFloat, doorRightPos.y, doorRightPos.z);
            }
            else
            {
                doorLeftTarget = new Vector3(doorLeftPos.x, doorLeftPos.y, doorLeftPos.z - doorAddFloat);
                doorRightTarget = new Vector3(doorRightPos.x, doorRightPos.y, doorRightPos.z + doorAddFloat);
            }

            ScholarManager.get.SpecialHear(elevator_pos);
        }
        else
        {
            doorLeftTarget = doorLeftPos;
            doorRightTarget = doorRightPos;
        }

        active = true;
    }
    */

    public void Open()     //(bool toEnter) выходишь или заходишь в лифт?
    {
        open = true;


        enter = !inside;
        //enter = toEnter;

        if (ninety)
        {
            doorLeftTarget = new Vector3(doorLeftPos.x - doorAddFloat, doorLeftPos.y, doorLeftPos.z);
            doorRightTarget = new Vector3(doorRightPos.x + doorAddFloat, doorRightPos.y, doorRightPos.z);
        }
        else
        {
            doorLeftTarget = new Vector3(doorLeftPos.x, doorLeftPos.y, doorLeftPos.z - doorAddFloat);
            doorRightTarget = new Vector3(doorRightPos.x, doorRightPos.y, doorRightPos.z + doorAddFloat);
        }

        active = true;
    }


    public void Close()
    {
        open = false;

        doorLeftTarget = doorLeftPos;
        doorRightTarget = doorRightPos;

        active = true;
    }


    private void DoorMove()
    {
        doorLeft.position = Vector3.Slerp(doorLeft.position, doorLeftTarget, 6 * Time.deltaTime);
        doorRight.position = Vector3.Slerp(doorRight.position, doorRightTarget, 6 * Time.deltaTime);



        if ((Mathf.Abs(doorLeft.position.x - doorLeftTarget.x) < 0.0001f && ninety) || (Mathf.Abs(doorLeft.position.z - doorLeftTarget.z) < 0.0001f && !ninety))
        {
            if (open)
            {
                if (enter)
                {
                    if (inside)
                        ElevatorController.get.Close();
                }
                else
                {
                    if (close_time > 0)
                    {
                        close_time -= Time.deltaTime;
                    }
                    else if (Vector3.Distance(elevator_pos, Player.get.transform.position) > close_distance)
                    {
                        close_time = close_time_cd;
                        Close();
                    }
                }
            }
            else
            {
                active = false;
            }
        }
    }

}
