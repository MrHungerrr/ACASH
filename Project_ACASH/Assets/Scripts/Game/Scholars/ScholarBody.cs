using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using Animations;

public class ScholarBody : MonoBehaviour
{
    public Transform Head { get; private set; }
    public Transform Body { get; private set; }


    private bool active = false;

    private Transform headTarget;
    private Transform headHeight;
    private float headHeightTarget;
    private bool headHeightMoving;

    private Vector3 bodyTarget;
    private bool bodyMoving;




    public void Setup(Scholar scholar)
    {
        active = true;
        Head = transform.parent.Find("Head");
        headHeight = Head.Find("Model");
        Body = transform.parent.Find("Scholar");
        headTarget = Body.Find("Scholar").Find("Spine").Find("Head Target");

        bodyMoving = false;

        PositionBody(Vector3.zero);
        PositionHeadHeight(0.04f);
    }

    private void Update()
    {
        if (active)
        {
            if (bodyMoving)
                MoveBody();

            if (headHeightMoving)
                MoveHeadHeight();

            MoveHead();
        }
    }



    public void Enable()
    {
        SetBodyTarget(0.1f);
        SetHeadHeightTarget(0.07f);
    }

    public void Disable()
    {
        SetBodyTarget(0f);
        SetHeadHeightTarget(0.04f);
    }





    //===========================================================================================================================
    //===========================================================================================================================
    //Движение тела




    public void SetBodyTarget(float height)
    {
        bodyTarget = new Vector3(0, height, 0);
        bodyMoving = true;
    }



    private void MoveBody()
    {
        MoveBodyTo(bodyTarget);

        if (BodyOnPosition())
        {
            bodyMoving = false;
        }
    }

    private void MoveBodyTo(Vector3 target)
    {
        PositionBody(Vector3.Slerp(PositionBody(), target, 6f * Time.deltaTime));
    }


    public bool BodyOnPosition()
    {
        if (PositionBody() == bodyTarget)
        {
            return true;
        }
        else
        {
            return false;
        }
    }




    public Vector3 PositionBody()
    {
        return Body.localPosition;
    }


    private void PositionBody(Vector3 set_position)
    {
        Body.localPosition = set_position;
    }



    //===========================================================================================================================
    //===========================================================================================================================
    //Поворот бошки

    private void MoveHeadTo(Vector3 target)
    {
        PositionHead(Vector3.Slerp(PositionHead(), target, 6f * Time.deltaTime));
    }



    private void MoveHead()
    {
        if (!HeadOnPosition())
        {
            Vector3 target = new Vector3(headTarget.position.x, headTarget.position.y, headTarget.position.z);
            MoveHeadTo(target);
        }
    }


    public bool HeadOnPosition()
    {
        if (Head.position == headTarget.position)
        {
            return true;
        }
        else
        { 
            return false;
        }
    }



    public Vector3 PositionHead()
    {
        return Head.position;
    }

    public Quaternion RotationHead()
    {
        return Head.rotation;
    }

    private void PositionHead(Vector3 set_position)
    {
        Head.position = set_position;
    }

    private void RotationHead(Quaternion set_rotation)
    {
        Head.rotation = set_rotation;
    }





    //===========================================================================================================================
    //===========================================================================================================================
    //Движение бошки




    public void SetHeadHeightTarget(float height)
    {
        headHeightTarget = height;
        headHeightMoving = true;
    }



    private void MoveHeadHeight()
    {
        MoveHeadHeightTo(headHeightTarget);

        if (HeadHeightOnPosition())
        {
            headHeightMoving = false;
        }
    }

    private void MoveHeadHeightTo(float target)
    {
        PositionHeadHeight(Mathf.Lerp(PositionHeadHeight(), target, 6f * Time.deltaTime));
    }


    public bool HeadHeightOnPosition()
    {
        if (PositionHeadHeight() == headHeightTarget)
        {
            return true;
        }
        else
        {
            return false;
        }
    }




    public float PositionHeadHeight()
    {
        return headHeight.localPosition.y;
    }


    private void PositionHeadHeight(float height)
    {
        headHeight.localPosition = new Vector3(0, height, 0);
    }
}
