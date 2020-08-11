using UnityEngine;
using Single;
using Places;


public class SortManager: Singleton<SortManager>
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

    private MainCoordinate mainLine;
    private TypeOfSort firstObjectX;
    private TypeOfSort firstObjectZ;



    public void Sort(MainCoordinate mainCoordinate, TypeOfSort firstObjectX, TypeOfSort firstObjectZ, Place[] places)
    {
        SetSortSettings(mainCoordinate, firstObjectX, firstObjectZ);
        Sort(places);
    }



    private void SetSortSettings(MainCoordinate mainCoordinate, TypeOfSort firstObjectX, TypeOfSort firstObjectZ)
    {
        mainLine = mainCoordinate;
        this.firstObjectX = firstObjectX;
        this.firstObjectZ = firstObjectZ;
    }



    private void Sort(Place[] places)
    {
        for (int i = 0; i < (places.Length - 1); i++)
            for (int i2 = 0; i2 < (places.Length - 1 - i); i2++)
            {
                Vector3 pos1 = places[i2].Destination;
                Vector3 pos2 = places[i2 + 1].Destination;

                if (Compare(pos1, pos2))
                {
                    var buf = places[i2 + 1];
                    places[i2 + 1] = places[i2];
                    places[i2] = buf;
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
        switch(mainLine.ToString())
        {
            case "X":
                {
                    if (firstObjectX == TypeOfSort.Max)
                        return pos.x;
                    else
                        return -pos.x;
                }
            case "Z":
                {
                    if (firstObjectZ == TypeOfSort.Max)
                        return pos.z;
                    else
                        return -pos.z;
                }
        }

        return 0;
    }

    float Sec(Vector3 pos)
    {
        switch (mainLine.ToString())
        {
            case "X":
                {
                    if (firstObjectZ == TypeOfSort.Max)
                        return pos.z;
                    else
                        return -pos.z;
                }
            case "Z":
                {
                    if (firstObjectX == TypeOfSort.Max)
                        return pos.x;
                    else
                        return -pos.x;
                }
        }

        return 0;
    }
}
