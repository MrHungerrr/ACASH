using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Single;



public class InputManager : Singleton<InputManager>
{

    private Player Player;
    private PlayerControls Controls;


    [HideInInspector]
    public string inputType;
    [HideInInspector]
    public string gameType;
    [HideInInspector]
    public string gameType_last;


    [HideInInspector]
    public bool hold_crouch;



    private void Awake()
    {
        Player = GameObject.FindObjectOfType<Player>();
        Controls = new PlayerControls();

        Controls.Gameplay.Camera.performed += ctx => Player.Camera.rotateInput = ctx.ReadValue<Vector2>();
        Controls.Gameplay.Camera.canceled += ctx => Player.Camera.rotateInput = Vector2.zero;
        Controls.Gameplay.Move.performed += ctx => Player.Move.moveInput = ctx.ReadValue<Vector2>();
        Controls.Gameplay.Move.canceled += ctx => Player.Move.moveInput = Vector2.zero;
        Controls.Gameplay.Action.started += ctx => GameAction(true);
        Controls.Gameplay.Action.canceled += ctx => GameAction(false);
        Controls.Gameplay.Zoom.started += ctx => GameZoom(true);
        Controls.Gameplay.Zoom.canceled += ctx => GameZoom(false);
        Controls.Gameplay.Run.started += ctx => GameRun(true);
        Controls.Gameplay.Run.canceled += ctx => GameRun(false);
        Controls.Gameplay.Crouch.started += ctx => GameCrouch(true);
        Controls.Gameplay.Crouch.canceled += ctx => GameCrouch(false);
        Controls.Gameplay.Bull.started += ctx => GameTalkBad(true);
        Controls.Gameplay.Joke.started += ctx => GameTalkGood(true);
        Controls.Gameplay.Bull.canceled += ctx => GameTalkBad(false);
        Controls.Gameplay.Joke.canceled += ctx => GameTalkGood(false);
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


    private bool GameCanIDoAction()
    {
        return !Player.Talk.talking && !Player.Action.doing;
    }



    private void GameAction(bool option)
    {
        if (option)
        {
            if(GameCanIDoAction())
                Player.Action.Doing(true);
        }
        else
        {
            Player.Action.Doing(false);
        }
    }

    private void GameZoom(bool option)
    {
        if (option)
        {
                Player.Camera.Zoom(true);
        }
        else
        {
            Player.Camera.Zoom(false);
        }
    }

    private void GameRun(bool option)
    {
        if (option)
        {
            Player.Move.SwitchMove(PlayerMove.movement.Run);
        }
        else if (Player.Move.type_movement == PlayerMove.movement.Run)
        {
            Player.Move.SwitchMove(PlayerMove.movement.Normal);
        }
    }

    private void GameCrouch(bool option)
    {
        if(option)
        {
            if (hold_crouch)
            {
                Player.Move.SwitchMove(PlayerMove.movement.Crouch);
            }
            else
            {
                if (Player.Move.type_movement != PlayerMove.movement.Crouch)
                    Player.Move.SwitchMove(PlayerMove.movement.Crouch);
                else
                    Player.Move.SwitchMove(PlayerMove.movement.Normal);
            }
        }
        else
        {
            if (hold_crouch)
            {
                Player.Move.SwitchMove(PlayerMove.movement.Normal);
            }
        }
    }

    private void GameTalkGood(bool option)
    {
        if (option)
        {
            if (Player.Select.TryGetScholar() && GameCanIDoAction())
            {
                Player.Talk.TalkGood();
            }
        }
    }

    private void GameTalkBad(bool option)
    {
        if (option)
        {
            if (Player.Select.TryGetScholar() && GameCanIDoAction())
            {
                Player.Talk.TalkBad();
            }
        }
    }

    private void GameShout()
    {
        if(GameCanIDoAction())
            Player.Talk.Shout();
    }

    private void GameExecute()
    {
        if (Player.Select.TryGetScholar() && GameCanIDoAction())
        {
            Player.Talk.Execute();
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
