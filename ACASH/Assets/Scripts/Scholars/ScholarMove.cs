using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using Animations;

public class ScholarMove : MonoBehaviour
{

    private Scholar Scholar;

    [HideInInspector]
    public NavMeshAgent NavAgent { get; private set; }


    private Vector3 destination;
    private Quaternion targetRotation;
    private Vector3 last_position;

    private Transform head;
    private Transform head_target;


    [HideInInspector]
    public bool rotating { get; private set; } = false;
    [HideInInspector]
    public bool walking{ get; private set; } = false;
    private bool head_on_postion = false;




    public void Setup(Scholar scholar)
    {
        Scholar = scholar;

        head = transform.Find("Head");
        head_target = transform.Find("Scholar").Find("Scholar").Find("Spine").Find("Head Target");

        NavAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (Scholar != null)
        {
            if (rotating)
                Rotate();
            if (walking)
                Walk();

            MoveHead();
        }
    }




    //===========================================================================================================================
    //===========================================================================================================================
    //Ходьба

    public void SetDestination(Vector3 goal)
    {
        destination = new Vector3(goal.x, transform.position.y, goal.z);

        if (!ScholarIsHere())
        {
            NavAgent.SetDestination(destination);
            Scholar.Anim.SetAnimation(GetA.animations.Walking);
            walking = true;
        }
    }

    private void WatchDirection()
    {
        if (last_position != transform.position)
        {
            Quaternion target = BaseGeometry.GetQuaternionTo(last_position, transform.position);
            SetRotateGoal(target);
        }
    }


    private void Walk()
    {
        WatchDirection();

        if (ScholarIsHere())
        {
            Scholar.Anim.SetAnimation(GetA.animations.Nothing);
            walking = false;
        }
    }


    public bool ScholarIsHere()
    {
        if (transform.position == destination)
        {
            return true;
        }
        else
        {
            last_position = transform.position;
            return false;
        }
    }





    //===========================================================================================================================
    //===========================================================================================================================
    //Поворот


    public void SetRotateGoal(Quaternion target)
    {
        targetRotation = target;
        rotating = true;
    }

    public void SetRotateGoal(Vector3 position)
    {
        Quaternion target = BaseGeometry.GetQuaternionTo(transform, position);
        SetRotateGoal(target);
    }

    public void SetRotateGoal(float angle_plus)
    {
        Quaternion target = Quaternion.Euler(Rotation().eulerAngles.x, Rotation().eulerAngles.y + angle_plus, Rotation().eulerAngles.z);
        SetRotateGoal(target);
    }

    private void Rotate()
    {
        RotateTo(targetRotation);

        if(RotationIsHere())
        {
            Rotation(targetRotation);
            rotating = false;
        }
    }

    public void ResetRotateGoal()
    {
        targetRotation = Rotation();
        rotating = false;
    }

    public void RotateTo(Quaternion target)
    {
        Rotation(Quaternion.Slerp(Rotation(), target, 4f * Time.deltaTime));
    }


    public bool RotationIsHere()
    {
        if (Rotation() == targetRotation)
        {
            return true;
        }
        else
        {
            return false;
        }
    }






    //===========================================================================================================================
    //===========================================================================================================================
    //Поворот бошки

    public void MoveHeadTo(Vector3 target)
    {
        PositionHead(Vector3.Slerp(PositionHead(), target, 6f * Time.deltaTime));
    }



    private void MoveHead()
    {
        if (!HeadOnPosition())
        {
            Vector3 target = new Vector3(head_target.position.x, PositionHead().y, head_target.position.z);
            MoveHeadTo(target);
        }
    }


    public bool HeadOnPosition()
    {
        if (head.position.x == head_target.position.x && head.position.z == head_target.position.z)
        {
            return true;
        }
        else
        { 
            return false;
        }
    }



    public Vector3 Position()
    {
        return transform.position;
    }

    public Quaternion Rotation()
    {
        return transform.rotation;
    }

    public void Position(Vector3 set_position)
    {
        transform.position = set_position;
    }

    public void Rotation(Quaternion set_rotation)
    {
        transform.rotation = set_rotation;
    }


    public Vector3 PositionHead()
    {
        return head.position;
    }

    public Quaternion RotationHead()
    {
        return head.rotation;
    }

    public void PositionHead(Vector3 set_position)
    {
        head.position = set_position;
    }

    public void RotationHead(Quaternion set_rotation)
    {
        head.rotation = set_rotation;
    }




    public void Stop()
    {
        if (walking)
        {
            walking = false;
            destination = transform.position;
            NavAgent.SetDestination(destination);
            Scholar.Anim.SetAnimation(GetA.animations.Nothing);
        }

        if (rotating)
        {
            rotating = false;
            SetRotateGoal(Rotation());
        }
    }

}
