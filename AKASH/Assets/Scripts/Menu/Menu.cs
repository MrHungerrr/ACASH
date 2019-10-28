using UnityEngine;
using System.Collections;
using N_BH;


public class Menu : Singleton<Menu>
{
    private bool state;

    private const float dof_coef = 0.25f;

    [HideInInspector]
    public SliderController slider;
    private MenuSelectable[] menu_objects;
    private int count_row;
    //private int count_column;
    private int select_row;
    //private int select_column;
    private string select_name;
    [HideInInspector]
    public string current_menu;
    private bool settings;
    private string type_of_setting;

    [HideInInspector]
    public Vector2 moveInput;
    private Vector2 move;
    private int move_cd;
    private const int move_const_keyboard_cd = 12;
    private const int move_const_gamepad_cd = 12;


    void Awake()
    {
    }

    private void Start()
    {
        MenuEnable(false);
    }

    private void Update()
    {
        MoveVertical();
        if (settings)
            MoveHorizontal();
    }




    public void MenuEnable(bool u)
    {
        if (u)
        {
            StartCoroutine(MenuOn());
        }
        else
        {
            StartCoroutine(MenuOff());
        }
    }




    public void Select(int row)
    {
        if (select_name != null)
            menu_objects[select_row].Select(false);

        Debug.Log(row);

        select_name = menu_objects[row].topic;
        menu_objects[row].Select(true);
        select_row = row;

        Debug.Log("Выбранное меню: " + select_name);
    }




    public void Enter()
    {
        if (settings)
        {
            MenuAgent.get.Enter("Accept Settings");
        }
        else
        {
            MenuAgent.get.Enter(select_name);
        }
    }



    public void Escape()
    {
        MenuAgent.get.Escape(current_menu);
    }



    public void Set(MenuSelectable[] objects, string name, bool set)
    {
        settings = set;
        current_menu = name;
        count_row = objects.Length;
        menu_objects = new MenuSelectable[count_row];

        Debug.Log("Название меню: " + name + ". Количество элементов = " + count_row);

        for (int i = 0; i < count_row; i++)
        {
            Debug.Log(objects[i].topic);
            menu_objects[i] = objects[i];
            menu_objects[i].menu_nomber = i;
            menu_objects[i].Select(false);
        }

        select_name = null;
        Select(0);
    }




    private void MoveVertical()
    {
        if(Mathf.Abs(moveInput.y)>0.75)
        {
            if (move_cd <= 0)
            {

                if (moveInput.y > 0)
                {
                    int buf = select_row - 1;

                    if (buf < 0)
                        buf = count_row - 1;


                    Select(buf);
                }
                else
                {
                    int buf = select_row + 1;

                    if (buf >= count_row)
                        buf = 0;

                    Select(buf);
                }

                switch(InputManager.get.inputType)
                {
                    case "keyboard":
                        {
                            move_cd = move_const_keyboard_cd;
                            break;
                        }
                    default:
                        {
                            move_cd = move_const_gamepad_cd;
                            break;
                        }
                }

            }
        }
        else if (moveInput == Vector2.zero)
        {
            move_cd = 0;
        }

        if(move_cd > 0)
        {
            move_cd--;
        }
    }




    private void MoveHorizontal()
    {
        if (Mathf.Abs(moveInput.x) > 0.75)
        {
            if (move_cd <= 0)
            {

                if (moveInput.x > 0)
                {
                    //+1
                }
                else
                {
                    //-1
                }

                switch (InputManager.get.inputType)
                {
                    case "keyboard":
                        {
                            move_cd = move_const_keyboard_cd;
                            break;
                        }
                    default:
                        {
                            move_cd = move_const_gamepad_cd;
                            break;
                        }
                }

            }
        }
        else if (moveInput == Vector2.zero)
        {
            move_cd = 0;
        }

        if (move_cd > 0)
        {
            move_cd--;
        }
    }




    private void SwtichSettings()
    {
        switch(type_of_setting)
        {
            case "slider":
                {
                    break;
                }
            case "selector":
                {
                    break;
                }
        }
    }



    private IEnumerator MenuOn()
    {
        state = true;
        float dof = PostProcessManager.get.GetDOF();
        MenuAgent.get.Set("Pause");

        InputType();

        while (dof > 0.1)
        {
            dof -= (dof / 2) * dof_coef;
            PostProcessManager.get.DOF(dof);
            yield return new WaitForEndOfFrame();
        }
    }




    private IEnumerator MenuOff()
    {
        state = false;
        float dof = PostProcessManager.get.GetDOF();
        Cursor.lockState = CursorLockMode.Locked;

        if (current_menu != null)
            MenuAgent.get.Disable(current_menu);

        while (dof < 32)
        {
            dof += ((33 - dof) / 2) * dof_coef;
            PostProcessManager.get.DOF(dof);
            yield return new WaitForEndOfFrame();
        }

    }




    public void InputType()
    {
        if (state)
        {
            switch (InputManager.get.inputType)
            {
                case "keyboard":
                    {
                        Cursor.lockState = CursorLockMode.None;
                        break;
                    }
                case "playstation":
                    {
                        Cursor.lockState = CursorLockMode.Locked;
                        break;
                    }
                case "xbox":
                    {
                        Cursor.lockState = CursorLockMode.Locked;
                        break;
                    }

            }
        }
    }

}
