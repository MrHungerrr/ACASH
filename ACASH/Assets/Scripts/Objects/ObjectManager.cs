using UnityEngine;
using Single;


public class ObjectManager: Singleton<ObjectManager>
{

    private Door[] doors;

    public void SetLevel()
    {
        doors = GameObject.FindObjectsOfType<Door>();

        foreach (Door door in doors)
        {
            door.Enable(true);
        }
    }


    public void UnsetLevel()
    {
        foreach(Door door in doors)
        {
            door.Enable(false);
        }
    }
}
