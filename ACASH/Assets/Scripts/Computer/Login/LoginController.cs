using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginController : MonoBehaviour
{

    private Computer Comp;
    [HideInInspector]
    public InputField login;
    [HideInInspector]
    public InputField password;
    [HideInInspector]
    public User user;



    public void SetLoginController(Computer c)
    {
        Transform buf = transform.Find("Login");

        login = buf.Find("Input Field Login").GetComponent<InputField>();
        password = buf.Find("Input Field Password").GetComponent<InputField>();

        login.SetInputField();
        password.SetInputField();

        Comp = c;
    }

    public void Reset()
    {
        if (Comp != null)
        {
            login.Reset();
            password.Reset();
            Comp.Numpad.Set(login);
        }
    }


    public void TryLogin()
    {

        Debug.Log("Login: " + login.text + "\nPassword: " + password.text);

        try
        {
            user = UserManager.get.login_user[login.text];
        }
        catch
        {
            user = null;
        }


        if(user != null)
        {
            if(user.password == password.text)
            {
                Comp.Desktop.SetUser(user);
                Comp.Windows.Enter("Desktop");
                //Обновление коллайдеров
            }
            else
            {
                Debug.Log("Неверный пароль");
            }
        }
        else
        {
            Debug.Log("Не существующий логин");
        }
    }



}
