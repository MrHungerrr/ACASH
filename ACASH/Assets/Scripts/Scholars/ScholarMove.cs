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


    [HideInInspector]
    public bool rotating { get; private set; } = false;
    [HideInInspector]
    public bool walking{ get; private set; } = false;




    public void SetupMove(Scholar scholar)
    {
        Scholar = scholar;
        NavAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if(rotating)
            Rotate();
        if (walking)
            Walk();
    }




    //===========================================================================================================================
    //===========================================================================================================================
    //Ходьба

    public void SetDestination(Vector3 goal)
    {
        destination = new Vector3(goal.x, transform.position.y, goal.z);
        NavAgent.SetDestination(destination);
        Scholar.Anim.SetAnimation(GetA.animations.Walking);
        walking = true;
    }


    private void Walk()
    {
        if(ScholarIsHere())
        {
            Scholar.Anim.SetAnimation(GetA.animations.Nothing);
            walking = false;
        }
    }


    public bool ScholarIsHere()
    {
        if ((transform.position - destination).magnitude <= 0.05)
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
    //Поворот

    public void SetRotateGoal(Quaternion target)
    {
        targetRotation = target;
        rotating = true;
    }

    public void SetRotateGoal(Vector3 position)
    {
        targetRotation = BaseGeometry.GetQuaternionTo(transform, position);
        rotating = true;
    }

    public void SetRotateGoal(float angle_plus)
    {
        Debug.Log("Вращаемся на " + angle_plus);
        targetRotation = Quaternion.Euler(Rotation().eulerAngles.x, Rotation().eulerAngles.y + angle_plus, Rotation().eulerAngles.z);
        rotating = true;
    }

    private void Rotate()
    {
        RotateTo(targetRotation);

        if(RotationIsHere())
        {
            rotating = false;
        }
    }

    public void RotateTo(Quaternion target)
    {
        Rotation(Quaternion.Slerp(Rotation(), target, 3f * Time.deltaTime));
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
