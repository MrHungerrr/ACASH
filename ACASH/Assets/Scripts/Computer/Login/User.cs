using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New User", menuName = "Computer/User")]
public class User : ScriptableObject
{
    [HideInInspector]
    public string login;
    [HideInInspector]
    public string password;
    public Sprite background;
    public Icon[] icons;



    public void SetKeys(string new_login, string new_password)
    {
        login = new_login;
        password = new_password;
    }
}
