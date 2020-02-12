using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

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
    [HideInInspector]
    public bool checking { get; private set; } = false;



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
        Scholar.Anim.SetAnimation("Walking");
        walking = true;
    }


    private void Walk()
    {
        if(ScholarIsHere())
        {
            Scholar.Anim.SetAnimation("Nothing");
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

    public void SetRotateGoal(float angle)
    {
        targetRotation = Quaternion.Euler(Rotation().eulerAngles.x, Rotation().eulerAngles.y + angle, Rotation().eulerAngles.z);
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



    public void CheckForTeacher()
    {
        checking = true;
        StartCoroutine(Check());
    }

    private IEnumerator Check()
    {
        SetRotateGoal(120);

        while (!RotationIsHere() && !Scholar.Senses.T_here)
        {
            yield return new WaitForEndOfFrame();
        }

        if (!Scholar.Senses.T_here)
        {
            SetRotateGoal(120);
        }

        while (!Scholar.Senses.T_here && !RotationIsHere())
        {
            yield return new WaitForEndOfFrame();
        }

        if (!Scholar.Senses.T_here)
        {
            SetRotateGoal(120);
        }

        while (!Scholar.Senses.T_here && !RotationIsHere())
        {
            yield return new WaitForEndOfFrame();
        }

        if (Scholar.Senses.T_here)
            SetRotateGoal(Player.get.transform.position);

        checking = false;
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
        walking = false;
    }

}
