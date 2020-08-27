using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using Cinemachine;
using Computers;

public class ComputerControl: MonoBehaviour, IInteraction
{
    public ComputerCamera Camera => _camera;
    public ComputerCursor Cursor => _cursor;


    [SerializeField] private ComputerCursor _cursor;
    [SerializeField] private ComputerCamera _camera;

    private TeacherComputer _computer;

    private ComputerButton _selectedButton;
    private bool _isButtonSelected;





    public void  SetLevel(TeacherComputer computer)
    {
        _computer = computer;

        _cursor.SetLevel();
        _camera.SetLevel();

        Enable(false);
    }


    public void Interact()
    {
        Enable(true);
    }




    public void Enable(bool option)
    {
        if (option)
        {
            ComputerControlManager.Instance.Set(this);
            Player.Instance.Select.Disable(this.GetType());
        }
        else
        {
            Player.Instance.Select.Enable(this.GetType());
        }

        _camera.Enable(option);
    }


    public void MyUpdate()
    {
        MouseCollision();
        _camera.MyUpdate();
    }

    private void MouseCollision()
    {
        var collidedButton = GetCollision(_cursor.transform.position);

        if (collidedButton != null)
        {
            collidedButton.Select.Set(true);
            _cursor.ChangeImage(ComputerCursor.types.Finger);
            _isButtonSelected = true;
        }
        else if (_isButtonSelected)
        {
            collidedButton.Select.Set(false);
            _cursor.ChangeImage(ComputerCursor.types.Pointer);
            _isButtonSelected = false;
        }

        _selectedButton = collidedButton;
    }


    private ComputerButton GetCollision(in Vector2 Position)
    {
        foreach(var button in _computer.Windows.CurrentWindow.Buttons)
        {
            if(button.Collider.IsCollided(Position))
            {
                return button;
            }
        }

        return null;
    }

    public void Select()
    {
        _computer.Sound.PlayAnyway(ComputerSounds.sounds.Click);

        if (_isButtonSelected)
            _selectedButton.Execute();
    }
}
