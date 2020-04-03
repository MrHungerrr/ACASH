using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, I_Interaction
{
    [HideInInspector]
    public bool open { get; private set; } = false;
    public bool locked;
    private bool active = false;

    private Rigidbody RB;
    private Quaternion commonRot;
    private Quaternion targetRot;

    private DoorSounds Sound;


    private Transform doorT;
    private const float close_distance = 1f;
    private float close_time;
    private const float close_time_cd = 3f;
    private bool scholar_open;
    private bool in_range;



    private void Start()
    {
        RB = GetComponent<Rigidbody>();

        Sound = GetComponent<DoorSounds>();
        Sound.Setup(RB.gameObject);

        doorT = transform.parent;
        commonRot = transform.rotation;
        close_time = close_time_cd;
        this.tag = "Door";
        in_range = false;

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
        if (!open)
        {
            open = true;

            if (Player.get.Move.type_movement != PlayerMove.movement.Crouch)
            {
                DoorInteract(Player.get.Move.Position());
            }
            else
            {
                DoorQuietInteract(Player.get.Move.Position());
            }
        }
        else
        {
            Close();
        }

        Player.get.Action.Doing(false);
    }



    private void TargetRotate(float angle)
    {
        targetRot = Quaternion.Euler(commonRot.eulerAngles.x, commonRot.eulerAngles.y + angle, commonRot.eulerAngles.z);
    }



    public void DoorInteract(Vector3 pos)
    {
        if (!locked)
        {

            if (Vector3.Distance(doorT.position + doorT.right, pos) > Vector3.Distance(doorT.position - doorT.right, pos))
            {
                TargetRotate(90);
            }
            else
            {
                TargetRotate(-90);
            }

            ScholarManager.get.SpecialHear(doorT.position);
            Sound.Play(DoorSounds.sounds.Open);
            active = true;
        }

    }


    public void DoorQuietInteract(Vector3 pos)
    {
        if (!locked)
        {

            if (Vector3.Distance(doorT.position + doorT.right, pos) > Vector3.Distance(doorT.position - doorT.right, pos))
            {
                TargetRotate(50);
            }
            else
            {
                TargetRotate(-50);
            }

            active = true;
        }
    }



    public void Close()
    {
        Sound.Play(DoorSounds.sounds.Close);
        targetRot = commonRot;
        open = false;
        active = true;
    }



    private void DoorRot()
    {   
        RB.MoveRotation(Quaternion.Slerp(transform.rotation, targetRot, 6 * Time.deltaTime));


        if ((Mathf.Abs(transform.rotation.eulerAngles.y - targetRot.eulerAngles.y) < 0.1f) && (Mathf.Abs(RB.angularVelocity.y)  < 0.1f))
        {
            if(open)
            {
                if(close_time>0)
                {
                    close_time -= Time.deltaTime;
                }
                else if(Vector3.Distance(doorT.position, Player.get.transform.position) > close_distance)
                {
                    close_time = close_time_cd;
                    Close();
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
                if (Vector3.Distance(doorT.position, ScholarManager.get.scholars[i].transform.position) < 0.4f)
                {
                    if (!open)
                    {
                        if (Mathf.Abs(BaseGeometry.GetQuaternionToY(ScholarManager.get.scholars[i].Move.transform, doorT.position).eulerAngles.y - ScholarManager.get.scholars[i].Move.Rotation().eulerAngles.y) < 45)
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
            Close();
            scholar_open = false;
        }
    }
}
