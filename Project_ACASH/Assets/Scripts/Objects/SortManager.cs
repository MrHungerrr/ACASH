using UnityEngine;
using Single;


public class SortManager: MonoSingleton<SortManager>
{
    public enum TypeOfSort
    {
        Max,
        Min
    }

    public enum MainCoordinate
    {
        X,
        Z
    }

    [SerializeField]
    private MainCoordinate MainLine;

    [SerializeField]
    private TypeOfSort First_Obj_X;

    [SerializeField]
    private TypeOfSort First_Obj_Z;




    public void SetSort(MainCoordinate main, TypeOfSort first_obj_x, TypeOfSort first_obj_z)
    {
        MainLine = main;
        First_Obj_X = first_obj_x;
        First_Obj_Z = first_obj_z;
    }



    public void Sort(GameObject[] objects)
    {
        for (int i = 0; i < (objects.Length - 1); i++)
            for (int i2 = 0; i2 < (objects.Length - 1 - i); i2++)
            {
                Vector3 pos1 = objects[i2].transform.position;
                Vector3 pos2 = objects[i2 + 1].transform.position;

                if (Compare(pos1, pos2))
                {
                    var buf = objects[i2 + 1];
                    objects[i2 + 1] = objects[i2];
                    objects[i2] = buf;
                }
            }
    }





    private bool Compare(Vector3 pos1, Vector3 pos2)
    {
        if ((First(pos1) < First(pos2)) || (First(pos1) == First(pos2) && Sec(pos1) < Sec(pos2)))
            return true;
        else
            return false;
    }

   

    private float First(Vector3 pos)
    {
        switch(MainLine.ToString())
        {
            case "X":
                {
                    if (First_Obj_X.ToString() == "Max")
                        return pos.x;
                    else
                        return -pos.x;
                }
            case "Z":
                {
                    if (First_Obj_Z.ToString() == "Max")
                        return pos.z;
                    else
                        return -pos.z;
                }
        }

        return 0;
    }

    float Sec(Vector3 pos)
    {
        switch (MainLine.ToString())
        {
            case "X":
                {
                    if (First_Obj_Z.ToString() == "Max")
                        return pos.z;
                    else
                        return -pos.z;
                }
            case "Z":
                {
                    if (First_Obj_X.ToString() == "Max")
                        return pos.x;
                    else
                        return -pos.x;
                }
        }

        return 0;
    }
}
