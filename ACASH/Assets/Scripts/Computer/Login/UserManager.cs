using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using N_BH;

public class UserManager : Singleton<UserManager>
{
    [SerializeField] 
    private User[] users;

    [HideInInspector]
    public Dictionary<string, User> login_user = new Dictionary<string, User>();



    public void SetUserManager()
    {
        foreach(User user in users)
        {
            user.SetKeys(RandomFourDigits(), RandomFourDigits());
            login_user.Add(user.login, user);
        }
    }


    private string RandomFourDigits()
    {
        string res = "";

        for(int i = 0; i<4; i++)
        {
            res += Random.Range(0, 10).ToString();
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
