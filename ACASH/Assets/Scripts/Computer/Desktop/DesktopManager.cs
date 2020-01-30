using UnityEngine;

public static class DesktopManager
{

    public static void SetDesktopManager()
    {
        DesktopController[] DCs = GameObject.FindObjectsOfType<DesktopController>();

        foreach(DesktopController DC in DCs)
        {
            DC.SetDesktopController();
        }
    }


    public static User GetUser(string name, string password)
    {
        User user = LoadUser(name);

        if(user != null && user.password == password)
        {
            return user;
        }
        else
        {
            return null;
        }

    }


    public static User LoadUser(string key)
    {
        try
        {
            return Resources.Load<User>("Users/" + key);
        }
        catch
        {
            return null;
        }
    }
}
