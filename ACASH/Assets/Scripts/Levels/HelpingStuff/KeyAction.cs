using PlayerOptions;


public class KeyAction
{

    public ActionEvent.OnAction OnKeyDown;
    public ActionEvent.OnAction OnKeyUp;

    public bool pressing;


    public void Setup(GetP.actions action)
    {
        Remove();

        switch(action)
        {
            case GetP.actions.Talk_Good:
                {
                    InputManager.get.Controls.Gameplay.Joke.started += ctx => KeyDown();
                    InputManager.get.Controls.Gameplay.Joke.canceled += ctx => KeyUp();
                    break;
                }
            case GetP.actions.Talk_Bad:
                {
                    InputManager.get.Controls.Gameplay.Bull.started += ctx => KeyDown();
                    InputManager.get.Controls.Gameplay.Bull.canceled += ctx => KeyUp();
                    break;
                }
            case GetP.actions.Answer_Yes:
                {
                    InputManager.get.Controls.Gameplay.Joke.started += ctx => KeyDown();
                    InputManager.get.Controls.Gameplay.Joke.canceled += ctx => KeyUp();
                    break;
                }
            case GetP.actions.Answer_No:
                {
                    InputManager.get.Controls.Gameplay.Bull.started += ctx => KeyDown();
                    InputManager.get.Controls.Gameplay.Bull.canceled += ctx => KeyUp();
                    break;
                }
            case GetP.actions.Action:
                {
                    InputManager.get.Controls.Gameplay.Action.started += ctx => KeyDown();
                    InputManager.get.Controls.Gameplay.Action.canceled += ctx => KeyUp();
                    break;
                }
            case GetP.actions.Zoom:
                {
                    InputManager.get.Controls.Gameplay.Zoom.started += ctx => KeyDown();
                    InputManager.get.Controls.Gameplay.Zoom.canceled += ctx => KeyUp();
                    break;
                }
            case GetP.actions.Crouch:
                {
                    InputManager.get.Controls.Gameplay.Crouch.started += ctx => KeyDown();
                    InputManager.get.Controls.Gameplay.Crouch.canceled += ctx => KeyUp();
                    break;
                }
            case GetP.actions.Execute:
                {
                    InputManager.get.Controls.Gameplay.Execute.started += ctx => KeyDown();
                    InputManager.get.Controls.Gameplay.Execute.canceled += ctx => KeyUp();
                    break;
                }
            case GetP.actions.Hud:
                {
                    InputManager.get.Controls.Gameplay.HUD.started += ctx => KeyDown();
                    InputManager.get.Controls.Gameplay.HUD.canceled += ctx => KeyUp();
                    break;
                }
            case GetP.actions.Menu:
                {
                    InputManager.get.Controls.Gameplay.Menu.started += ctx => KeyDown();
                    InputManager.get.Controls.Gameplay.Menu.canceled += ctx => KeyUp();
                    break;
                }
            case GetP.actions.Move:
                {
                    InputManager.get.Controls.Gameplay.Move.started += ctx => KeyDown();
                    InputManager.get.Controls.Gameplay.Move.canceled += ctx => KeyUp();
                    break;
                }
            case GetP.actions.Rotate:
                {
                    InputManager.get.Controls.Gameplay.Camera.started += ctx => KeyDown();
                    InputManager.get.Controls.Gameplay.Camera.canceled += ctx => KeyUp();
                    break;
                }
            case GetP.actions.Run:
                {
                    InputManager.get.Controls.Gameplay.Run.started += ctx => KeyDown();
                    InputManager.get.Controls.Gameplay.Run.canceled += ctx => KeyUp();
                    break;
                }
            case GetP.actions.Shout:
                {
                    InputManager.get.Controls.Gameplay.Shout.started += ctx => KeyDown();
                    InputManager.get.Controls.Gameplay.Shout.canceled += ctx => KeyUp();
                    break;
                }
        }
    }

    private void KeyDown()
    {
        if(OnKeyDown != null)
            OnKeyDown();

        pressing = true;
    }


    private void KeyUp()
    {
        if (OnKeyUp != null)
            OnKeyUp();

        pressing = false;
    }


    public void Remove()
    {
        ActionEvent.Unsubscribe(OnKeyDown);
        ActionEvent.Unsubscribe(OnKeyUp);

        pressing = false;
    }
}
