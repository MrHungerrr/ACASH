using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody RB;
    [HideInInspector]
    public bool moving;


    [HideInInspector]
    public Vector2 moveInput;
    private Vector3 move;

    [HideInInspector]
    public float rotateAngle;
    private float last_rotateAngle;

    public enum movement
    {
        Crouch,
        Normal,
        Run,
    }

    [HideInInspector]
    public movement type_movement;
    private const int RB_coef = 2500;
    private float crouchSpeed = 1f;
    private float normalSpeed = 2f;
    private float runSpeed = 3.5f;
    private float crouchSound = 0.5f;
    private float normalSound = 3f;
    private float runSound = 5f;
    private float movementSpeed;
    private float movementSound;
    




    public void Setup()
    {
        this.RB = GetComponent<Rigidbody>();
        SwitchMove(movement.Normal);
    }



    public void Update()
    {
        MoveCalculate();
    }

    public void FixedUpdate()
    {
        if (move != Vector3.zero)
        {
            moving = true;
            PlayerMovement();
        }
        else
        {
            moving = false;
        }

        if (rotateAngle != 0f)
        {
            Rotate();
        }
    }

    public void SwitchMove(movement type)
    {
        type_movement = type;

        switch (type)
        {
            case movement.Normal:
                {
                    movementSpeed = normalSpeed;
                    movementSound = normalSound;
                    break;
                }
            case movement.Run:
                {
                    movementSpeed = runSpeed;
                    movementSound = runSound;
                    break;
                }
            case movement.Crouch:
                {
                    //Присесть на корточки
                    movementSpeed = crouchSpeed;
                    movementSound = crouchSound;
                    break;
                }
        }
    }

    private void MoveCalculate()
    {
        if (moveInput != Vector2.zero)
        {
            Vector3 moveInput3 = moveInput.normalized;
            moveInput3 = new Vector3(moveInput3.x, 0, moveInput3.y);
            move = moveInput3;

            //Debug.Log(moveInput3 + "\n" + move);
        }
    }

    private void PlayerMovement()
    {
        Vector3 new_move = transform.TransformDirection(move * movementSpeed * RB_coef * Time.fixedDeltaTime);
        //Vector3 new_position = transform.position + new_move;

        RB.AddForce(new_move);

        ScholarManager.get.Hear(movementSound);

        move = Vector3.zero;
    }


    private void Rotate()
    {
        Quaternion rotation_goal = Quaternion.Euler(0, transform.eulerAngles.y + rotateAngle, 0);
        RB.MoveRotation(rotation_goal);
        rotateAngle = 0f;
    }


    public Vector3 Position()
    {
        return transform.position;
    }

    public void Position(Vector3 position)
    {
        RB.position = position;
    }



}
