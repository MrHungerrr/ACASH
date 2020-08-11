using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour
{
    public enum movement
    {
        Normal,
        Run,
    }

    public movement MovementType => _movementType;


    private const int RB_COEF = 2500;
    private const float NORMAL_SPEED = 2f;
    private const float RUN_SPEED = 3.5f;

    private Rigidbody _RB;

    private bool _moving;
    private float _movementSpeed;
    private movement _movementType;

    private Vector2 _moveInput;
    private Vector3 _move;

    private float _rotateAngle;


    public void Setup()
    {
        this._RB = GetComponent<Rigidbody>();
        SwitchMove(movement.Normal);
    }



    public void Update()
    {
        MoveCalculate();
    }

    public void FixedUpdate()
    {
        if (_move != Vector3.zero)
        {
            _moving = true;
            PlayerMovement();
        }
        else
        {
            _moving = false;
        }

        if (_rotateAngle != 0f)
        {
            Rotate();
        }
    }

    public void SwitchMove(movement type)
    {
        _movementType = type;

        switch (type)
        {
            case movement.Normal:
                {
                    _movementSpeed = NORMAL_SPEED;
                    break;
                }
            case movement.Run:
                {
                    _movementSpeed = RUN_SPEED;
                    break;
                }
        }
    }


    public void MoveInput(Vector3 input)
    {
        _moveInput = input;
    }


    private void MoveCalculate()
    {
        if (_moveInput != Vector2.zero)
        {
            Vector3 moveInput3 = _moveInput.normalized;
            moveInput3 = new Vector3(moveInput3.x, 0, moveInput3.y);
            _move = moveInput3;

            //Debug.Log(moveInput3 + "\n" + move);
        }
    }

    private void PlayerMovement()
    {
        Vector3 new_move = transform.TransformDirection(_move * _movementSpeed * RB_COEF * Time.fixedDeltaTime);
        //Vector3 new_position = transform.position + new_move;

        _RB.AddForce(new_move);

        _move = Vector3.zero;
    }


    public void AddRotateAngle(float angle)
    {
        _rotateAngle += angle;
    }


    private void Rotate()
    {
        Quaternion rotation_goal = Quaternion.Euler(0, transform.eulerAngles.y + _rotateAngle, 0);
        _RB.MoveRotation(rotation_goal);
        _rotateAngle = 0f;
    }


    public Vector3 Position()
    {
        return transform.position;
    }

    public void Position(Vector3 position)
    {
        _RB.position = position;
    }



}
