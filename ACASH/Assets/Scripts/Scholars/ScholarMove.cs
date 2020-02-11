using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class ScholarMove : MonoBehaviour
{

    [HideInInspector]
    public NavMeshAgent NavAgent;
    private Scholar Scholar;
    private Vector3 destination;
    private Quaternion targetRotation;
    [HideInInspector]
    public bool watching = false;
    [HideInInspector]
    public bool walking = false;

    [HideInInspector]
    public delegate void DoneDelegate();
    [HideInInspector]
    public event DoneDelegate moveDoneEvent;
    [HideInInspector]
    public event DoneDelegate lookDoneEvent;


    public void SetupMove(Scholar scholar)
    {
        Scholar = scholar;
        NavAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if(watching)
            Look();
        if (walking)
            Walk();
    }

    public void SetDestination(Vector3 goal)
    {
        destination = new Vector3(goal.x, transform.position.y, goal.z);
        NavAgent.SetDestination(destination);
        Scholar.Anim.SetAnimation("Walking");
        walking = true;
    }


    private void Walk()
    {
        if(ScholarIsHere())
        {
            Scholar.Anim.SetAnimation("Nothing");
            walking = false;
            moveDoneEvent();
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

    public void SetSightGoal(Quaternion target)
    {
        targetRotation = target;
        watching = true;
    }

    public void SetSightGoal(Vector3 position)
    {
        targetRotation = BaseGeometry.GetQuaternionTo(transform, position);
        watching = true;
    }

    private void Look()
    {
        SightTo(targetRotation);

        if(SightIsHere())
        {
            watching = false;
            lookDoneEvent();
        }
    }

    public void SightTo(Quaternion target)
    {
        Rotation(Quaternion.Slerp(Rotation(), target, 3f * Time.deltaTime));
    }


    public bool SightIsHere()
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
        destination = transform.position;
        NavAgent.SetDestination(destination);
        Scholar.Anim.SetAnimation("Nothing");
        Scholar.walking = false;
    }

}
