using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Single;

public class UserManager : MonoSingleton<UserManager>
{
    [SerializeField] 
    private User[] users;

    [HideInInspector]
    public Dictionary<string, User> login_user;



    public void SetUserManager()
    {
      /*  foreach(User user in users)
        {
            user.SetKeys(RandomFourDigits(), RandomFourDigits());
            Debug.Log(user.name + "\nLogin: " + user.login + "   Password: " + user.password);
            login_user.Add(user.login, user);
        }
        */
        login_user =  new Dictionary<string, User>();

        for (int i = 0; i< users.Length; i++)
        {

            users[i].SetKeys(i.ToString() + i.ToString() + i.ToString() + i.ToString(), i.ToString() + i.ToString() + i.ToString() + i.ToString());
            login_user.Add(users[i].login, users[i]);
        }
    }


    private string RandomFourDigits()
    {
        string res = "";

        for(int i = 0; i<4; i++)
        {
            res += Random.Range(0, 5).ToString();
        }

        return res;
    }


    public User GetUser(string name)
    {
        for(int i = 0; i < users.Length; i++)
        {
            if (name == users[i].name)
                return users[i];
        }
        return null;
    }
}
