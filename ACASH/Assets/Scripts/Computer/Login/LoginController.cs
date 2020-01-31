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
        Transform buf = transform.Find("Login").Find("Input Zone");

        login = buf.Find("Login").GetComponentInChildren<InputField>();
        password = buf.Find("Password").GetComponentInChildren<InputField>();
        Comp = c;
    }


    public void TryLogin()
    {
        user = UserManager.get.login_user[login.text];

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
