using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using N_BH;



public class InputManager : Singleton<InputManager>
{

    [HideInInspector]
    public bool game;
    [HideInInspector]
    public bool cutScene;
    [HideInInspector]
    public bool disPlayer;

    private PlayerControls Controls;


    [HideInInspector]
    public string inputType;
    [HideInInspector]
    public string gameType;
    [HideInInspector]
    public string gameType_last;

    //private ParticleSystem.VelocityOverLifetimeModule vel;
    //private ParticleSystem.ShapeModule shape;

    private void Awake()
    {
        Controls = new PlayerControls();

        Controls.Gameplay.Camera.performed += ctx => PlayerCamera.get.rotateInput = ctx.ReadValue<Vector2>();
        Controls.Gameplay.Camera.canceled += ctx => PlayerCamera.get.rotateInput = Vector2.zero;
        Controls.Gameplay.Move.performed += ctx => Player.get.moveInput = ctx.ReadValue<Vector2>();
        Controls.Gameplay.Move.canceled += ctx => Player.get.moveInput = Vector2.zero;
        Controls.Gameplay.Action.started += ctx => GameAction(true);
        Controls.Gameplay.Action.canceled += ctx => GameAction(false);
        Controls.Gameplay.Zoom.started += ctx => GameZoom(true);
        Controls.Gameplay.Zoom.canceled += ctx => GameZoom(false);
        Controls.Gameplay.Run.started += ctx => GameRun(true);
        Controls.Gameplay.Run.canceled += ctx => GameRun(false);
        Controls.Gameplay.Crouch.started += ctx => GameCrouch();
        Controls.Gameplay.Bull.started += ctx => GameBull();
        Controls.Gameplay.Joke.started += ctx => GameJoke();
        Controls.Gameplay.Shout.started += ctx => GameShout();
        Controls.Gameplay.Execute.started += ctx => GameExecute();
        Controls.Gameplay.Menu.started += ctx => GameMenu();

        Controls.Menu.Move.performed += ctx => Menu.get.moveInput = ctx.ReadValue<Vector2>();
        Controls.Menu.Move.canceled += ctx => Menu.get.moveInput = Vector2.zero;
        Controls.Menu.Select.started += ctx => MenuSelect();
        Controls.Menu.Escape.started += ctx => MenuEscape();
        Controls.Menu.Resume.started += ctx => MenuResume();

        Controls.Computer.Move.performed += ctx => ComputerController.get.mouseInput = ctx.ReadValue<Vector2>();
        Controls.Computer.Move.canceled += ctx => ComputerController.get.mouseInput = Vector2.zero;
        Controls.Computer.Fast.started += ctx => ComputerFast(true);
        Controls.Computer.Fast.canceled += ctx => ComputerFast(false);
        Controls.Computer.Select.started += ctx => ComputerSelect();
        Controls.Computer.Escape.started += ctx => ComputerEscape();
        Controls.Computer.Menu.started += ctx => ComputerMenu();


        Controls.InputType.Keyboard.performed += ctx => TypeOfInput("keyboard");
        Controls.InputType.PlayStation.performed += ctx => TypeOfInput("playstation");
        Controls.InputType.Xbox.performed += ctx => TypeOfInput("xbox");
    }

    private void Start()
    {
        game = true;
        cutScene = false;
        disPlayer = false;

        //menu = GameObject.FindObjectOfType<Menu>();
        //menu.SwitchMenu(false);

        SwitchGameInput("gameplay");
        TypeOfInput("keyboard");
        Controls.InputType.Enable();


        SwitchGameInput("computer");

    }


    public void SwitchGameInput(string type)
    {
        Controls.Gameplay.Disable();
        Controls.Menu.Disable();
        Controls.Computer.Disable();
        Controls.Cutscene.Disable();

        switch (type)
        {
            case "gameplay":
                {
                    Controls.Gameplay.Enable();
                    break;
                }
            case "menu":
                {
                    Controls.Menu.Enable();
                    break;
                }
            case "computer":
                {
                    Controls.Computer.Enable();
                    break;
                }
            case "cutscene":
                {
                    Controls.Gameplay.Enable();
                    break;
                }
        }
        gameType_last = gameType;
        gameType = type;
        CameraManager.get.GameplayType();

        //Debug.Log("Тип управления - " + type);
    }


    private void TypeOfInput(string type)
    {
        if (inputType != type)
        {
            inputType = type;
            Menu.get.InputType();

            switch (type)
            {
                case "keyboard":
                    {
                        break;
                    }
                case "playstation":
                    {
                        break;
                    }
                case "xbox":
                    {
                        break;
                    }
            }
        }
    }




    //----------------------------------------------------------------------------------------------------------------------------------------------------------------
    //Gameplay Input
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------

    private void GameAction(bool u)
    {
        if (u)
        {
            Player.get.doing = true;
        }
        else if (Player.get.doing)
        {
            Player.get.doing = false;
            Player.get.act = false;
        }
    }

    private void GameZoom(bool u)
    {
        if(u)
        {
            Player.get.look_closer = true;
            PlayerCamera.get.zoom = true;
            PlayerCamera.get.zooming = true;
        }
        else
        {
            Player.get.look_closer = false;
            PlayerCamera.get.zoom = false;
        }
    }

    private void GameRun(bool u)
    {
        /*
        if (u)
        {
            pScript.SwitchMove("run");
        }
        else if (pScript.typeOfMovement == "run")
        {
            pScript.SwitchMove("normal");
        }
        */
    }

    private void GameCrouch()
    {
        if (Player.get.typeOfMovement != "crouch")
            Player.get.SwitchMove("crouch");
        else
            Player.get.SwitchMove("normal");

    }

    private void GameJoke()
    {
        if (!Player.get.act && Player.get.actReady && Player.get.actTag == "Scholar")
        {
            if (Player.get.asked)
                Player.get.Answer(true);
            else
                Player.get.Bull(false);
        }
    }

    private void GameBull()
    {
        if (!Player.get.act && Player.get.actReady && Player.get.actTag == "Scholar")
        {
            if (Player.get.asked)
                Player.get.Answer(false);
            else
                Player.get.Bull(true);
        }
    }

    private void GameShout()
    {
        if (!Player.get.act)
            Player.get.Shout();
    }

    private void GameExecute()
    {
        if (!Player.get.act && Player.get.actReady)
            Player.get.Execute();
    }

    private void GameMenu()
    {

        Menu.get.MenuEnable(true);
        SwitchGameInput("menu");
    }




    //----------------------------------------------------------------------------------------------------------------------------------------------------------------
    //Menu Input
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------

    public void MenuResume()
    {
        if (game)
        {
            Menu.get.MenuEnable(false);
            SwitchGameInput(gameType_last);
        }
    }

    private void MenuEscape()
    {
        Menu.get.Escape();
    }

    private void MenuSelect()
    {
        Menu.get.Enter();
    }




    //----------------------------------------------------------------------------------------------------------------------------------------------------------------
    //Computer Input
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------

    public void ComputerSelect()
    {
        ComputerController.get.Select();    
    }

    private void ComputerEscape()
    {
        ComputerController.get.Escape();
    }

    private void ComputerFast(bool option)
    {
        ComputerController.get.fast = option;
    }

    private void ComputerMenu()
    {
        Menu.get.MenuEnable(true);
        SwitchGameInput("menu");
    }



    //----------------------------------------------------------------------------------------------------------------------------------------------------------------
    //Cutscene Input
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------
}
