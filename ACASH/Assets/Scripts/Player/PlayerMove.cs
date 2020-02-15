using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour
{
    private CharacterController CharController;


    [HideInInspector]
    public Vector2 moveInput;
    private Vector2 move;


    public enum movement
    {
        Crouch,
        Normal,
        Run,
    }

    [HideInInspector]
    public movement type_movement;
    private float crouchSpeed = 35f;
    private float normalSpeed = 65f;
    private float runSpeed = 100f;
    private float crouchSound = 0.5f;
    private float normalSound = 3f;
    private float runSound = 5f;
    private float movementSpeed;
    private float movementSound;





    public void SetupMove()
    {
        CharController = GetComponent<CharacterController>();
        SwitchMove(movement.Normal);
    }



    public void Update()
    {
        PlayerMovement();
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

    private void PlayerMovement()
    {
        move = moveInput.normalized * movementSpeed * Time.deltaTime;

        Vector3 forwardMovement = transform.forward * move.y;
        Vector3 rightMovement = transform.right * move.x;

        if (forwardMovement != Vector3.zero || rightMovement != Vector3.zero)
        {
            CharController.SimpleMove(forwardMovement + rightMovement);
            ScholarManager.get.Hear(movementSound);
        }
    }

}
