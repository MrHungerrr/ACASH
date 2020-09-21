using UnityEngine;
using Vkimow.Tools.Single;
using Objects._2D.Places;
using System;

namespace Objects._2D.Tools
{
    public class SortManager2D : Singleton<SortManager2D>
    {
        public enum TypeOfSort
        {
            Max,
            Min
        }

        public enum MainCoordinate
        {
            X,
            Y
        }

        private MainCoordinate mainLine;
        private TypeOfSort firstObjectX;
        private TypeOfSort firstObjectY;



        public void Sort(MainCoordinate mainCoordinate, TypeOfSort firstObjectX, TypeOfSort firstObjectY, GameObject[] places)
        {
            SetSortSettings(mainCoordinate, firstObjectX, firstObjectY);
            Sort(places, x => x.transform.position);
        }


        public void Sort<T>(MainCoordinate mainCoordinate, TypeOfSort firstObjectX, TypeOfSort firstObjectY, T[] GameObjects, Func<T, Vector3> getPosition) where T : MonoBehaviour
        {
            SetSortSettings(mainCoordinate, firstObjectX, firstObjectY);
            Sort(GameObjects, getPosition);
        }



        private void SetSortSettings(MainCoordinate mainCoordinate, TypeOfSort firstObjectX, TypeOfSort firstObjectY)
        {
            mainLine = mainCoordinate;
            this.firstObjectX = firstObjectX;
            this.firstObjectY = firstObjectY;
        }



        private void Sort<T>(T[] places, Func<T, Vector3> getPosition)
        {
            for (int i = 0; i < (places.Length - 1); i++)
                for (int i2 = 0; i2 < (places.Length - 1 - i); i2++)
                {
                    Vector3 pos1 = getPosition(places[i2]);
                    Vector3 pos2 = getPosition(places[i2 + 1]);

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
            switch (mainLine.ToString())
            {
                case "X":
                    {
                        if (firstObjectX == TypeOfSort.Max)
                            return pos.x;
                        else
                            return -pos.x;
                    }
                case "Y":
                    {
                        if (firstObjectY == TypeOfSort.Max)
                            return pos.y;
                        else
                            return -pos.y;
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
                        if (firstObjectY == TypeOfSort.Max)
                            return pos.y;
                        else
                            return -pos.y;
                    }
                case "Y":
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
}