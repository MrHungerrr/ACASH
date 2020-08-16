using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using Animations;

public class ScholarMove : MonoBehaviour
{
    public Rigidbody RB => _rb;
    public NavMeshAgent NavAgent => _navAgent;
    public bool Walking { get; private set; } = false;
    public bool Rotating { get; private set; } = false;



    private Scholar _scholar;
    private Vector3 _destination;
    private Quaternion _targetRotation;
    private Vector3 _lastPosition;

    private bool _paused = true;


    [SerializeField]
    private NavMeshAgent _navAgent;
    [SerializeField]
    private Rigidbody _rb;


    public void Setup(Scholar scholar)
    {
        _scholar = scholar;
        _paused = false;
    }


    private void FixedUpdate()
    { 
        if (!_paused)
        {
            if (Rotating)
                Rotate();
            if (Walking)
                Walk();
        }
    }




    //===========================================================================================================================
    //===========================================================================================================================
    //Ходьба

    public void SetDestination(Vector3 goal)
    {
        _destination = new Vector3(goal.x, transform.position.y, goal.z);

        if (!ScholarIsHere())
        {
            NavAgent.isStopped = false;
            NavAgent.SetDestination(_destination);

            _lastPosition = transform.position;
            Walking = true;
            RB.isKinematic = false;

            _scholar.Anim.SetAnimation(Get.animations.Walking);
        }
    }

    private void WatchDirection()
    {
        if (_lastPosition != transform.position)
        {
            Quaternion target = BaseGeometry.GetQuaternionTo(_lastPosition, transform.position);
            SetRotateGoal(target);
        }
    }


    private void Walk()
    {
        WatchDirection();

        if (ScholarIsHere())
        {
            _scholar.Anim.SetAnimation(Get.animations.Nothing);
            Walking = false;
            NavAgent.isStopped = true;
            RB.isKinematic = true;
        }
    }


    public bool ScholarIsHere()
    {
        if (transform.position == _destination)
        {
            return true;
        }
        else
        {
            _lastPosition = transform.position;
            return false;
        }
    }





    //===========================================================================================================================
    //===========================================================================================================================
    //Поворот


    public void SetRotateGoal(Quaternion target)
    {
        _targetRotation = target;
        Rotating = true;
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
        RotateTo(_targetRotation);

        if(RotationIsHere())
        {
            Rotating = false;
        }
    }

    public void ResetRotateGoal()
    {
        _targetRotation = Rotation();
        Rotating = false;
    }

    private void RotateTo(Quaternion target)
    {
        Rotation(Quaternion.Slerp(Rotation(), target, 5f * Time.fixedDeltaTime));
    }


    public bool RotationIsHere()
    {
        if (Rotation() == _targetRotation)
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
        if (_paused)
        {
            _paused = false;
            RB.angularVelocity = Vector3.zero;

            if (Walking)
                SetDestination(_destination);
        }
    }

    public void Pause()
    {
        if (Walking)
        {
            NavAgent.isStopped = true;
            _scholar.Anim.SetAnimation(Get.animations.Nothing);
        }

        _paused = true;
    }


    public void Stop()
    {
        if (Walking)
        {
            Walking = false;
            NavAgent.isStopped = true;
            _destination = transform.position;
            _scholar.Anim.SetAnimation(Get.animations.Nothing);
        }

        if (Rotating)
        {
            _targetRotation = Rotation();
            Rotating = false;
        }
    }

}
