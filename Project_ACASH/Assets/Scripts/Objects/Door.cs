using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteraction
{

    private bool active = false;

    [HideInInspector]
    public bool open { get; private set; } = false;
    public bool locked;
    private bool changing = false;

    private Rigidbody RB;
    private Quaternion commonRot;
    private Quaternion targetRot;

    private DoorSounds _sound;


    private Transform doorT;
    private const float close_distance = 1f;
    private float close_time;
    private const float close_time_cd = 3f;

    private bool scholar_open;
    private bool in_range;



    private void Start()
    {
        RB = GetComponent<Rigidbody>();

        _sound = new DoorSounds(RB.gameObject);

        doorT = transform.parent;
        commonRot = transform.rotation;
        close_time = close_time_cd;
        this.tag = "Door";
        in_range = false;

        changing = false;
    }


    public void Enable(bool option)
    {
        active = option;
    }

    void FixedUpdate()
    {
        if (active)
        {
            if (changing)
                DoorRot();

            ScholarOpen();
        }
    }



    public void Interact()
    {
        if (!open)
        {
            open = true;
            DoorInteract(Player.Instance.Move.Position());
        }
        else
        {
            Close();
        }
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

            _sound.Play(DoorSounds.sounds.Open);
            changing = true;
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

            changing = true;
        }
    }



    public void Close()
    {
        _sound.Play(DoorSounds.sounds.Close);
        targetRot = commonRot;
        open = false;
        changing = true;
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
                else if(Vector3.Distance(doorT.position, Player.Instance.transform.position) > close_distance)
                {
                    close_time = close_time_cd;
                    Close();
                }
            }
            else
            {
                changing = false;
            }
        }
    }



    private void ScholarOpen()
    {
        int i;
        in_range = false;

        for(i = 0; i < ScholarManager.Instance.Scholars.Length; i++)
        {
            try
            { 
                if (Vector3.Distance(doorT.position, ScholarManager.Instance.Scholars[i].transform.position) < 0.3f)
                {
                    if (!open)
                    {
                        if (Mathf.Abs(BaseGeometry.LookingAngle2D(ScholarManager.Instance.Scholars[i].Move.transform, doorT.position)) < 60)
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
            DoorInteract(ScholarManager.Instance.Scholars[i].Move.Position());

            scholar_open = true;
        }
        else if (!in_range && open && scholar_open)
        {
            Close();

            scholar_open = false;
        }

    }
}
