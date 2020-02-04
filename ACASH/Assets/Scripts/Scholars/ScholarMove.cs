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


    private void Awake()
    {
        Scholar = transform.GetComponentInChildren<Scholar>();
        NavAgent = GetComponent<NavMeshAgent>();
    }

    public void SetDestination(Vector3 goal)
    {
        destination = new Vector3(goal.x, transform.position.y, goal.z);
        NavAgent.SetDestination(destination);
        Scholar.Anim.SetAnimation("Walking");
        Scholar.walking = true;
    }


    public bool IsHere()
    {
        if ((transform.position - destination).magnitude <= 0.01)
        {
            Scholar.Anim.SetAnimation("Nothing");
            Scholar.walking = false;
            return true;
        }
        else
        {
            //Debug.Log((transform.position - destination).magnitude);
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
