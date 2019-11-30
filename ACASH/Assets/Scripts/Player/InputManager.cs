using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using N_BH;



public class InputManager : Singleton<InputManager>
{

    private PlayerControls Controls;


    [HideInInspector]
    public string inputType;
    [HideInInspector]
    public string gameType;
    [HideInInspector]
    public string gameType_last;


    [HideInInspector]
    public bool hold_crouch;

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
        Controls.Gameplay.Crouch.started += ctx => GameCrouch(true);
        Controls.Gameplay.Crouch.canceled += ctx => GameCrouch(false);
        Controls.Gameplay.Bull.started += ctx => GameBull(true);
        Controls.Gameplay.Joke.started += ctx => GameJoke(true);
        Controls.Gameplay.Bull.canceled += ctx => GameBull(false);
        Controls.Gameplay.Joke.canceled += ctx => GameJoke(false);
        Controls.Gameplay.Shout.started += ctx => GameShout();
        Controls.Gameplay.Execute.started += ctx => GameExecute();
        Controls.Gameplay.HUD.started += ctx => GameHUD();
        Controls.Gameplay.Menu.started += ctx => GameMenu();

        Controls.Menu.Move.performed += ctx => Menu.get.moveInput = ctx.ReadValue<Vector2>();
        Controls.Menu.Move.canceled += ctx => Menu.get.moveInput = Vector2.zero;
        Controls.Menu.Select.started += ctx => MenuSelect();
        Controls.Menu.Escape.started += ctx => MenuEscape();
        Controls.Menu.Resume.started += ctx => MenuResume();

        Controls.Computer.Move.performed += ctx => ComputerManager.get.mouseInput = ctx.ReadValue<Vector2>();
        Controls.Computer.Move.canceled += ctx => ComputerManager.get.mouseInput = Vector2.zero;
        Controls.Computer.Fast.started += ctx => ComputerFast(true);
        Controls.Computer.Fast.canceled += ctx => ComputerFast(false);
        Controls.Computer.Zoom.started += ctx => ComputerZoom(true);
        Controls.Computer.Zoom.canceled += ctx => ComputerZoom(false);
        Controls.Computer.Select.started += ctx => ComputerSelect();
        Controls.Computer.Exit.started += ctx => ComputerExit();
        Controls.Computer.Menu.started += ctx => ComputerMenu();

        Controls.DoorLock.Camera.performed += ctx => DoorLockManager.get.rotInput = ctx.ReadValue<Vector2>();
        Controls.DoorLock.Camera.canceled += ctx => DoorLockManager.get.rotInput = Vector2.zero;
        Controls.DoorLock.Zoom.started += ctx => DoorLockZoom(true);
        Controls.DoorLock.Zoom.canceled += ctx => DoorLockZoom(false);
        Controls.DoorLock.Exit.started += ctx => DoorLockExit();
        Controls.DoorLock.Menu.started += ctx => DoorLockMenu();

        Controls.Execute.Move.performed += ctx => ExecuteHUDController.get.moveInput = ctx.ReadValue<float>();
        Controls.Execute.Move.canceled += ctx => ExecuteHUDController.get.moveInput = 0f;
        Controls.Execute.Accept.started += ctx => ExecuteAccept();
        Controls.Execute.Back.started += ctx => ExecuteBack();
        Controls.Execute.Menu.started += ctx => ExecuteMenu();


