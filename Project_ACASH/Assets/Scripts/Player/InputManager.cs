using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Single;



public static class InputManager
{
    public enum Input
    { 
        Xbox,
        Playstation,
        Keyboard
    }

    public enum GameplayType
    {
        FirstPerson,
        Menu,
        Computer,
        Cutscene,
        Disable,
    }


    public static PlayerControls Controls => _controls;
    public static Action OnGameTypeChanged;
    public static Input InputType => _inputType;
    public static GameplayType GameType => _gameType;


    private static PlayerControls _controls;

    private static Input _inputType;
    private static GameplayType _gameType;
    private static GameplayType _gameTypeLast;




    public static void Setup()
    {
        _controls = new PlayerControls();

        #region First Person Gameplay
        _controls.Gameplay.Camera.performed += ctx => Player.Instance.Camera.RotateInput(ctx.ReadValue<Vector2>());
        _controls.Gameplay.Camera.canceled += ctx => Player.Instance.Camera.RotateInput(Vector2.zero);
        _controls.Gameplay.Move.performed += ctx => Player.Instance.Move.moveInput = ctx.ReadValue<Vector2>();
        _controls.Gameplay.Move.canceled += ctx => Player.Instance.Move.moveInput = Vector2.zero;
        _controls.Gameplay.Action.started += ctx => GameAction(true);
        _controls.Gameplay.Action.canceled += ctx => GameAction(false);
        _controls.Gameplay.Zoom.started += ctx => GameZoom(true);
        _controls.Gameplay.Zoom.canceled += ctx => GameZoom(false);
        _controls.Gameplay.Run.started += ctx => GameRun(true);
        _controls.Gameplay.Run.canceled += ctx => GameRun(false);
        _controls.Gameplay.Execute.started += ctx => GameExecute();
        _controls.Gameplay.HUD.started += ctx => GameHUD();
        _controls.Gameplay.Menu.started += ctx => GameMenu();
        #endregion

        #region Menu
        _controls.Menu.Move.performed += ctx => Menu.Instance.moveInput = ctx.ReadValue<Vector2>();
        _controls.Menu.Move.canceled += ctx => Menu.Instance.moveInput = Vector2.zero;
        _controls.Menu.Select.started += ctx => MenuSelect();
        _controls.Menu.Escape.started += ctx => MenuEscape();
        _controls.Menu.Resume.started += ctx => MenuResume();
        #endregion

        #region Computer
        _controls.Computer.Move.performed += ctx => ComputerManager.Instance.MouseInput(ctx.ReadValue<Vector2>());
        _controls.Computer.Move.canceled += ctx => ComputerManager.Instance.MouseInput(Vector2.zero);
        _controls.Computer.Zoom.started += ctx => ComputerZoom(true);
        _controls.Computer.Zoom.canceled += ctx => ComputerZoom(false);
        _controls.Computer.Select.started += ctx => ComputerSelect();
        _controls.Computer.Exit.started += ctx => ComputerExit();
        _controls.Computer.Menu.started += ctx => ComputerMenu();
        #endregion

        #region Cutscene 
        _controls.Cutscene.Menu.started += ctx => GameMenu();
        #endregion

        #region Type of Input
        _controls.InputType.Keyboard.performed += ctx => TypeOfInput(Input.Keyboard);
        _controls.InputType.PlayStation.performed += ctx => TypeOfInput(Input.Playstation);
        _controls.InputType.Xbox.performed += ctx => TypeOfInput(Input.Xbox);
        #endregion


        _controls.InputType.Enable();


    }



    public static void SwitchGameInput(GameplayType type)
    {
        _controls.Gameplay.Disable();
        _controls.Menu.Disable();
        _controls.Computer.Disable();
        _controls.Cutscene.Disable();

        switch (type)
        {
            case GameplayType.FirstPerson:
                {
                    _controls.Gameplay.Enable();
                    break;
                }
            case GameplayType.Menu:
                {
                    _controls.Menu.Enable();
                    break;
                }
            case GameplayType.Computer:
                {
                    _controls.Computer.Enable();
                    break;
                }
            case GameplayType.Cutscene:
                {
                    _controls.Cutscene.Enable();
                    break;
                }
            case GameplayType.Disable:
                {
                    break;
                }
            default:
                {
                    Debug.Log("<color=red>Input Manager</color> Не правильно введен тип управления");
                    break;
                }
        }
        _gameTypeLast = _gameType;
        _gameType = type;

        OnGameTypeChanged?.Invoke();
    }


