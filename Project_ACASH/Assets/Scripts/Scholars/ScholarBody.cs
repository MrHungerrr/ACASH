using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using Animations;

public class ScholarBody : MonoBehaviour
{
    private bool active = false;

    private Transform head;
    private Transform head_target;

    private Transform head_height;
    private float head_height_target;
    private bool head_height_moving;

    private Transform body;
    private Vector3 body_target;
    private bool body_moving;




    public void Setup(Scholar scholar)
    {
        active = true;
        head = transform.parent.Find("Head");
        head_height = head.Find("Model");
        body = transform.parent.Find("Scholar");
        head_target = body.Find("Scholar").Find("Spine").Find("Head Target");

        body_moving = false;

        PositionBody(Vector3.zero);
        PositionHeadHeight(0.04f);
    }

    private void Update()
    {
        if (active)
        {
            if (body_moving)
                MoveBody();

            if (head_height_moving)
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
        body_target = new Vector3(0, height, 0);
        body_moving = true;
    }



    private void MoveBody()
    {
        MoveBodyTo(body_target);

        if (BodyOnPosition())
        {
            body_moving = false;
        }
    }

    private void MoveBodyTo(Vector3 target)
    {
        PositionBody(Vector3.Slerp(PositionBody(), target, 6f * Time.deltaTime));
    }


    public bool BodyOnPosition()
    {
        if (PositionBody() == body_target)
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
        return body.localPosition;
    }


    private void PositionBody(Vector3 set_position)
    {
        body.localPosition = set_position;
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
            Vector3 target = new Vector3(head_target.position.x, head_target.position.y, head_target.position.z);
            MoveHeadTo(target);
        }
    }


    public bool HeadOnPosition()
    {
        if (head.position == head_target.position)
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
        return head.position;
    }

    public Quaternion RotationHead()
    {
        return head.rotation;
    }

    private void PositionHead(Vector3 set_position)
    {
        head.position = set_position;
    }

    private void RotationHead(Quaternion set_rotation)
    {
        head.rotation = set_rotation;
    }





    //===========================================================================================================================
    //===========================================================================================================================
    //Движение бошки




    public void SetHeadHeightTarget(float height)
    {
        head_height_target = height;
        head_height_moving = true;
    }



    private void MoveHeadHeight()
    {
        MoveHeadHeightTo(head_height_target);

        if (HeadHeightOnPosition())
        {
            head_height_moving = false;
        }
    }

    private void MoveHeadHeightTo(float target)
    {
        PositionHeadHeight(Mathf.Lerp(PositionHeadHeight(), target, 6f * Time.deltaTime));
    }


    public bool HeadHeightOnPosition()
    {
        if (PositionHeadHeight() == head_height_target)
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
        return head_height.localPosition.y;
    }


    private void PositionHeadHeight(float height)
    {
        head_height.localPosition = new Vector3(0, height, 0);
    }
}