        Controls.InputType.Keyboard.performed += ctx => TypeOfInput("keyboard");
        Controls.InputType.PlayStation.performed += ctx => TypeOfInput("playstation");
        Controls.InputType.Xbox.performed += ctx => TypeOfInput("xbox");
    }

    private void Start()
    {
        //menu = GameObject.FindObjectOfType<Menu>();
        //menu.SwitchMenu(false);
        //SwitchGameInput("menu");
        //TypeOfInput("keyboard");
        Controls.InputType.Enable();
    }


    public void SwitchGameInput(string type)
    {
        Controls.Gameplay.Disable();
        Controls.Menu.Disable();
        Controls.Computer.Disable();
        Controls.Cutscene.Disable();
        Controls.Execute.Disable();
        Controls.DoorLock.Disable();

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
            case "doorlock":
                {
                    Controls.DoorLock.Enable();
                    break;
                }
            case "cutscene":
                {
                    Controls.Gameplay.Enable();
                    break;
                }
            case "execute":
                {
                    Controls.Execute.Enable();
                    break;
                }
            case "disable":
                {
                    break;
                }
            default:
                {
                    Debug.Log("<color=red>Input Manager</color> Не правильно введен тип управления");
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
            ExecuteHUDController.get.InputType();

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

    private void GameCrouch(bool option)
    {
        if(option)
        {
            if (hold_crouch)
            {
                Player.get.SwitchMove("crouch");
            }
            else
            {
                if (Player.get.typeOfMovement != "crouch")
                    Player.get.SwitchMove("crouch");
                else
                    Player.get.SwitchMove("normal");
            }
        }
        else
        {
            if (hold_crouch)
            {
                Player.get.SwitchMove("normal");
            }
        }
    }

    private void GameJoke(bool option)
    {
        if (option)
        {
            if (!Player.get.act && Player.get.actReady && Player.get.actTag == "Scholar")
            {
                if (Player.get.asked)
                    Player.get.Answer(true);
                else
                    Player.get.Bull(false);
            }

            if (!Player.get.draw && Player.get.actReady && Player.get.actTag == "DeskBlock")
            {         
                Player.get.Draw(true);
            }
        }
        else
        {
            if(Player.get.draw)
                Player.get.Draw(false);
        }
    }

    private void GameBull(bool option)
    {
        if (option)
        {
            if (!Player.get.act && Player.get.actReady && Player.get.actTag == "Scholar")
            {
                if (Player.get.asked)
                    Player.get.Answer(false);
                else
                    Player.get.Bull(true);
            }


            if (!Player.get.draw && Player.get.actReady && Player.get.actTag == "DeskBlock")
            {
                Player.get.UnDraw(true);
            }
        }
        else
        {
            if (Player.get.draw)
                Player.get.UnDraw(false);
        }
    }

    private void GameShout()
    {
        if (!Player.get.act)
            Player.get.Shout();
    }

    private void GameExecute()
    {
        if (!Player.get.act && Player.get.actReady && Player.get.actTag == "Scholar")
        {
            HUDManager.get.ExecuteHUD(true);
            SwitchGameInput("execute");
        }
    }

    private void GameHUD()
    {
        HUDManager.get.ControlHUD();
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
        if (GameManager.get.game)
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
        ComputerManager.get.Select();    
    }

    private void ComputerExit()
    {
        ComputerManager.get.Exit();
        SwitchGameInput("gameplay");
    }

    private void ComputerFast(bool option)
    {
        ComputerManager.get.fast = option;
    }

    private void ComputerZoom(bool option)
    {
        ComputerManager.get.Zoom(option);
    }

    private void ComputerMenu()
    {
        Menu.get.MenuEnable(true);
        SwitchGameInput("menu");
    }




    //----------------------------------------------------------------------------------------------------------------------------------------------------------------
    //DoorLock Input
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------

    private void DoorLockExit()
    {
        DoorLockManager.get.Exit();
        SwitchGameInput("gameplay");
    }

    private void DoorLockZoom(bool option)
    {
        DoorLockManager.get.Zoom(option);
    }

    private void DoorLockMenu()
    {
        Menu.get.MenuEnable(true);
        SwitchGameInput("menu");
    }





    //----------------------------------------------------------------------------------------------------------------------------------------------------------------
    //DoorLock Input
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------

    private void ExecuteAccept()
    {
        ExecuteHUDController.get.Accept();
    }

    public void ExecuteBack()
    {
        HUDManager.get.ExecuteHUD(false);
        SwitchGameInput("gameplay");
    }

    private void ExecuteMenu()
    {
        Menu.get.MenuEnable(true);
        SwitchGameInput("menu");
    }





    //----------------------------------------------------------------------------------------------------------------------------------------------------------------
    //Cutscene Input
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------
}
