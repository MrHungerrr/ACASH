using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoginController : MonoBehaviour
{

    private A_Computer Comp;
    [HideInInspector]
    public InputFieldLogin login;
    [HideInInspector]
    public InputFieldLogin password;
    [HideInInspector]
    public User user;
    private TextMeshProUGUI invalid;

    public bool loged { get; private set;}



    public void SetLoginController(A_Computer c)
    {
        Transform buf = transform.Find("Login");

        login = buf.Find("Input Field Login").GetComponent<InputFieldLogin>();
        password = buf.Find("Input Field Password").GetComponent<InputFieldLogin>();
        invalid = buf.Find("Error Message").GetComponent<TextMeshProUGUI>();

        login.SetInputField();
        password.SetInputField();
        loged = false;

        Comp = c;
    }

    public void Reset()
    {
        login.Reset();
        password.Reset();
        loged = false;
        invalid.text = "";
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
                loged = true;
                Comp.Desktop.SetUser(user);
                Comp.Commands.Do("Desktop");
                //Обновление коллайдеров
            }
            else
            {
                password.Reset();
                Comp.Numpad.Set(password);
                invalid.text = "Invalid Password";
            }
        }
        else
        {
            login.Reset();
            password.Reset();
            Comp.Numpad.Set(login);
            invalid.text = "Invalid Username";
        }
    }

}
