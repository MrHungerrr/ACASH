using UnityEngine;
using Single;


public class DeskManager : MonoSingleton<DeskManager>
{
    private enum TypeOfSort
    {
        Max,
        Min
    }

    private enum MainCoordinate
    {
        X,
        Z
    }

    [SerializeField]
    private MainCoordinate MainLine;

    [SerializeField]
    private TypeOfSort First_Desk_X;

    [SerializeField]
    private TypeOfSort First_Desk_Z;




    [HideInInspector]
    public ScholarComputer[] desks;



    public void Setup()
    {
        desks = GameObject.FindObjectsOfType<ScholarComputer>();
        DeskSort();
    }



    private void DeskSort()
    {
        //Самая первая парта у которой самый большой x и z

        for (int i = 0; i < (desks.Length - 1); i++)
            for (int i2 = 0; i2 < (desks.Length - 1 - i); i2++)
            {
                Vector3 pos1 = desks[i2].transform.position;
                Vector3 pos2 = desks[i2 + 1].transform.position;

                if (Sort(pos1, pos2))
                {
                    var buf = desks[i2 + 1];
                    desks[i2 + 1] = desks[i2];
                    desks[i2] = buf;
                }
            }


        for (int i = 0; i < desks.Length; i++)
        {
            //Debug.Log(desks[i].name + " = Desk_" + i);
            desks[i].name = "Desk_" + i;
        }
    }


    private bool Sort(Vector3 pos1, Vector3 pos2)
    {
        if ((First(pos1) < First(pos2)) || (First(pos1) == First(pos2) && Sec(pos1) < Sec(pos2)))
            return true;
        else
            return false;
    }

   

    float First(Vector3 pos)
    {
        switch(MainLine.ToString())
        {
            case "X":
                {
                    if (First_Desk_X.ToString() == "Max")
                        return pos.x;
                    else
                        return -pos.x;
                }
            case "Z":
                {
                    if (First_Desk_Z.ToString() == "Max")
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
                    if (First_Desk_Z.ToString() == "Max")
                        return pos.z;
                    else
                        return -pos.z;
                }
            case "Z":
                {
                    if (First_Desk_X.ToString() == "Max")
                        return pos.x;
                    else
                        return -pos.x;
                }
        }

        return 0;
    }
}
