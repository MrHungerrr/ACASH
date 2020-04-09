using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using Animations;

public class ScholarMove : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody RB { get; private set; }
    private Scholar Scholar;

    [HideInInspector]
    public NavMeshAgent NavAgent { get; private set; }



    private Vector3 destination;
    private Quaternion targetRotation;
    private Vector3 last_position;

    private bool paused = true;



    [HideInInspector]
    public bool rotating { get; private set; } = false;
    [HideInInspector]
    public bool walking{ get; private set; } = false;






    public void Setup(Scholar scholar)
    {
        Scholar = scholar;
        RB = GetComponent<Rigidbody>();
        //RB.isKinematic = true;

        NavAgent = GetComponent<NavMeshAgent>();
        paused = false;
    }


    private void FixedUpdate()
    { 
        if (!paused)
        {
            if (rotating)
                Rotate();
            if (walking)
                Walk();
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
            NavAgent.isStopped = false;
            NavAgent.SetDestination(destination);

            last_position = transform.position;
            walking = true;
            RB.isKinematic = false;

            Scholar.Anim.SetAnimation(GetA.animations.Walking);
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
            NavAgent.isStopped = true;
            RB.isKinematic = true;
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
        Quaternion target = BaseGeometry.GetQuaternionToY(transform, position);
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
            rotating = false;
        }
    }

    public void ResetRotateGoal()
    {
        targetRotation = Rotation();
        rotating = false;
    }

    private void RotateTo(Quaternion target)
    {
        Rotation(Quaternion.Slerp(Rotation(), target, 5f * Time.fixedDeltaTime));
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
    // Колижн с плеером

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Pause();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Continue();
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
        //Какого-то хуя РигидБоди просто с катушек слетает и хуево поворачивает студента
        //RB.rotation = set_rotation;
        //RB.MoveRotation(set_rotation);
        transform.rotation = set_rotation;
    }


    public void Rotation(Vector3 set_rotation)
    {
        //Какого-то хуя РигидБоди просто с катушек слетает и хуево поворачивает студента
        //RB.rotation = set_rotation;
        //RB.MoveRotation(set_rotation);

        transform.rotation = BaseGeometry.GetQuaternionToY(transform, set_rotation); 
    }




    public void Continue()
    {
        if (paused)
        {
            paused = false;
            RB.angularVelocity = Vector3.zero;

            if (walking)
                SetDestination(destination);
        }
    }

    public void Pause()
    {
        if (walking)
        {
            NavAgent.isStopped = true;
            Scholar.Anim.SetAnimation(GetA.animations.Nothing);
        }

        paused = true;
    }


    public void Stop()
    {
        if (walking)
        {
            walking = false;
            NavAgent.isStopped = true;
            destination = transform.position;
            Scholar.Anim.SetAnimation(GetA.animations.Nothing);
        }

        if (rotating)
        {
            targetRotation = Rotation();
            rotating = false;
        }
    }

}
