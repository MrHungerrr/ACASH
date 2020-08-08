using PlayerOptions;
using UnityEngine.Events;


public class KeyAction
{

    public UnityEvent OnKeyDown = new UnityEvent();
    public UnityEvent OnKeyUp = new UnityEvent();

    public bool pressing;


    public void Setup(GetP.actions action)
    {
        Remove();

        switch(action)
        {
            case GetP.actions.Action:
                {
                    InputManager.Controls.Gameplay.Action.started += ctx => KeyDown();
                    InputManager.Controls.Gameplay.Action.canceled += ctx => KeyUp();
                    break;
                }
            case GetP.actions.Zoom:
                {
                    InputManager.Controls.Gameplay.Zoom.started += ctx => KeyDown();
                    InputManager.Controls.Gameplay.Zoom.canceled += ctx => KeyUp();
                    break;
                }
            case GetP.actions.Execute:
                {
                    InputManager.Controls.Gameplay.Execute.started += ctx => KeyDown();
                    InputManager.Controls.Gameplay.Execute.canceled += ctx => KeyUp();
                    break;
                }
            case GetP.actions.Hud:
                {
                    InputManager.Controls.Gameplay.HUD.started += ctx => KeyDown();
                    InputManager.Controls.Gameplay.HUD.canceled += ctx => KeyUp();
                    break;
                }
            case GetP.actions.Menu:
                {
                    InputManager.Controls.Gameplay.Menu.started += ctx => KeyDown();
                    InputManager.Controls.Gameplay.Menu.canceled += ctx => KeyUp();
                    break;
                }
            case GetP.actions.Move:
                {
                    InputManager.Controls.Gameplay.Move.started += ctx => KeyDown();
                    InputManager.Controls.Gameplay.Move.canceled += ctx => KeyUp();
                    break;
                }
            case GetP.actions.Rotate:
                {
                    InputManager.Controls.Gameplay.Camera.started += ctx => KeyDown();
                    InputManager.Controls.Gameplay.Camera.canceled += ctx => KeyUp();
                    break;
                }
            case GetP.actions.Run:
                {
                    InputManager.Controls.Gameplay.Run.started += ctx => KeyDown();
                    InputManager.Controls.Gameplay.Run.canceled += ctx => KeyUp();
                    break;
                }
        }
    }

    private void KeyDown()
    {
        OnKeyDown.Invoke();

        pressing = true;
    }


    private void KeyUp()
    {
        OnKeyUp.Invoke();

        pressing = false;
    }


    public void Remove()
    {
        OnKeyDown.RemoveAllListeners();
        OnKeyUp.RemoveAllListeners();

        pressing = false;
    }
}
