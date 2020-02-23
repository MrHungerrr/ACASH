using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoginController : MonoBehaviour
{

    private Computer Comp;
    [HideInInspector]
    public InputFieldCode login;
    [HideInInspector]
    public InputFieldCode password;
    [HideInInspector]
    public User user;
    private TextMeshProUGUI invalid;



    public void SetLoginController(Computer c)
    {
        Transform buf = transform.Find("Login");

        login = buf.Find("Input Field Login").GetComponent<InputFieldCode>();
        password = buf.Find("Input Field Password").GetComponent<InputFieldCode>();
        invalid = buf.Find("Error Message").GetComponent<TextMeshProUGUI>();

        login.SetInputField();
        password.SetInputField();

        Comp = c;
    }

    public void Reset()
    {
        login.Reset();
        password.Reset();
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
                Comp.Desktop.SetUser(user);
                Comp.Windows.Enter("Desktop");
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