    private static void TypeOfInput(Input type)
    {
        if (_inputType != type)
        {
            _inputType = type;
            Menu.Instance.InputType();

            switch (type)
            {
                case Input.Keyboard:
                    {
                        break;
                    }
                case Input.Playstation:
                    {
                        break;
                    }
                case Input.Xbox:
                    {
                        break;
                    }
            }
        }
    }

    //----------------------------------------------------------------------------------------------------------------------------------------------------------------
    //Gameplay Input
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------


    private static bool GameCanITalk()
    {
        return !Player.Instance.Talk.talking && !Player.Instance.Action.doing;
    }

    private static bool GameCanIDoAction()
    {
        return !Player.Instance.Action.doing;
    }

    private static void GameAction(bool option)
    {
        if (option)
        {
            if(GameCanIDoAction())
                Player.Instance.Action.Doing(true);
        }
        else
        {
            Player.Instance.Action.Doing(false);
        }
    }

    private static void GameZoom(bool option)
    {
        if (option)
        {
                Player.Instance.Camera.Zoom(true);
        }
        else
        {
            Player.Instance.Camera.Zoom(false);
        }
    }

    private static void GameRun(bool option)
    {
        if (option)
        {
            Player.Instance.Move.SwitchMove(PlayerMove.movement.Run);
        }
        else if (Player.Instance.Move.type_movement == PlayerMove.movement.Run)
        {
            Player.Instance.Move.SwitchMove(PlayerMove.movement.Normal);
        }
    }


    private static void GameTalkGood(bool option)
    {
        if (option)
        {
            if (Player.Instance.Select.TryGetScholar() && GameCanITalk())
            {
                Player.Instance.Talk.TalkGood();
            }
        }
    }

    private static void GameTalkBad(bool option)
    {
        if (option)
        {
            if (Player.Instance.Select.TryGetScholar() && GameCanITalk())
            {
                Player.Instance.Talk.TalkBad();
            }
        }
    }

    private static void GameShout()
    {
        if(GameCanITalk())
            Player.Instance.Talk.Shout();
    }

    private static void GameExecute()
    {
        if (Player.Instance.Select.TryGetScholar() && GameCanITalk())
        {
            Player.Instance.Talk.Execute();
        }
    }

    private static void GameHUD()
    {
        HUDManager.Instance.ControlHUD();
    }

    private static void GameMenu()
    {
        Menu.Instance.MenuEnable(true);
        SwitchGameInput(GameplayType.Menu);
    }





    //----------------------------------------------------------------------------------------------------------------------------------------------------------------
    //Menu Input
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------

    public static void MenuResume()
    {
        if (GameManager.Instance.Game)
        {
            Menu.Instance.MenuEnable(false);
            SwitchGameInput(_gameTypeLast);
        }
    }

    private static void MenuEscape()
    {
        Menu.Instance.Escape();
    }

    private static void MenuSelect()
    {
        Menu.Instance.Enter();
    }




    //----------------------------------------------------------------------------------------------------------------------------------------------------------------
    //Computer Input
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------

    public static void ComputerSelect()
    {
        ComputerManager.Instance.Select();    
    }

    private static void ComputerExit()
    {
        ComputerManager.Instance.Exit();
        SwitchGameInput(GameplayType.FirstPerson);
    }

    private static void ComputerZoom(bool option)
    {
        ComputerManager.Instance.Zoom(option);
    }

    private static void ComputerMenu()
    {
        Menu.Instance.MenuEnable(true);
        SwitchGameInput(GameplayType.Menu);
    }
}
